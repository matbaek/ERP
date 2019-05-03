using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Customer : DB.Database
    {
        //Property
        public int CustomerID{ get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerTelephone { get; set; }
        public string CustomerZip { get; set; }
        public string Town{ get; set; }

        public Customer() { }

        public Customer(int customerID, string companyName, string contactPerson, string customerAddress, string customerTelephone, string customerZip)
        {
            this.CustomerID = customerID;
            this.CompanyName = companyName;
            this.ContactPerson = contactPerson;
            this.CustomerAddress = customerAddress;
            this.CustomerTelephone = customerTelephone;
            this.CustomerZip = customerZip;
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
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
                        string contactPerson = reader["ContactPerson"].ToString();
                        string customerAddress = reader["CustomerAddress"].ToString();
                        string customerTelephone = reader["CustomerTelephone"].ToString();
                        string customerZip = reader["Zip"].ToString();
                        customers.Add(new Customer(customerID, companyName, contactPerson, customerAddress, customerTelephone, customerZip));
                    }
                }
            }

            return customers;
        }
    }
}
