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
    /// Interaction logic for WindowPickProductOffer.xaml
    /// </summary>
    public partial class WindowPickProductOffer : Window
    {
        private ProductRepository productRepository = new ProductRepository();
        private Product product;
        private bool sendProduct = false;
        private double amount = 0;

        public delegate void SendProduct(Product items, double amount);
        public static event SendProduct eventSendProduct;
        public WindowPickProductOffer()
        {
            InitializeComponent();
            WindowPickProductAmountOffer.eventSendProductAmountOffer += WindowPickProductAmountOffer_eventSendProductAmountOffer;
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
            Products.ItemsSource = items;
        }

        private void Products_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WindowPickProductAmountOffer wpao = new WindowPickProductAmountOffer(product);
            wpao.ShowDialog();
            if (sendProduct)
            {
                eventSendProduct(product, amount);
                this.Close();
            }
        }

        void WindowPickProductAmountOffer_eventSendProductAmountOffer(double amount, bool sendProduct)
        {
            this.amount = amount;
            this.sendProduct = sendProduct;
        }
    }
}

