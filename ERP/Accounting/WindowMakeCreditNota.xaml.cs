using Application;
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
using System.Windows.Shapes;

namespace ERP.Accounting
{
    /// <summary>
    /// Interaction logic for WindowMakeCreditNota.xaml
    /// </summary>
    public partial class WindowMakeCreditNota : Window
    {
        private OrderlineRepository orderlineRepository = new OrderlineRepository();
        private InvoiceRepository invoiceRepository = new InvoiceRepository();
        private Invoice invoice = new Invoice();
        public WindowMakeCreditNota(Invoice invoice)
        {
            InitializeComponent();
            TextBoxCustomer.Text = invoice.Order.Customer.CompanyName;
            TextBoxDateOfDelivery.Text = invoice.DateOfDelivery.ToString();
            TextBoxFormOfDelivery.Text = invoice.FormOfDelivery;
            TextBoxFormOfPayment.Text = invoice.FormOfPayment;
            Orderlines.ItemsSource = orderlineRepository.DisplayOrderlines(invoice.Order);
            TextBoxTotalPrice.Text = invoice.Order.TotalPrice.ToString();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
