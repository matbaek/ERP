using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Customer
    {
        //Property
        public int CustomerID{ get; set; }
        public string CompanyName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerTelephone { get; set; }
        public string CustomerZip { get; set; }
        public string CustomerTown { get; set; }
        public string CustomerEmail { get; set; }

        public Customer() { }

        public Customer(int customerID, string companyName, string customerAddress, string customerTelephone, string customerZip, string customerTown, string customerEmail)
        {
            this.CustomerID = customerID;
            this.CompanyName = companyName;
            this.CustomerAddress = customerAddress;
            this.CustomerTelephone = customerTelephone;
            this.CustomerZip = customerZip;
            this.CustomerTown = customerTown;
            this.CustomerEmail = customerEmail;
        }

        public void AddCustomer(string companyName, string customerAddress, string customerTelephone, string customerZip, string customerTown, string customerEmail)
        {
            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("AddCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CompanyName", companyName);
                command.Parameters.AddWithValue("@CustomerAddress", customerAddress);
                command.Parameters.AddWithValue("@CustomerTelephone", customerTelephone);
                command.Parameters.AddWithValue("@CustomerZip", customerZip);
                command.Parameters.AddWithValue("@CustomerTown", customerTown);
                command.Parameters.AddWithValue("@Email", customerEmail);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void EditCustomer(int customerID, string companyName, string customerAddress, string customerTelephone, string customerZip, string customerTown, string customerEmail)
        {
            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("EditCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CustomerID", customerID);
                command.Parameters.AddWithValue("@CompanyName", companyName);
                command.Parameters.AddWithValue("@CustomerAddress", customerAddress);
                command.Parameters.AddWithValue("@CustomerTelephone", customerTelephone);
                command.Parameters.AddWithValue("@CustomerZip", customerZip);
                command.Parameters.AddWithValue("@CustomerTown", customerTown);
                command.Parameters.AddWithValue("@Email", customerEmail);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteCustomer(int customerID)
        {
            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DeleteCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CustomerID", customerID);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowCustomers", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int customerID = int.Parse(reader["CustomerID"].ToString());
                        string companyName = reader["CompanyName"].ToString();
                        string customerAddress = reader["CustomerAddress"].ToString();
                        string customerTelephone = reader["CustomerTelephone"].ToString();
                        string customerZip = reader["CustomerZip"].ToString();
                        string customerTown = reader["CustomerTown"].ToString();
                        string customerEmail = reader["Email"].ToString();
                        customers.Add(new Customer(customerID, companyName, customerAddress, customerTelephone, customerZip, customerTown, customerEmail));
                    }
                }
            }

            return customers;
        }

        public List<Customer> GetSpecificCustomers(string companyName, string customerAddress, string customerTelephone, string customerZip, string customerTown, string customerEmail)
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowSpecificCustomers", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CompanyName", companyName);
                command.Parameters.AddWithValue("@CustomerAddress", customerAddress);
                command.Parameters.AddWithValue("@CustomerTelephone", customerTelephone);
                command.Parameters.AddWithValue("@CustomerZip", customerZip);
                command.Parameters.AddWithValue("@CustomerTown", customerTown);
                command.Parameters.AddWithValue("@Email", customerEmail);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int _customerID = int.Parse(reader["CustomerID"].ToString());
                        string _companyName = reader["CompanyName"].ToString();
                        string _customerAddress = reader["CustomerAddress"].ToString();
                        string _customerTelephone = reader["CustomerTelephone"].ToString();
                        string _customerZip = reader["CustomerZip"].ToString();
                        string _customerTown = reader["CustomerTown"].ToString();
                        string _customerEmail = reader["Email"].ToString();
                        customers.Add(new Customer(_customerID, _companyName, _customerAddress, _customerTelephone, _customerZip, _customerTown, _customerEmail));
                    }
                }
            }

            return customers;
        }

        public Customer GetCustomer(int customerID)
        {
            Customer customer = new Customer();

            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CustomerID", customerID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int _customerID = int.Parse(reader["CustomerID"].ToString());
                        string _companyName = reader["CompanyName"].ToString();
                        string _customerAddress = reader["CustomerAddress"].ToString();
                        string _customerTelephone = reader["CustomerTelephone"].ToString();
                        string _customerZip = reader["CustomerZip"].ToString();
                        string _customerTown = reader["CustomerTown"].ToString();
                        string _customerEmail = reader["Email"].ToString();
                        customer = new Customer(_customerID, _companyName, _customerAddress, _customerTelephone, _customerZip, _customerTown, _customerEmail);
                    }
                }
            }

            return customer;
        }
    }
}

