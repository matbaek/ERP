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
        private bool sendProduct = false;
        private double amount = 0;

        public delegate void SendProduct(Product items, double amount);
        public static event SendProduct eventSendProduct;
        public WindowPickProduct()
        {
            InitializeComponent();
            WindowProductAmount.eventSendProductAmount += WindowProductAmount_eventSendProductAmount;
            Update();
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((Product)Products.SelectedItem) != null)
            {
                product = ((Product)Products.SelectedItem);
            }
        }

        private void Update()
        {
            List<Product> items = productRepository.DisplayProducts();

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ProductAmount == 0)
                {
                    items.RemoveAt(i);
                }
            }
            Products.ItemsSource = items;
        }

        private void Products_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WindowProductAmount wpa = new WindowProductAmount(product);
            wpa.ShowDialog();
            if(sendProduct)
            {
                eventSendProduct(product, amount);
                this.Close();
            }
        }

        void WindowProductAmount_eventSendProductAmount(double amount, bool sendProduct)
        {
            this.amount = amount;
            this.sendProduct = sendProduct;
        }
    }
}