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
    /// Interaction logic for WindowAddCustomer.xaml
    /// </summary>
    public partial class WindowAddCustomer : Window
    {
        private CustomerRepository customerRepository = new CustomerRepository();
        public WindowAddCustomer()
        {
            InitializeComponent();
        }

        private void ButtonCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            Domain.Customer customer = new Domain.Customer();
            WindowShowDialog wsd = new WindowShowDialog();
            customer.CompanyName = TextBoxCompanyName.Text.ToString();

            if (TextBoxCustomerAddress != null && TextBoxCustomerTelephone != null && TextBoxCustomerZip != null && TextBoxCustomerTelephone != null && TextBoxCustomerEmail != null)
            {
                customer.CompanyName = TextBoxCompanyName.Text;
                customer.CustomerEmail = TextBoxCustomerEmail.Text;
                customer.CustomerAddress = TextBoxCustomerAddress.Text;
                customer.CustomerTelephone = TextBoxCustomerTelephone.Text;
                customer.CustomerZip = TextBoxCustomerZip.Text;
                customer.CustomerTown = TextBoxCustomerTown.Text;

                customerRepository.AddCustomer(customer);
                wsd.LabelShowDialog.Content = "Kunden blev tilføjet";
                wsd.ShowDialog();

                this.Close();
            }
            else
            {
                wsd.LabelShowDialog.Content = "Muligvis tommefelter";
                wsd.ShowDialog();
            }

        }
    }
}
