using System;
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
        public int CustomerID { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public double TotalPrice { get; set; }

        //Methods

        public Order() { }

        public Order(int orderID, int customerID, DateTime dateOfPurchase, double totalPrice)
        {
            this.OrderID = orderID;
            this.CustomerID = customerID;
            this.DateOfPurchase = dateOfPurchase;
            this.TotalPrice = totalPrice;

        }

        public void EditOrder(int orderID, int customerID, DateTime dateOfPurchase, double totalPrice)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("EditOrder", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("OrderID", orderID);
                command.Parameters.AddWithValue("CustomerID", customerID);
                command.Parameters.AddWithValue("PurchaseDate", dateOfPurchase);
                command.Parameters.AddWithValue("Price", totalPrice);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();

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
                        int customerID = int.Parse(reader["CustomerID"].ToString());
                        DateTime dateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString());
                        double totalPrice = double.Parse(reader["TotalPrice"].ToString());
                        orders.Add(new Order(orderID, customerID, dateOfPurchase, totalPrice));
                    }
                }
            }

            return orders;
        }

        public List<Order> GetSpecificOrders(int customerID, DateTime dateOfPurchase, double totalPrice)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowSpecificCustomers", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CustomerID", customerID);
                command.Parameters.AddWithValue("@DateOfPurchase", dateOfPurchase);
                command.Parameters.AddWithValue("@TotalPrice", totalPrice);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int _orderID = int.Parse(reader["OrderID"].ToString());
                        int _customerID = int.Parse(reader["CustomerID"].ToString());
                        DateTime _dateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString());
                        double _totalPrice = double.Parse(reader["TotalPrice"].ToString());
                        orders.Add(new Order(_orderID, _customerID, _dateOfPurchase, _totalPrice));
                    }
                }
            }

            return orders;
        }

        public Order GetOrder(int orderID)
        {
            Order order = new Order();

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
                        int _customerID = int.Parse(reader["CustomerID"].ToString());
                        DateTime _dateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString());
                        double _totalPrice = double.Parse(reader["TotalPrice"].ToString());
                        order = new Order(_orderID, _customerID, _dateOfPurchase, _totalPrice);
                    }
                }
            }

            return order;
        }
    }
}
