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

            if (TextBoxCompanyName != null || TextBoxContactPerson != null || TextBoxCustomerAddress != null || TextBoxCustomerTown != null || TextBoxCustomerZip != null || TextBoxCustomerTelephone != null)
            {
                if (TextBoxCompanyName.Text.ToString() == "Firma navn")
                {
                    customer.CompanyName = string.Empty;
                }
                else
                {
                    customer.CompanyName = TextBoxCompanyName.Text.ToString();
                }

                if (TextBoxContactPerson.Text.ToString() == "Kontakt person")
                {
                    customer.ContactPerson = string.Empty;
                }
                else
                {
                    customer.ContactPerson = TextBoxContactPerson.Text.ToString();
                }

                if (TextBoxCustomerAddress.Text.ToString() == "Adresse")
                {
                    customer.CustomerAddress = string.Empty;
                }
                else
                {
                    customer.CustomerAddress = TextBoxCustomerAddress.Text.ToString();
                }

                if (TextBoxCustomerTown.Text.ToString() == "By")
                {
                    customer.CustomerTown = string.Empty;
                }
                else
                {
                    customer.CustomerTown = TextBoxCustomerTown.Text.ToString();
                }

                if (TextBoxCustomerZip.Text.ToString() == "Postnr")
                {
                    customer.CustomerZip = string.Empty;
                }
                else
                {
                    customer.CustomerZip = TextBoxCustomerZip.Text.ToString();
                }

                if (TextBoxCustomerTelephone.Text.ToString() == "Telefon")
                {
                    customer.CustomerTelephone = string.Empty;
                }
                else
                {
                    customer.CustomerTelephone = TextBoxCustomerTelephone.Text.ToString();
                }

                eventSendList(customerRepository.DisplaySpecificCustomers(customer));

                this.Close();
            }
            else
            {
                wsd.LabelShowDialog.Content = "Der var en fejl i 'Firma navn', 'Kontakt person', 'Adresse', 'By', 'Postnr' eller 'Telefon'";
                wsd.ShowDialog();
            }
        }
        private void TextBoxContactPerson_TextChanged()
        {

        }
    }
}
