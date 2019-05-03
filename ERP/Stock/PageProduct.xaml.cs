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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ERP.Stock
{
    /// <summary>
    /// Interaction logic for PageProduct.xaml
    /// </summary>
    public partial class PageProduct : Page
    {
        public PageProduct()
        {
            InitializeComponent();
            List<Product> items = new List<Product>();
            items.Add(new Product() { Name = "John Doe", Weight = 42, Price = 42, Amount = 42, PackingDate = new DateTime(), ExpirationDate = new DateTime() });
            items.Add(new Product() { Name = "Jane Doe", Weight = 39, Price = 42, Amount = 42, PackingDate = new DateTime(), ExpirationDate = new DateTime() });
            items.Add(new Product() { Name = "Sammy Doe", Weight = 7, Price = 42, Amount = 42, PackingDate = new DateTime(), ExpirationDate = new DateTime() });
            Products.ItemsSource = items;
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            WindowAddProduct wap = new WindowAddProduct();
            wap.ShowDialog();
        }

        private void ButtonEditProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDeleteProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            PageMenu pm = new PageMenu();
            this.NavigationService.Navigate(pm);
        }
    }
    public class Product
    {
        public string Name { get; set; }
        public double Weight{ get; set; }
        public double Price{ get; set; }
        public double Amount { get; set; }
        public DateTime PackingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}