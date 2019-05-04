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
using Application;
using Domain;

namespace ERP.Stock
{
    /// <summary>
    /// Interaction logic for WindowDeleteProduct.xaml
    /// </summary>
    public partial class WindowDeleteProduct : Window
    {
        private ProductRepository productRepository = new ProductRepository();
        private int productID;
        public WindowDeleteProduct(int productID, string productName)
        {
            InitializeComponent();
            this.productID = productID;
            LabelDeleteProduct.Content = $"Er du sikker på at du vil slette '{productName}'?";
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            productRepository.DeleteProduct(productID);
            this.Close();
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
