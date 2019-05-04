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
    /// Interaction logic for WindowEditProduct.xaml
    /// </summary>
    public partial class WindowEditProduct : Window
    {
        private ProductRepository productRepository = new ProductRepository();
        private Product product = new Product();

        public WindowEditProduct(int productID)
        {
            product = productRepository.DisplayProduct(productID);
            InitializeComponent();
            TextBoxName.Text = product.ProductName;
            TextBoxWeight.Text = product.ProductWeight.ToString();
            TextBoxPrice.Text = product.ProductPrice.ToString();
            TextBoxAmount.Text = product.ProductAmount.ToString();
            TextBoxDateOfPackaging.Text = product.DateOfPackaging.ToString();
            TextBoxDateOfExpiration.Text = product.DateOfExpiration.ToString();

        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            WindowShowDialog wsd = new WindowShowDialog();
            product.ProductName = TextBoxName.Text.ToString();

            if (double.TryParse(TextBoxWeight.Text, out double resultWeight) && double.TryParse(TextBoxPrice.Text, out double resultPrice) && double.TryParse(TextBoxAmount.Text, out double resultAmount) && TextBoxDateOfPackaging.SelectedDate != null && TextBoxDateOfExpiration.SelectedDate != null)
            {
                product.ProductWeight = double.Parse(TextBoxWeight.Text);
                product.ProductPrice = double.Parse(TextBoxPrice.Text);
                product.ProductAmount = double.Parse(TextBoxAmount.Text);
                product.DateOfPackaging = DateTime.Parse(TextBoxDateOfPackaging.Text);
                product.DateOfExpiration = DateTime.Parse(TextBoxDateOfExpiration.Text);

                productRepository.EditProduct(product);

                wsd.LabelShowDialog.Content = "Varen blev opdateret";
                wsd.ShowDialog();

                this.Close();
            }
            else
            {
                wsd.LabelShowDialog.Content = "Der var en fejl i lidt af hvert xd";
                wsd.ShowDialog();
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = string.Empty;
        }
    }
}
