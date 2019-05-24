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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ERP.Accounting
{
    /// <summary>
    /// Interaction logic for PageInvoice.xaml
    /// </summary>
    public partial class PageInvoice : Page
    {
        private InvoiceRepository invoiceRepository = new InvoiceRepository();
        private Invoice invoice = new Invoice();
        private List<Invoice> invoices = new List<Invoice>();
        public PageInvoice()
        {
            InitializeComponent();
            Update();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            WindowSearchInvoice wsi = new WindowSearchInvoice();
            wsi.ShowDialog();
            Update();
        }

        private void ButtonAddInvoice_Click(object sender, RoutedEventArgs e)
        {
            WindowAddInvoice wai = new WindowAddInvoice();
            wai.ShowDialog();
            Update();
        }

        private void ButtonSendInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (invoice.Send == true)
            {
                WindowShowDialog wsd = new WindowShowDialog();
                wsd.LabelShowDialog.Content = "Faktura er allerede sendt!";
                wsd.ShowDialog();
            }
            else if (invoice.InvoiceID != 0)
            {
                WindowSendInvoice wsi = new WindowSendInvoice(invoice);
                wsi.ShowDialog();
                Update();
            }
            else
            {
                WindowShowDialog wsd = new WindowShowDialog();
                wsd.LabelShowDialog.Content = "Ingen faktura er valgt!";
                wsd.ShowDialog();
            }
        }

        private void ButtonMakeCreditNota_Click(object sender, RoutedEventArgs e)
        {
            if (invoice.CreditNota == true)
            {
                WindowShowDialog wsd = new WindowShowDialog();
                wsd.LabelShowDialog.Content = "Faktura er allerede lavet til en kreditnota!";
                wsd.ShowDialog();
            }
            else if(invoice.InvoiceID != 0)
            {
                WindowMakeCreditNota wec = new WindowMakeCreditNota(invoice);
                wec.ShowDialog();
                Update();
            }
            else
            {
                WindowShowDialog wsd = new WindowShowDialog();
                wsd.LabelShowDialog.Content = "Ingen faktura er valgt!";
                wsd.ShowDialog();
            }
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            PageMenu pm = new PageMenu();
            this.NavigationService.Navigate(pm);
        }

        private void Update()
        {
            invoices = invoiceRepository.DisplayInvoices();
            List<Object> tempList = new List<Object>();
            for (int i = 0; i < invoices.Count; i++)
            {
                string tempCreditNota = string.Empty;
                string tempSend = string.Empty;
                if (invoices[i].CreditNota == true) { tempCreditNota = "Ja"; } else { tempCreditNota = "Nej"; }
                if (invoices[i].Send == true) { tempSend = "Ja"; } else { tempSend = "Nej"; }
                tempList.Add(new { InvoiceID = invoices[i].InvoiceID, Customer = invoices[i].Order.Customer.CompanyName, DateOfDelivery = invoices[i].DateOfDelivery, FormOfDelivery = invoices[i].FormOfDelivery, FormOfPayment = invoices[i].FormOfPayment, SendStatus = tempSend, CreditNotaStatus = tempCreditNota });
            }

            Invoices.ItemsSource = tempList;
        }

        private void Invoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Object)(Invoices.SelectedItem) != null)
            {
                invoice = invoices[Invoices.SelectedIndex];
            }
        }
    }
}
