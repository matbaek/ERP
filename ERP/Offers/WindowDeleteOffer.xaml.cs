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
using Domain.Offers;
using Application.Offers;

namespace ERP.Offers
{
    /// <summary>
    /// Interaction logic for WindowDeleteOffer.xaml
    /// </summary>
    public partial class WindowDeleteOffer : Window
    {
        private OfferRepository offerRepository = new OfferRepository();
        private Offer offer;
        public WindowDeleteOffer(Offer offer)
        {
            InitializeComponent();
            this.offer = offer;
            LabelDeleteOffer.Content = $"Er du sikker på at du vil slette ordren '{offer.OfferID}'?";
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            offerRepository.DeleteOffer(offer.OfferID);
            this.Close();
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
