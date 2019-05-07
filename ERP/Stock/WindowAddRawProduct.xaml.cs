using System;
using System.Windows;
using System.Windows.Controls;
using Application;
using Domain;

namespace ERP.Stock
{
    /// <summary>
    /// Interaction logic for WindowAddRawProduct.xaml
    /// </summary>
    public partial class WindowAddRawProduct : Window
    {
        private RawProductRepository rawProductRepository = new RawProductRepository();
        public WindowAddRawProduct()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            RawProduct rawProduct = new RawProduct();
            WindowShowDialog wsd = new WindowShowDialog();
            rawProduct.RawProductName = TextBoxName.Text.ToString();

            if(double.TryParse(TextBoxWeight.Text, out double resultWeight) && TextBoxDateOfPurchase.SelectedDate != null)
            {
                rawProduct.RawProductWeight = double.Parse(TextBoxWeight.Text);
                rawProduct.DateOfPurchase = DateTime.Parse(TextBoxDateOfPurchase.Text);

                rawProductRepository.AddRawProduct(rawProduct);

                wsd.LabelShowDialog.Content = "Råvaren blev tilføjet";
                wsd.ShowDialog();

                this.Close();
            }
            else
            {
                wsd.LabelShowDialog.Content = "Der var en fejl i 'Vægt' eller 'Indkøbsdato'";
                wsd.ShowDialog();
            }
        }

        private void TextBoxWeight_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
