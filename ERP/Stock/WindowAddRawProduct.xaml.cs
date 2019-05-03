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

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = string.Empty;
        }
    }
}
