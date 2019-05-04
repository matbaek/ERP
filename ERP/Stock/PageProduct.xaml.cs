using Domain;
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
using Application;

namespace ERP.Stock
{
    /// <summary>
    /// Interaction logic for PageProduct.xaml
    /// </summary>
    public partial class PageProduct : Page
    {
        private ProductRepository productRepository = new ProductRepository();
        private int productID = 0;
        private string productName = string.Empty;
        public PageProduct()
        {
            InitializeComponent();
            WindowSearchProduct.eventSendList += WindowsSearchProduct_eventSendList;
            Update();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            WindowSearchProduct wsp = new WindowSearchProduct();
            wsp.ShowDialog();
        }

        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            WindowAddProduct wap = new WindowAddProduct();
            wap.ShowDialog();
            Update();
        }

        private void ButtonEditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (productID != 0)
            {
                WindowEditProduct wep = new WindowEditProduct(productID);
                wep.ShowDialog();
                Update();
            }
            else
            {
                WindowShowDialog wsd = new WindowShowDialog();
                wsd.LabelShowDialog.Content = "Ingen vare er valgt!";
                wsd.ShowDialog();
            }
        }

        private void ButtonDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (productID != 0)
            {
                WindowDeleteProduct wdp = new WindowDeleteProduct(productID, productName);
                wdp.ShowDialog();
                Update();
            }
            else
            {
                WindowShowDialog wsd = new WindowShowDialog();
                wsd.LabelShowDialog.Content = "Ingen vare er valgt!";
                wsd.ShowDialog();
            }
        }

        private void Update()
        {
            List<Product> items = productRepository.DisplayProducts();
            Products.ItemsSource = items;
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            PageMenu pm = new PageMenu();
            this.NavigationService.Navigate(pm);
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((Product)Products.SelectedItem) != null)
            {
                productID = ((Product)Products.SelectedItem).ProductID;
                productName = ((Product)Products.SelectedItem).ProductName;
            }
        }

        void WindowsSearchProduct_eventSendList(List<Product> items)
        {
            Products.ItemsSource = items;
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}