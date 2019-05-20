using Domain;
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

namespace ERP
{
    /// <summary>
    /// Interaction logic for PageMenu.xaml
    /// </summary>
    public partial class PageMenu : Page
    {
        public PageMenu()
        {
            InitializeComponent();
        }

        private void ButtonProduct_Click(object sender, RoutedEventArgs e)
        {
            Stock.PageProduct pp = new Stock.PageProduct();
            this.NavigationService.Navigate(pp);
        }

        private void ButtonRawProduct_Click(object sender, RoutedEventArgs e)
        {
            Stock.PageRawProduct prp = new Stock.PageRawProduct();
            this.NavigationService.Navigate(prp);
        }

        private void ButtonCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer.PageCustomer pc = new Customer.PageCustomer();
            this.NavigationService.Navigate(pc);
        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            Orders.PageOrder po = new Orders.PageOrder();
            this.NavigationService.Navigate(po);
        }

        private void ButtonAccounting_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
