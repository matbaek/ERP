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
using Application;
using Domain;

namespace ERP.Customer
{
    /// <summary>
    /// Interaction logic for PageCustomer.xaml
    /// </summary>
    public partial class PageCustomer : Page
    {
        private CustomerRepository customerRepository = new CustomerRepository();
        public PageCustomer()
        {
            InitializeComponent();
            List<Domain.Customer> items = customerRepository.DisplayCustomers();

            Customers.ItemsSource = items;
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAddRawProduct_Click(object sender, RoutedEventArgs e)
        {
            WindowAddCustomer wac = new WindowAddCustomer();
            wac.ShowDialog();
        }

        private void ButtonEditRawProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDeleteRawProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            PageMenu pm = new PageMenu();
            this.NavigationService.Navigate(pm);
        }
    }
}