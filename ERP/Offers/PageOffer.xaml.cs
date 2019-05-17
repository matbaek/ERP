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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Domain.Offers;
using Application.Offers;

namespace ERP.Offers
{
    /// <summary>
    /// Interaction logic for PageOffer.xaml
    /// </summary>
    public partial class PageOffer : Page
    {
        private OfferRepository offerRepository = new OfferRepository();
        private Offer offer;

        public PageOffer()
        {
            InitializeComponent();
            WindowSearchOffer.eventSendList += WindowsSearchOrder_eventSendList;
            Update();
        }

        private void ButtonAddOffer_Click(object sender, RoutedEventArgs e)
        {
            WindowAddOffer wao = new WindowAddOffer();
            wao.ShowDialog();
        }
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            WindowSearchOffer wso = new WindowSearchOffer();
            wso.ShowDialog();
        }

        private void ButtonEditOffer_Click(object sender, RoutedEventArgs e)
        {
            if (offer != null)
            {
                WindowEditOffer weo = new WindowEditOffer(offer);
                weo.ShowDialog();
                Update();
            }
            else
            {
                WindowShowDialog wsd = new WindowShowDialog();
                wsd.LabelShowDialog.Content = "Inget tilbud er valgt!";
                wsd.ShowDialog();
            }
        }

        private void ButtonDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (offer != null)
            {
                WindowDeleteOffer weo = new WindowDeleteOffer(offer);
                weo.ShowDialog();
                Update();
            }
            else
            {
                WindowShowDialog wsd = new WindowShowDialog();
                wsd.LabelShowDialog.Content = "Inget tilbud er valgt!";
                wsd.ShowDialog();
            }
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            PageMenu pm = new PageMenu();
            this.NavigationService.Navigate(pm);
        }

        private void Update()
        {
            List<Offer> items = offerRepository.DisplayOffers();
            Offers.ItemsSource = items;
        }

        private void Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Offer)(Offers.SelectedItem) != null)
            {
                offer = ((Offer)Offers.SelectedItem);
            }
        }

        void WindowsSearchOrder_eventSendList(List<Offer> items)
        {
            Offers.ItemsSource = items;
        }
    }
}

