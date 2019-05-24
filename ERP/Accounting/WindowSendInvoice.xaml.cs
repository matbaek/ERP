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
    /// Interaction logic for WindowSendInvoice.xaml
    /// </summary>
    public partial class WindowSendInvoice : Window
    {
        private OrderlineRepository orderlineRepository = new OrderlineRepository();
        private ProductRepository productRepository = new ProductRepository();
        private InvoiceRepository invoiceRepository = new InvoiceRepository();
        private Invoice invoice = new Invoice();
        public WindowSendInvoice(Invoice invoice)
        {
            InitializeComponent();
            this.invoice = invoice;
            TextBoxCustomer.Text = invoice.Order.Customer.CompanyName;
            TextBoxDateOfDelivery.Text = invoice.DateOfDelivery.ToString();
            TextBoxFormOfDelivery.Text = invoice.FormOfDelivery;
            TextBoxFormOfPayment.Text = invoice.FormOfPayment;
            Orderlines.ItemsSource = orderlineRepository.DisplayOrderlines(invoice.Order);
            TextBoxTotalPrice.Text = invoice.Order.TotalPrice.ToString();
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            WindowShowDialog wsd = new WindowShowDialog();

            invoiceRepository.SendInvoice(invoice);

            wsd.LabelShowDialog.Content = "Ikke implementeret endnu!";
            wsd.ShowDialog();

            this.Close();
        }
    }
}
