using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Domain;
using Domain.Offers;
using Application;
using Application.Offers;

namespace ERP.Offers
{
    /// <summary>
    /// Interaction logic for WindowAddOffer.xaml
    /// </summary>
    public partial class WindowAddOffer : Window
    {
        private OfferRepository offerRepository = new OfferRepository();
        private List<Orderline> orderlines = new List<Orderline>();
        private Offer offer = new Offer();
        private Orderline orderline = new Orderline();
        private OrderlineRepository orderlineRepository = new OrderlineRepository();
        private List<Object> tempList = new List<Object>();

        public WindowAddOffer()
        {
            InitializeComponent();
            WindowPickProductOffer.eventSendProduct += WindowPickProductOffer_eventSendProduct;
            WindowPickCustomerOffer.eventSendList += WindowPickCustomerOffer_eventSendList;
            UpdateTotalPrice();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            WindowShowDialog wsd = new WindowShowDialog();

            if (double.TryParse(TextBoxTotalPrice.Text, out double resultTotalPrice) && TextBoxDateOfOffer.SelectedDate != null)
            {
                offer.TotalPrice = double.Parse(TextBoxTotalPrice.Text);
                offer.DateOfOffer = DateTime.Parse(TextBoxDateOfOffer.Text);
                offer.DateOfOfferExpiration = DateTime.Parse(TextBoxDateOfOffer.Text); //Skal lige laves om er bare det samme som DateOfOffer lige nu
                offerRepository.AddOffer(offer);

                for (int i = 0; i < orderlines.Count; i++)
                {
                    orderlines[i].ID = offerRepository.DisplayLastOfferID();
                    orderline.ID = orderlines[i].ID;
                    orderline.Product = orderlines[i].Product;
                    orderline.Amount = orderlines[i].Amount;
                    orderlineRepository.AddOrderline(orderline);
                }

                wsd.LabelShowDialog.Content = "Tilbudet blev tilføjet";
                wsd.ShowDialog();

                this.Close();
            }
            else
            {
                wsd.LabelShowDialog.Content = "Der var en fejl man";
                wsd.ShowDialog();
            }

        }
        private void ButtonAdd_Product_Click(object sender, RoutedEventArgs e)
        {
            WindowPickProductOffer wppo = new WindowPickProductOffer();
            wppo.ShowDialog();
        }

        void WindowPickProductOffer_eventSendProduct(Product item, double amount)
        {
            orderlines.Add(new Orderline(0, 0, item, amount));

            Orderlines.ItemsSource = orderlines;

            Update();
        }

        void WindowPickCustomerOffer_eventSendList(Domain.Customer items)
        {
            TextBoxCustomer.Text = items.CompanyName;
            offer.Customer = items;
        }

        private void TextBoxCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WindowPickCustomerOffer wpco = new WindowPickCustomerOffer();
            wpco.ShowDialog();
        }

        private void UpdateTotalPrice()
        {
            double totalPrice = 0;
            for (int i = 0; i < orderlines.Count; i++)
            {
                totalPrice += orderlines[i].Product.ProductPrice * orderlines[i].Amount;
            }
            TextBoxTotalPrice.Text = totalPrice.ToString();
        }

        private void Update()
        {
            CollectionViewSource.GetDefaultView(Orderlines.ItemsSource).Refresh();
            UpdateTotalPrice();
        }
    }
}
