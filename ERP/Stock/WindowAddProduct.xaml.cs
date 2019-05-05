using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Application;
using Domain;


namespace ERP.Stock
{
    /// <summary>
    /// Interaction logic for WindowAddProduct.xaml
    /// </summary>
    public partial class WindowAddProduct : Window
    {
        private ProductRepository productRepository = new ProductRepository();
        private RawProductRepository rawProductRepository = new RawProductRepository();
        private List<RawProduct> rawProducts = new List<RawProduct>();

        public WindowAddProduct()
        {
            InitializeComponent();
            rawProducts = rawProductRepository.DisplayRawProducts();
            for (int i = 0; i < rawProducts.Count; i++)
            {
                ComboBoxRawProduct.Items.Add($"{rawProducts[i].RawProductName} ({rawProducts[i].RawProductWeight} kg)");
                ComboBoxRawProduct.Name = $"{rawProducts[i].RawProductID}";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            WindowShowDialog wsd = new WindowShowDialog();
            product.ProductName = TextBoxName.Text.ToString();

            if (double.TryParse(TextBoxWeight.Text, out double resultWeight) && double.TryParse(TextBoxPrice.Text, out double resultPrice) && double.TryParse(TextBoxAmount.Text, out double resultAmount) && TextBoxDateOfPackaging.SelectedDate != null && TextBoxDateOfExpiration.SelectedDate != null)
            {
                product.ProductWeight = resultWeight;
                product.ProductPrice = resultPrice;
                product.ProductAmount = resultAmount;
                product.DateOfPackaging = DateTime.Parse(TextBoxDateOfPackaging.Text);
                product.DateOfExpiration = DateTime.Parse(TextBoxDateOfExpiration.Text);

                productRepository.AddProduct(product);
                wsd.LabelShowDialog.Content = "Varen blev tilføjet";
                wsd.ShowDialog();

                this.Close();
            }
            else
            {
                wsd.LabelShowDialog.Content = "Der var en fejl i 'Vægt' eller 'Indkøbsdato'";
                wsd.ShowDialog();
            }



        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = string.Empty;
        }

        private void ComboBoxRawProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int rawProductID = 1;
            TextBoxWeight.Text = rawProducts[rawProductID].RawProductName;

        }
    }
}
