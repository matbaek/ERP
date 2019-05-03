using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using Application;
using Domain;

namespace ERP.Stock
{
    /// <summary>
    /// Interaction logic for PageRawProduct.xaml
    /// </summary>
    public partial class PageRawProduct : Page
    {
        private RawProductRepository rawProductRepository = new RawProductRepository();
        private int rawProductID = 0;
        private string rawProductName = string.Empty;
        public PageRawProduct()
        {
            InitializeComponent();
            WindowSearchRawProduct.eventSendList += WindowsSearchRawProduct_eventSendList;
            Update();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            WindowSearchRawProduct wsrp = new WindowSearchRawProduct();
            wsrp.ShowDialog();
        }

        private void ButtonAddRawProduct_Click(object sender, RoutedEventArgs e)
        {
            WindowAddRawProduct warp = new WindowAddRawProduct();
            warp.ShowDialog();
            Update();
        }

        private void ButtonEditRawProduct_Click(object sender, RoutedEventArgs e)
        {
            if (rawProductID != 0)
            {
                WindowEditRawProduct werp = new WindowEditRawProduct(rawProductID);
                werp.ShowDialog();
                Update();
            }
            else
            {
                WindowShowDialog wsd = new WindowShowDialog();
                wsd.LabelShowDialog.Content = "Ingen råvare er valgt!";
                wsd.ShowDialog();
            }
        }

        private void ButtonDeleteRawProduct_Click(object sender, RoutedEventArgs e)
        {
            if (rawProductID != 0)
            {
                WindowDeleteRawProduct wdrp = new WindowDeleteRawProduct(rawProductID, rawProductName);
                wdrp.ShowDialog();
                Update();
            }
            else
            {
                WindowShowDialog wsd = new WindowShowDialog();
                wsd.LabelShowDialog.Content = "Ingen råvare er valgt!";
                wsd.ShowDialog();
            }
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            PageMenu pm = new PageMenu();
            this.NavigationService.Navigate(pm);
        }

        private void RawProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(((RawProduct)RawProducts.SelectedItem) != null)
            {
                rawProductID = ((RawProduct)RawProducts.SelectedItem).RawProductID;
                rawProductName = ((RawProduct)RawProducts.SelectedItem).RawProductName;
            }
        }


        private void Update()
        {
            List<RawProduct> items = rawProductRepository.DisplayRawProducts();
            RawProducts.ItemsSource = items;
        }

        void WindowsSearchRawProduct_eventSendList(List<RawProduct> items)
        {
            RawProducts.ItemsSource = items;
        }
    }
}
