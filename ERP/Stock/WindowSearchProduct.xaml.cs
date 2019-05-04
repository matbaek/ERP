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
    /// Interaction logic for WindowSearchRawProduct.xaml
    /// </summary>
    public partial class WindowSearchProduct : Window
    {
        private ProductRepository productRepository = new ProductRepository();

        public delegate void SendList(List<Product> items);
        public static event SendList eventSendList;
        public WindowSearchProduct()
        {
            InitializeComponent();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            WindowShowDialog wsd = new WindowShowDialog();

            if (double.TryParse(TextBoxWeight.Text, out double resultWeight) && double.TryParse(TextBoxPrice.Text, out double resultPrice) && double.TryParse(TextBoxAmount.Text, out double resultAmount) || TextBoxDateOfPackaging.SelectedDate != null || TextBoxDateOfExpiration != null)
            {
                if (TextBoxName.Text.ToString() == "Navn")
                {
                    product.ProductName = string.Empty;
                }
                else
                {
                    product.ProductName = TextBoxName.Text.ToString();
                }
                if(double.TryParse(TextBoxWeight.Text, out resultWeight) == false)
                {
                    product.ProductWeight = 0;
                }
                else
                {
                    product.ProductWeight = double.Parse(TextBoxWeight.Text);
                }
                if (double.TryParse(TextBoxPrice.Text, out resultPrice) == false)
                {
                    product.ProductPrice = 0;
                }
                else
                {
                    product.ProductPrice = double.Parse(TextBoxPrice.Text);
                }
                if (double.TryParse(TextBoxAmount.Text, out resultAmount) == false)
                {
                    product.ProductAmount = 0;
                }
                else
                {
                    product.ProductAmount = double.Parse(TextBoxAmount.Text);
                }
                if (TextBoxDateOfPackaging.SelectedDate == null)
                {
                    product.DateOfPackaging = new DateTime(1973, 1, 1, 12, 0, 0);
                }
                else
                {
                    product.DateOfPackaging = DateTime.Parse(TextBoxDateOfPackaging.Text);
                }
                if (TextBoxDateOfExpiration.SelectedDate == null)
                {
                    product.DateOfExpiration = new DateTime(1973, 1, 1, 12, 0, 0);
                }
                else
                {
                    product.DateOfExpiration = DateTime.Parse(TextBoxDateOfExpiration.Text);
                }

                eventSendList(productRepository.DisplaySpecificProducts(product));

                this.Close();
            }
            else if(TextBoxWeight.Text == string.Empty || TextBoxPrice.Text == string.Empty || TextBoxAmount.Text == string.Empty || TextBoxDateOfPackaging.SelectedDate == null || TextBoxDateOfExpiration == null)
            {
                product.DateOfPackaging = new DateTime(1973, 1, 1, 12, 0, 0);
                product.DateOfExpiration = new DateTime(1973, 1, 1, 12, 0, 0);

                eventSendList(productRepository.DisplaySpecificProducts(product));

                this.Close();
            }
            else
            {
                wsd.LabelShowDialog.Content = "Der var en fejl i 'Vægt', 'Pris', 'Antal' 'Pakkedato' eller 'Udløbsdato'";
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
