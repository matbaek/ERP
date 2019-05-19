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
using Application;


namespace ERP.Offers
{
    /// <summary>
    /// Interaction logic for WindowEditOffer.xaml
    /// </summary>
    public partial class WindowEditOffer : Window
    {
        private OfferRepository offerRepository = new OfferRepository();
        private List<Orderline> orderlines = new List<Orderline>();
        private Offer offer = new Offer();
        private Orderline orderline = new Orderline();
        private OrderlineRepository orderlineRepository = new OrderlineRepository();
        private Domain.Customer customer = new Domain.Customer();
        public WindowEditOffer(Offer offer)
        {
            InitializeComponent();
            TextBoxCustomer.Text = offer.Customer.CompanyName;
            TextBoxTotalPrice.Text = offer.TotalPrice.ToString();
            TextBoxDateOfOffer.Text = offer.DateOfOffer.ToString();
            orderlines = orderlineRepository.DisplayOrderlines(new Order(), offer);
            customer = offer.Customer;

            Orderlines.ItemsSource = orderlines;

            WindowPickCustomerOffer.eventSendList += WindowPickCustomerOffer_eventSendList;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            WindowShowDialog wsd = new WindowShowDialog();

            if (double.TryParse(TextBoxTotalPrice.Text, out double resultTotalPrice) && TextBoxDateOfOffer.SelectedDate != null)
            {
                offer.TotalPrice = double.Parse(TextBoxTotalPrice.Text);
                offer.DateOfOffer = DateTime.Parse(TextBoxDateOfOffer.Text);
                offerRepository.EditOffer(offer);

                wsd.LabelShowDialog.Content = "Ordren blev redigeret";
                wsd.ShowDialog();

                this.Close();
            }
            else
            {
                wsd.LabelShowDialog.Content = "Der var en fejl man";
                wsd.ShowDialog();
            }
        }

        void WindowPickCustomerOffer_eventSendList(Domain.Customer items)
        {
            TextBoxCustomer.Text = items.CompanyName;
            offer.Customer = items;
        }

        private void TextBoxCustomer_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WindowPickCustomerOffer wpco = new WindowPickCustomerOffer();
            wpco.ShowDialog();
        }

        private void Update()
        {
            CollectionViewSource.GetDefaultView(Orderlines.ItemsSource).Refresh();
        }
    }
}
