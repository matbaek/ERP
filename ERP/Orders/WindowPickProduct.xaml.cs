using Application;
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
using System.Windows.Shapes;

namespace ERP.Orders
{
    /// <summary>
    /// Interaction logic for WindowPickProduct.xaml
    /// </summary>
    public partial class WindowPickProduct : Window
    {
        private ProductRepository productRepository = new ProductRepository();
        private Product product;

        public delegate void SendProduct(Product items);
        public static event SendProduct eventSendProduct;
        public WindowPickProduct()
        {
            InitializeComponent();
            Update();
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((Product)Products.SelectedItem) != null)
            {
                product = ((Product)Products.SelectedItem);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            eventSendProduct(product);
            this.Close();

        }
        private void Update()
        {
            List<Product> items = productRepository.DisplayProducts();
            Products.ItemsSource = items;
        }

    }
}