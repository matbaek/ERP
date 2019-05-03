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
    /// Interaction logic for WindowDeleteRawProduct.xaml
    /// </summary>
    public partial class WindowDeleteRawProduct : Window
    {
        private RawProductRepository rawProductRepository = new RawProductRepository();
        private int rawProductID;
        public WindowDeleteRawProduct(int rawProductID, string rawProductName)
        {
            InitializeComponent();
            this.rawProductID = rawProductID;
            LabelDeleteRawProduct.Content = $"Er du sikker på at du vil slette '{rawProductName}'?";
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            rawProductRepository.DeleteRawProduct(rawProductID);
            this.Close();
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
