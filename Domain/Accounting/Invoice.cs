﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Invoice : DB.Database
    {
        //Property
        public int InvoiceID { get; set; }
        public Order Order { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public string FormOfDelivery { get; set; }
        public string FormOfPayment { get; set; }
        public bool CreditNote { get; set; }
        public bool Send { get; set; }

        //Methods

        public Invoice() { }

        public Invoice(int invoiceID, Order order, DateTime dateOfDelivery, string formOfDelivery, string formOfPayment, bool creditNote, bool send)
        {
            this.InvoiceID = invoiceID;
            this.Order = order;
            this.DateOfDelivery = dateOfDelivery;
            this.FormOfDelivery = formOfDelivery;
            this.FormOfPayment = formOfPayment;
            this.CreditNote = creditNote;
            this.Send = Send;

        }

        public void AddInvoice(Order order, DateTime dateOfDelivery, string formOfDelivery, string formOfPayment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("AddInvoice", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("OrderID", order.OrderID);
                command.Parameters.AddWithValue("DateOfDelivery", dateOfDelivery);
                command.Parameters.AddWithValue("FormOfDelivery", formOfDelivery);
                command.Parameters.AddWithValue("FormOfPayment", formOfPayment);
                command.Parameters.AddWithValue("CreditNota", false);
                command.Parameters.AddWithValue("Send", false);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Invoice> GetInvoices()
        {
            List<Invoice> invoices = new List<Invoice>();
            Order tempOrder = new Order();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowInvoices", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int invoiceID = int.Parse(reader["InvoiceID"].ToString());
                        Order order = tempOrder.GetOrder(int.Parse(reader["OrderID"].ToString()));
                        DateTime dateOfDelivery = DateTime.Parse(reader["DateOfDelivery"].ToString());
                        string formOfDelivery = reader["FormOfDelivery"].ToString();
                        string formOfPayment = reader["FormOfPayment"].ToString();
                        bool creditNota = bool.Parse(reader["CreditNota"].ToString());
                        bool send = bool.Parse(reader["Send"].ToString());
                        invoices.Add(new Invoice(invoiceID, order, dateOfDelivery, formOfDelivery, formOfPayment, creditNota, send));
                    }
                }
            }

            return invoices;
        }

        public Invoice GetInvoice(int invoiceID)
        {
            Invoice invoice = new Invoice();
            Order tempOrder = new Order();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowInvoice", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("InvoiceID", invoiceID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int _invoiceID = int.Parse(reader["InvoiceID"].ToString());
                        Order order = tempOrder.GetOrder(int.Parse(reader["OrderID"].ToString()));
                        DateTime dateOfDelivery = DateTime.Parse(reader["DateOfDelivery"].ToString());
                        string formOfDelivery = reader["FormOfDelivery"].ToString();
                        string formOfPayment = reader["FormOfPayment"].ToString();
                        bool creditNota = bool.Parse(reader["CreditNota"].ToString());
                        bool send = bool.Parse(reader["Send"].ToString());
                        invoice = new Invoice(_invoiceID, order, dateOfDelivery, formOfDelivery, formOfPayment, creditNota, send);
                    }
                }
            }

            return invoice;
        }
    }
}
