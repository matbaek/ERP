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

namespace ERP
{
    /// <summary>
    /// Interaction logic for WindowDeleteCustomer.xaml
    /// </summary>
    public partial class WindowDeleteCustomer : Window
    {
        private CustomerRepository customerRepository = new CustomerRepository();
        private int customerID;
        public WindowDeleteCustomer(int customerID, string companyName)
        {
            InitializeComponent();
            this.customerID = customerID;
            LabelDeleteCustomer.Content = $"Er du sikker på at du vil slette '{companyName}'?";
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            customerRepository.DeleteCustomer(customerID);
            this.Close();
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
