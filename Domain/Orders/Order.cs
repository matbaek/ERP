﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Order : DB.Database
    {
        //Property
        public int OrderID { get; set; }
        public Customer Customer { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public double TotalPrice { get; set; }

        //Methods

        public Order() { }

        public Order(int orderID, Customer customer, DateTime dateOfPurchase, double totalPrice)
        {
            this.OrderID = orderID;
            this.Customer = customer;
            this.DateOfPurchase = dateOfPurchase;
            this.TotalPrice = totalPrice;

        }

        public void AddOrder(Customer customer, DateTime dateOfPurchase, double totalPrice)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("AddOrder", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("CustomerID", customer.CustomerID);
                command.Parameters.AddWithValue("DateOfPurchase", dateOfPurchase);
                command.Parameters.AddWithValue("TotalPrice", totalPrice);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void EditOrder(int orderID, Customer customer, DateTime dateOfPurchase, double totalPrice)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("EditOrder", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("OrderID", orderID);
                command.Parameters.AddWithValue("CustomerID", customer.CustomerID);
                command.Parameters.AddWithValue("DateOfPurchase", dateOfPurchase);
                command.Parameters.AddWithValue("TotalPrice", totalPrice);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteOrder(int orderID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DeleteOrder", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderID", orderID);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            Customer tempCustomer = new Customer();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowOrders", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int orderID = int.Parse(reader["OrderID"].ToString());
                        Customer customer = tempCustomer.GetCustomer(int.Parse(reader["CustomerID"].ToString()));
                        DateTime dateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString());
                        double totalPrice = double.Parse(reader["TotalPrice"].ToString());
                        orders.Add(new Order(orderID, customer, dateOfPurchase, totalPrice));
                    }
                }
            }

            return orders;
        }

        public List<Order> GetSpecificOrders(int orderID, Customer customer, DateTime dateOfPurchase, double totalPrice)
        {
            List<Order> orders = new List<Order>();
            Customer tempCustomer = new Customer();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowSpecificOrders", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@OrderID", orderID);
                command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                command.Parameters.AddWithValue("@DateOfPurchase", dateOfPurchase);
                command.Parameters.AddWithValue("@TotalPrice", totalPrice);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int _orderID = int.Parse(reader["OrderID"].ToString());
                        Customer _customer = tempCustomer.GetCustomer(int.Parse(reader["CustomerID"].ToString()));
                        DateTime _dateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString());
                        double _totalPrice = double.Parse(reader["TotalPrice"].ToString());
                        orders.Add(new Order(_orderID, _customer, _dateOfPurchase, _totalPrice));
                    }
                }
            }

            return orders;
        }

        public Order GetOrder(int orderID)
        {
            Order order = new Order();
            Customer tempCustomer = new Customer();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowOrder", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderID", orderID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int _orderID = int.Parse(reader["OrderID"].ToString());
                        Customer _customer = tempCustomer.GetCustomer(int.Parse(reader["CustomerID"].ToString()));
                        DateTime _dateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString());
                        double _totalPrice = double.Parse(reader["TotalPrice"].ToString());
                        order = new Order(_orderID, _customer, _dateOfPurchase, _totalPrice);
                    }
                }
            }

            return order;
        }

        public int GetLastOrderID()
        {
            int newOrderID = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetLastOrderID", connection);
                command.CommandType = CommandType.StoredProcedure;

                newOrderID = Convert.ToInt32(command.ExecuteScalar());   
            }

            return newOrderID;
        }
    }
}
