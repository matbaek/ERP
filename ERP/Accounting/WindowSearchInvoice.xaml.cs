using Domain;
using ERP.Orders;
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

namespace ERP.Accounting
{
    /// <summary>
    /// Interaction logic for WindowSearchInvoice.xaml
    /// </summary>
    public partial class WindowSearchInvoice : Window
    {
        private Invoice invoice = new Invoice();
        public WindowSearchInvoice()
        {
            InitializeComponent();
            WindowPickCustomer.eventSendList += WindowPickCustomer_eventSendList;
            invoice.Order = new Order();

            ComboBoxFormOfDelivery.Items.Add("Afhentning");
            ComboBoxFormOfDelivery.Items.Add("GLS");

            ComboBoxFormOfPayment.Items.Add("Dankort");
            ComboBoxFormOfPayment.Items.Add("Kontant");
            ComboBoxFormOfPayment.Items.Add("MobilePay");
        }

        private void ButtonSearchInvoice_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBoxCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WindowPickCustomer wpc = new WindowPickCustomer();
            wpc.ShowDialog();
        }

        void WindowPickCustomer_eventSendList(Domain.Customer customer)
        {
            TextBoxCustomer.Text = customer.CompanyName;
            invoice.Order.Customer = customer;
        }

        private void ComboBoxFormOfDelivery_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            invoice.FormOfDelivery = ComboBoxFormOfDelivery.SelectedItem.ToString();
        }

        private void ComboBoxFormOfPayment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            invoice.FormOfPayment = ComboBoxFormOfPayment.SelectedItem.ToString();
        }
    }
}
