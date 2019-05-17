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
using Domain;
using Application;

namespace ERP.Offers
{
    /// <summary>
    /// Interaction logic for WindowPickCustomer.xaml
    /// </summary>
    public partial class WindowPickCustomerOffer : Window
    {
        private CustomerRepository customerRepository = new CustomerRepository();
        private Domain.Customer customer;

        public delegate void SendList(Domain.Customer customer);
        public static event SendList eventSendList;
        public WindowPickCustomerOffer()
        {
            InitializeComponent();
            Update();
        }

        private void Customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((Domain.Customer)Customers.SelectedItem) != null)
            {
                customer = ((Domain.Customer)Customers.SelectedItem);
            }
        }

        private void Customers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            eventSendList(customer);
            this.Close();
        }

        private void Update()
        {
            List<Domain.Customer> items = customerRepository.DisplayCustomers();
            Customers.ItemsSource = items;
        }
    }
}
