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
    public partial class WindowSearchRawProduct : Window
    {
        private RawProductRepository rawProductRepository = new RawProductRepository();

        public delegate void SendList(List<RawProduct> items);
        public static event SendList eventSendList;
        public WindowSearchRawProduct()
        {
            InitializeComponent();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            RawProduct rawProduct = new RawProduct();
            WindowShowDialog wsd = new WindowShowDialog();
            rawProduct.RawProductName = TextBoxName.Text.ToString();

            if (double.TryParse(TextBoxWeight.Text, out double resultWeight) || TextBoxDateOfPurchase.SelectedDate != null)
            {
                if(double.TryParse(TextBoxWeight.Text, out resultWeight) == false)
                {
                    rawProduct.RawProductWeight = 0;
                }
                else
                {
                    rawProduct.RawProductWeight = double.Parse(TextBoxWeight.Text);
                }
                if(TextBoxDateOfPurchase.SelectedDate == null)
                {
                    rawProduct.DateOfPurchase = new DateTime(1973, 1, 1, 12, 0, 0);
                }
                else
                {
                    rawProduct.DateOfPurchase = DateTime.Parse(TextBoxDateOfPurchase.Text);
                }

                eventSendList(rawProductRepository.DisplaySpecificRawProducts(rawProduct));

                this.Close();
            }
            else if(TextBoxWeight.Text == string.Empty || TextBoxDateOfPurchase.SelectedDate == null)
            {
                rawProduct.DateOfPurchase = new DateTime(1973, 1, 1, 12, 0, 0);

                eventSendList(rawProductRepository.DisplaySpecificRawProducts(rawProduct));

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
