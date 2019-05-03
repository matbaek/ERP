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
    /// Interaction logic for WindowEditRawProduct.xaml
    /// </summary>
    public partial class WindowEditRawProduct : Window
    {
        private RawProductRepository rawProductRepository = new RawProductRepository();
        private RawProduct rawProduct = new RawProduct();

        public WindowEditRawProduct(int rawProductID)
        {
            rawProduct = rawProductRepository.DisplayRawProduct(rawProductID);
            InitializeComponent();
            TextBoxName.Text = rawProduct.RawProductName;
            TextBoxWeight.Text = rawProduct.RawProductWeight.ToString();
            TextBoxDateOfPurchase.Text = rawProduct.DateOfPurchase.ToString();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            WindowShowDialog wsd = new WindowShowDialog();
            rawProduct.RawProductName = TextBoxName.Text.ToString();

            if (double.TryParse(TextBoxWeight.Text, out double resultWeight) && TextBoxDateOfPurchase.SelectedDate != null)
            {
                rawProduct.RawProductWeight = double.Parse(TextBoxWeight.Text);
                rawProduct.DateOfPurchase = DateTime.Parse(TextBoxDateOfPurchase.Text);

                rawProductRepository.EditRawProduct(rawProduct);

                wsd.LabelShowDialog.Content = "Råvaren blev opdateret";
                wsd.ShowDialog();

                this.Close();
            }
            else
            {
                wsd.LabelShowDialog.Content = "Der var en fejl i 'Vægt' eller 'Indkøbsdato'";
                wsd.ShowDialog();
            }
        }
    }
}
