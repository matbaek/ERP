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

        public List<Invoice> DisplayInvoices()
        {
            return invoice.GetInvoices();
        }

        public Invoice DisplayInvoice(int invoiceID)
        {
            return invoice.GetInvoice(invoiceID);
        }
    }
}
