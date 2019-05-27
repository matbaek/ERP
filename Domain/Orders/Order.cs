using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Order
    {
        //Property
        public int OrderID { get; set; }
        public Customer Customer { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public double TotalPrice { get; set; }
        public bool Active { get; set; }

        //Methods

        public Order() { }

        public Order(int orderID, Customer customer, DateTime dateOfPurchase, double totalPrice, bool active)
        {
            this.OrderID = orderID;
            this.Customer = customer;
            this.DateOfPurchase = dateOfPurchase;
            this.TotalPrice = totalPrice;
            this.Active = active;

        }

        public void AddOrder(Customer customer, DateTime dateOfPurchase, double totalPrice, bool active)
        {
            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("AddOrder", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("CustomerID", customer.CustomerID);
                command.Parameters.AddWithValue("DateOfPurchase", dateOfPurchase);
                command.Parameters.AddWithValue("TotalPrice", totalPrice);
                command.Parameters.AddWithValue("Active", active);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void EditOrder(int orderID, Customer customer, DateTime dateOfPurchase, double totalPrice, bool active)
        {
            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("EditOrder", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("OrderID", orderID);
                command.Parameters.AddWithValue("CustomerID", customer.CustomerID);
                command.Parameters.AddWithValue("DateOfPurchase", dateOfPurchase);
                command.Parameters.AddWithValue("TotalPrice", totalPrice);
                command.Parameters.AddWithValue("Active", active);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteOrder(int orderID)
        {
            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
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

            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
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
                        bool active = bool.Parse(reader["Active"].ToString());
                        orders.Add(new Order(orderID, customer, dateOfPurchase, totalPrice, active));
                    }
                }
            }

            return orders;
        }

        public List<Order> GetNonActiveOrders(bool status)
        {
            List<Order> orders = new List<Order>();
            Customer tempCustomer = new Customer();

            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowNonActiveOrders", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Active", status);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int orderID = int.Parse(reader["OrderID"].ToString());
                        Customer customer = tempCustomer.GetCustomer(int.Parse(reader["CustomerID"].ToString()));
                        DateTime dateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString());
                        double totalPrice = double.Parse(reader["TotalPrice"].ToString());
                        bool active = bool.Parse(reader["Active"].ToString());
                        orders.Add(new Order(orderID, customer, dateOfPurchase, totalPrice, active));
                    }
                }
            }

            return orders;
        }

        public List<Order> GetSpecificOrders(int orderID, Customer customer, DateTime dateOfPurchase, double totalPrice, bool active)
        {
            List<Order> orders = new List<Order>();
            Customer tempCustomer = new Customer();

            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowSpecificOrders", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@OrderID", orderID);
                command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                command.Parameters.AddWithValue("@DateOfPurchase", dateOfPurchase);
                command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                command.Parameters.AddWithValue("Active", active);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int _orderID = int.Parse(reader["OrderID"].ToString());
                        Customer _customer = tempCustomer.GetCustomer(int.Parse(reader["CustomerID"].ToString()));
                        DateTime _dateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString());
                        double _totalPrice = double.Parse(reader["TotalPrice"].ToString());
                        active = bool.Parse(reader["Active"].ToString());
                        orders.Add(new Order(orderID, customer, dateOfPurchase, totalPrice, active));
                    }
                }
            }

            return orders;
        }

        public Order GetOrder(int orderID)
        {
            Order order = new Order();
            Customer tempCustomer = new Customer();

            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
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
                        Customer customer = tempCustomer.GetCustomer(int.Parse(reader["CustomerID"].ToString()));
                        DateTime dateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString());
                        double totalPrice = double.Parse(reader["TotalPrice"].ToString());
                        bool active = bool.Parse(reader["Active"].ToString());
                        order = new Order(_orderID, customer, dateOfPurchase, totalPrice, active);
                    }
                }
            }

            return order;
        }

        public int GetLastOrderID()
        {
            int newOrderID = 0;

            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
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
