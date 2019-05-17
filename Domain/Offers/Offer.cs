using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Offers
{
    public class Offer : DB.Database
    {
        //Property
        public int OfferID { get; set; }
        public Customer Customer { get; set; }
        public DateTime DateOfOffer { get; set; }
        public DateTime DateOfOfferExpiration { get; set; }
        public double TotalPrice { get; set; }

        //Methods

        public Offer() { }

        public Offer(int offerID, Customer customer, DateTime dateOfOffer, DateTime dateOfOfferExpiration, double totalPrice)
        {
            this.OfferID = offerID;
            this.Customer = customer;
            this.DateOfOffer = dateOfOffer;
            this.TotalPrice = totalPrice;

        }

        public void AddOffer(Customer customer, DateTime dateOfOffer, DateTime dateOfOfferExpiration, double totalPrice)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("AddOffer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("CustomerID", customer.CustomerID);
                command.Parameters.AddWithValue("DateOfOffer", dateOfOffer);
                command.Parameters.AddWithValue("DateOfOfferExpiration", dateOfOfferExpiration);
                command.Parameters.AddWithValue("TotalPrice", totalPrice);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void EditOffer(int offerID, Customer customer, DateTime dateOfOffer, DateTime dateOfOfferExpiration, double totalPrice)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("EditOffer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("OfferID", offerID);
                command.Parameters.AddWithValue("CustomerID", customer.CustomerID);
                command.Parameters.AddWithValue("DateOfOffer", dateOfOffer);
                command.Parameters.AddWithValue("DateOfOfferExpiration", dateOfOfferExpiration);
                command.Parameters.AddWithValue("TotalPrice", totalPrice);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteOffer(int offerID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DeleteOffer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OfferID", offerID);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Offer> GetOffers()
        {
            List<Offer> offers = new List<Offer>();
            Customer tempCustomer = new Customer();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowOffers", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int offerID = int.Parse(reader["OfferID"].ToString());
                        Customer customer = tempCustomer.GetCustomer(int.Parse(reader["CustomerID"].ToString()));
                        DateTime dateOfOffer = DateTime.Parse(reader["DateOfOffer"].ToString());
                        DateTime dateOfOfferExpiration = DateTime.Parse(reader["DateOfOfferExpiration"].ToString());
                        double totalPrice = double.Parse(reader["TotalPrice"].ToString());
                        offers.Add(new Offer(offerID, customer, dateOfOffer, dateOfOfferExpiration, totalPrice));
                    }
                }
            }

            return offers;
        }

        public List<Offer> GetSpecificOffers(int offerID, Customer customer, DateTime dateOfOffer, DateTime dateOfOfferExpiration, double totalPrice)
        {
            List<Offer> offers = new List<Offer>();
            Customer tempCustomer = new Customer();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowSpecificOffers", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@OfferID", offerID);
                command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                command.Parameters.AddWithValue("@DateOfOffer", dateOfOffer);
                command.Parameters.AddWithValue("@DateOfOfferExpiration", dateOfOfferExpiration);
                command.Parameters.AddWithValue("@TotalPrice", totalPrice);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int _offerID = int.Parse(reader["OfferID"].ToString());
                        Customer _customer = tempCustomer.GetCustomer(int.Parse(reader["CustomerID"].ToString()));
                        DateTime _dateOfOffer = DateTime.Parse(reader["DateOfOffer"].ToString());
                        DateTime _dateOfOfferExpiration = DateTime.Parse(reader["DateOfOfferExpiration"].ToString());
                        double _totalPrice = double.Parse(reader["TotalPrice"].ToString());
                        offers.Add(new Offer(_offerID, _customer, _dateOfOffer, _dateOfOfferExpiration, _totalPrice));
                    }
                }
            }

            return offers;
        }

        public Offer GetOffer(int offerID)
        {
            Offer offer = new Offer();
            Customer tempCustomer = new Customer();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowOffer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OfferID", offerID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int _offerID = int.Parse(reader["OfferID"].ToString());
                        Customer _customer = tempCustomer.GetCustomer(int.Parse(reader["CustomerID"].ToString()));
                        DateTime _dateOfOffer = DateTime.Parse(reader["DateOfOffer"].ToString());
                        DateTime _dateOfOfferExpiration = DateTime.Parse(reader["DateOfOfferExpiration"].ToString());
                        double _totalPrice = double.Parse(reader["TotalPrice"].ToString());
                        offer = new Offer(_offerID, _customer, _dateOfOffer, _dateOfOfferExpiration, _totalPrice);
                    }
                }
            }

            return offer;
        }

        public int GetLastOfferID()
        {
            int newOfferID = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetLastOfferID", connection);
                command.CommandType = CommandType.StoredProcedure;

                newOfferID = Convert.ToInt32(command.ExecuteScalar());
            }

            return newOfferID;
        }
    }
}

