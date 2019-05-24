using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class InvoiceRepository
    {
        private Invoice invoice = new Invoice();

        //Metods
        public void AddInvoice(Invoice invoice)
        {
            invoice.AddInvoice(invoice.Order, invoice.DateOfDelivery, invoice.FormOfDelivery, invoice.FormOfPayment);
        }

        public void EditInvoice(Invoice invoice)
        {
            invoice.EditInvoice(invoice.InvoiceID, invoice.Order, invoice.DateOfDelivery, invoice.FormOfDelivery, invoice.FormOfPayment, true, false);
        }

        public void SendInvoice(Invoice invoice)
        {
            invoice.SendInvoice(invoice, invoice.Order.Customer);
        }

        public List<Invoice> DisplayInvoices()
        {
            return invoice.GetInvoices();
        }

        public Invoice DisplayInvoice(int invoiceID)
        {
            return invoice.GetInvoice(invoiceID);
        }

        public bool DisplayInvoiceFromOrder(int orderID)
        {
            return invoice.GetInvoiceFromOrder(orderID);
        }
    }
}
