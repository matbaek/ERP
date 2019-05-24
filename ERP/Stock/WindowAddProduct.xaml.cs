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
        private RawProduct rawProduct = new RawProduct();

        public WindowAddProduct()
        {
            InitializeComponent();
            rawProducts = rawProductRepository.DisplayRawProducts();
            ComboBoxRawProduct.Items.Add("");
            for (int i = 0; i < rawProducts.Count; i++)
            {
                ComboBoxRawProduct.Items.Add($"{rawProducts[i].RawProductName} ({rawProducts[i].RawProductWeight} kg)");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            WindowShowDialog wsd = new WindowShowDialog();
            product.ProductName = TextBoxName.Text.ToString();

            if (double.TryParse(TextBoxWeightAfterLoss.Text, out double resultWeight) && double.TryParse(TextBoxPrice.Text, out double resultPrice) && double.TryParse(TextBoxAmount.Text, out double resultAmount) && TextBoxDateOfPackaging.SelectedDate != null && TextBoxDateOfExpiration.SelectedDate != null)
            {
                product.ProductWeight = resultWeight;
                product.ProductPrice = resultPrice;
                product.ProductAmount = resultAmount;
                product.DateOfPackaging = DateTime.Parse(TextBoxDateOfPackaging.Text);
                product.DateOfExpiration = DateTime.Parse(TextBoxDateOfExpiration.Text);

                productRepository.AddProduct(product);
                rawProduct.RawProductWeight -= double.Parse(TextBoxWeightAfterLoss.Text);
                rawProductRepository.EditRawProduct(rawProduct);
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

        private void CalculateAmountPerProduct(object sender, TextChangedEventArgs e)
        {
            double productWeight = 0;
            double productAmount = 1;
            if (TextBoxWeight.Text != "")
            {
                productWeight = double.Parse(TextBoxWeight.Text);
            }
            if (TextBoxAmount.Text != "")
            {
                productAmount = double.Parse(TextBoxAmount.Text);
            }
            double productWeightAfterLoss = (productWeight / 100) * (100 - double.Parse(TextBoxLoss.Text));
            TextBoxWeightAfterLoss.Text = productWeightAfterLoss.ToString();
            TextBoxWeightPerProduct.Text = (productWeightAfterLoss / productAmount).ToString();
        }

        private void ComboBoxRawProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int comboBoxNumber = ComboBoxRawProduct.SelectedIndex - 1;
            if (comboBoxNumber != -1)
            {
                TextBoxName.Width = 181;
                Loss.Visibility = Visibility.Visible;
                TextBoxLoss.Visibility = Visibility.Visible;
                TextBoxLoss.Text = "20";

                rawProduct = rawProducts[comboBoxNumber];
                TextBoxName.Text = rawProduct.RawProductName;
                TextBoxWeight.Text = rawProduct.RawProductWeight.ToString();
            }
            else
            {
                TextBoxName.Width = 273;
                Loss.Visibility = Visibility.Hidden;
                TextBoxLoss.Visibility = Visibility.Hidden;

                TextBoxName.Text = "";
                TextBoxWeight.Text = "0";
            }
        }
    }
}
