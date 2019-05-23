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
    /// Interaction logic for WindowEditCustomer.xaml
    /// </summary>
    public partial class WindowEditCustomer : Window
    {
        private CustomerRepository customerRepository = new CustomerRepository();
        private Domain.Customer customer = new Domain.Customer();
        public WindowEditCustomer(int customerID)
        {
            InitializeComponent();
            customer = customerRepository.DisplayCustomer(customerID);
            TextBoxCompanyName.Text = customer.CompanyName;
            TextBoxCustomerAddress.Text = customer.CustomerAddress.ToString();
            TextBoxCustomerTelephone.Text = customer.CustomerTelephone.ToString();
            TextBoxCustomerZip.Text = customer.CustomerZip.ToString();
            TextBoxCustomerTown.Text = customer.CustomerTown.ToString();
        }

        private void ButtonEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            WindowShowDialog wsd = new WindowShowDialog();

            if (TextBoxCustomerTelephone != null && TextBoxCustomerZip != null && TextBoxCustomerTown != null)
            {
                customer.CompanyName = TextBoxCompanyName.Text;
                customer.CustomerAddress = TextBoxCustomerAddress.Text;
                customer.CustomerTelephone = TextBoxCustomerTelephone.Text;
                customer.CustomerZip = TextBoxCustomerZip.Text;
                customer.CustomerTown = TextBoxCustomerTown.Text;

                customerRepository.EditCustomer(customer);

                wsd.LabelShowDialog.Content = "Kunden blev opdateret";
                wsd.ShowDialog();

                this.Close();
            }
            else
            {
                wsd.LabelShowDialog.Content = "Der var en fejl i opdateringen af kunden";
                wsd.ShowDialog();
            }

        }
    }
}
