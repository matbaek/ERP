using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RawProduct
    {
        //Property
        public int RawProductID { get; set; }
        public string RawProductName { get; set; }
        public double RawProductWeight { get; set; }
        public DateTime DateOfPurchase { get; set; }

        public RawProduct() { }

        public RawProduct(int rawProductID, string rawProductName, double rawProductWeight, DateTime dateOfPurchase)
        {
            this.RawProductID = rawProductID;
            this.RawProductName = rawProductName;
            this.RawProductWeight = rawProductWeight;
            this.DateOfPurchase = dateOfPurchase;
        }

        public void AddRawProduct(string rawProductName, double rawProductWeight, DateTime dateOfPurchase)
        {
            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("AddRawProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RawProductName", rawProductName);
                command.Parameters.AddWithValue("@RawProductWeight", rawProductWeight);
                command.Parameters.AddWithValue("@DateOfPurchase", dateOfPurchase);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void EditRawProduct(int rawProductID, string rawProductName, double rawProductWeight, DateTime dateOfPurchase)
        {
            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("EditRawProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RawProductID", rawProductID);
                command.Parameters.AddWithValue("@RawProductName", rawProductName);
                command.Parameters.AddWithValue("@RawProductWeight", rawProductWeight);
                command.Parameters.AddWithValue("@DateOfPurchase", dateOfPurchase);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteRawProduct(int rawProductID)
        {
            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DeleteRawProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RawProductID", rawProductID);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<RawProduct> GetRawProducts()
        {
            List<RawProduct> rawProducts = new List<RawProduct>();

            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowRawProducts", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int rawProductID = int.Parse(reader["RawProductID"].ToString());
                        string rawProductName = reader["RawProductName"].ToString();
                        double rawProductWeight = double.Parse(reader["RawProductWeight"].ToString());
                        DateTime dateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString());
                        rawProducts.Add(new RawProduct(rawProductID, rawProductName, rawProductWeight, dateOfPurchase));
                    }
                }
                connection.Close();
            }

            return rawProducts;
        }

        public List<RawProduct> GetSpecificRawProducts(string rawProductName, double rawProductWeight, DateTime dateOfPurchase)
        {
            List<RawProduct> rawProducts = new List<RawProduct>();

            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowSpecificRawProducts", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RawProductName", rawProductName);
                command.Parameters.AddWithValue("@RawProductWeight", rawProductWeight);
                command.Parameters.AddWithValue("@DateOfPurchase", dateOfPurchase);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int _rawProductID = int.Parse(reader["RawProductID"].ToString());
                        string _rawProductName = reader["RawProductName"].ToString();
                        double _rawProductWeight = double.Parse(reader["RawProductWeight"].ToString());
                        DateTime _dateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString());
                        rawProducts.Add(new RawProduct(_rawProductID, _rawProductName, _rawProductWeight, _dateOfPurchase));
                    }
                }
            }

            return rawProducts;
        }

        public RawProduct GetRawProduct(int rawProductID)
        {
            RawProduct rawProduct = new RawProduct();

            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowRawProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RawProductID", rawProductID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int _rawProductID = int.Parse(reader["RawProductID"].ToString());
                        string rawProductName = reader["RawProductName"].ToString();
                        double rawProductWeight = double.Parse(reader["RawProductWeight"].ToString());
                        DateTime dateOfPurchase = DateTime.Parse(reader["DateOfPurchase"].ToString());
                        rawProduct = new RawProduct(_rawProductID, rawProductName, rawProductWeight, dateOfPurchase);
                    }
                }
            }

            return rawProduct;
        }
    }
}
