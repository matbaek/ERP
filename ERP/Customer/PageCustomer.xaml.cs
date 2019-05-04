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
        private int customerID = 0;
        private string companyName = string.Empty;
        public PageCustomer()
        {
            InitializeComponent();
            WindowSearchCustomer.eventSendList += WindowSearchCustomer_eventSendList;
            Update();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            WindowSearchCustomer wsc = new WindowSearchCustomer();
            wsc.ShowDialog();
        }

        private void ButtonAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            WindowAddCustomer wac = new WindowAddCustomer();
            wac.ShowDialog();
            Update();
        }

        private void ButtonEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (customerID != 0)
            {
                WindowEditCustomer wec = new WindowEditCustomer(customerID);
                wec.ShowDialog();
                Update();
            }
            else
            {
                WindowShowDialog wsd = new WindowShowDialog();
                wsd.LabelShowDialog.Content = "Ingen kunde er valgt!";
                wsd.ShowDialog();
            }

        }

        private void ButtonDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (customerID != 0)
            {
                WindowDeleteCustomer wdp = new WindowDeleteCustomer(customerID, companyName);
                wdp.ShowDialog();
                Update();
            }
            else
            {
                WindowShowDialog wsd = new WindowShowDialog();
                wsd.LabelShowDialog.Content = "Ingen kunde er valgt!";
                wsd.ShowDialog();
            }
        }

        private void Update()
        {
            List<Domain.Customer> items = customerRepository.DisplayCustomers();
            Customers.ItemsSource = items;
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            PageMenu pm = new PageMenu();
            this.NavigationService.Navigate(pm);
        }

        private void Customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((Domain.Customer)Customers.SelectedItem) != null)
            {
                customerID = ((Domain.Customer)Customers.SelectedItem).CustomerID;
                companyName = ((Domain.Customer)Customers.SelectedItem).CompanyName;
            }
        }

        void WindowSearchCustomer_eventSendList(List<Domain.Customer> items)
        {
            Customers.ItemsSource = items;
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}