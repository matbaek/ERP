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
    /// Interaction logic for WindowSearchOffer.xaml
    /// </summary>
    public partial class WindowSearchOffer : Window
    {
        private OfferRepository offerRepository = new OfferRepository();

        public delegate void SendList(List<Offer> items);
        public static event SendList eventSendList;

        public WindowSearchOffer()
        {
            InitializeComponent();
        }

        private void ButtonSearchOffer_Click(object sender, RoutedEventArgs e)
        {
            Offer offer = new Offer();
            Domain.Customer customer = new Domain.Customer();
            WindowShowDialog wsd = new WindowShowDialog();

            if (double.TryParse(TextBoxOfferID.Text, out double resultOrderID) || (double.TryParse(TextBoxCustomer.Text, out double resultCustomer) || double.TryParse(TextBoxTotalPrice.Text, out double resultTotalPrice) || DatePickerDateOfOffer.SelectedDate != null))
            {
                if (double.TryParse(TextBoxOfferID.Text, out resultOrderID) == false)
                {
                    offer.OfferID = 0;
                }
                else
                {
                    offer.OfferID = int.Parse(TextBoxOfferID.Text);
                }

                if (double.TryParse(TextBoxCustomer.Text, out resultCustomer) == false)
                {
                    offer.Customer = new Domain.Customer();
                    offer.Customer.CustomerID = 0;
                }
                else
                {
                    offer.Customer = customer.GetCustomer(int.Parse(TextBoxCustomer.Text));
                }

                if (double.TryParse(TextBoxTotalPrice.Text, out resultTotalPrice) == false)
                {
                    offer.TotalPrice = 0;
                }
                else
                {
                    offer.TotalPrice = double.Parse(TextBoxTotalPrice.Text);
                }

                if (DatePickerDateOfOffer.SelectedDate == null)
                {
                    offer.DateOfOffer = new DateTime(1973, 1, 1, 12, 0, 0);
                }
                else
                {
                    offer.DateOfOffer = DateTime.Parse(DatePickerDateOfOffer.Text);
                }

                eventSendList(offerRepository.DisplaySpecificOffers(offer));
                this.Close();
            }

            else
            {
                wsd.LabelShowDialog.Content = "Der var en fejl i 'Order nummer', 'Kunde', 'Samlet pris' eller 'Pakke dato'";
                wsd.ShowDialog();
            }
        }
    }
}
