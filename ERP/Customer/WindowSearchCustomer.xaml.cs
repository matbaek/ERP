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

namespace ERP.Customer
{
    /// <summary>
    /// Interaction logic for WindowSearchCustomer.xaml
    /// </summary>
    public partial class WindowSearchCustomer : Window
    {
        private CustomerRepository customerRepository = new CustomerRepository();

        public delegate void SendList(List<Domain.Customer> items);
        public static event SendList eventSendList;
        public WindowSearchCustomer()
        {
            InitializeComponent();
        }

        private void ButtonSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            Domain.Customer customer = new Domain.Customer();
            WindowShowDialog wsd = new WindowShowDialog();

            if (TextBoxCompanyName != null || TextBoxCustomerEmail != null || TextBoxCustomerAddress != null || TextBoxCustomerTown != null || TextBoxCustomerZip != null || TextBoxCustomerTelephone != null)
            {
                customer.CompanyName = TextBoxCompanyName.Text.ToString();
                customer.CustomerEmail = TextBoxCustomerEmail.Text.ToString();
                customer.CustomerAddress = TextBoxCustomerAddress.Text.ToString();
                customer.CustomerTown = TextBoxCustomerTown.Text.ToString();
                customer.CustomerZip = TextBoxCustomerZip.Text.ToString();
                customer.CustomerTelephone = TextBoxCustomerTelephone.Text.ToString();

                eventSendList(customerRepository.DisplaySpecificCustomers(customer));

                this.Close();
            }
            else
            {
                wsd.LabelShowDialog.Content = "Der var en fejl i 'Firma navn', 'Adresse', 'By', 'Postnr' eller 'Telefon'";
                wsd.ShowDialog();
            }
        }
    }
}
