using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Product
    {
        //Property
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double ProductWeight { get; set; }
        public double ProductPrice { get; set; }
        public double ProductAmount { get; set; }
        public DateTime DateOfPackaging { get; set; }
        public DateTime DateOfExpiration { get; set; }

        public Product() { }

        public Product(int productID, string productName, double productWeight, double productPrice, double productAmount, DateTime dateOfPackaging, DateTime dateOfExpiration)
        {
            this.ProductID = productID;
            this.ProductName = productName;
            this.ProductWeight = productWeight;
            this.ProductPrice = productPrice;
            this.ProductAmount = productAmount;
            this.DateOfPackaging = dateOfPackaging;
            this.DateOfExpiration = dateOfExpiration;

        }

        public void AddProduct(string productName, double productWeight, double productPrice, double productAmount, DateTime dateOfPackaging, DateTime dateOfExpiration)
        {
            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("AddProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductName", productName);
                command.Parameters.AddWithValue("@ProductWeight", productWeight);
                command.Parameters.AddWithValue("@ProductPrice", productPrice);
                command.Parameters.AddWithValue("@ProductAmount", productAmount);
                command.Parameters.AddWithValue("@DateOfPackaging", dateOfPackaging);
                command.Parameters.AddWithValue("@DateOfExpiration", dateOfExpiration);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void EditProduct(int productID, string productName, double productWeight, double productPrice, double productAmount, DateTime dateOfPackaging, DateTime dateOfExpiration)
        {
            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("EditProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", productID);
                command.Parameters.AddWithValue("@ProductName", productName);
                command.Parameters.AddWithValue("@ProductWeight", productWeight);
                command.Parameters.AddWithValue("@ProductPrice", productPrice);
                command.Parameters.AddWithValue("@ProductAmount", productAmount);
                command.Parameters.AddWithValue("@DateOfPackaging", dateOfPackaging);
                command.Parameters.AddWithValue("@DateOfExpiration", dateOfExpiration);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteProduct(int productID)
        {
            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DeleteProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", productID);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowProducts", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int productID = int.Parse(reader["ProductID"].ToString());
                        string productName = reader["ProductName"].ToString();
                        double productWeight = double.Parse(reader["ProductWeight"].ToString());
                        double productPrice = double.Parse(reader["ProductPrice"].ToString());
                        double productAmount = double.Parse(reader["ProductAmount"].ToString());
                        DateTime dateOfPackaging = DateTime.Parse(reader["DateOfPackaging"].ToString());
                        DateTime dateOfExpiration = DateTime.Parse(reader["DateOfExpiration"].ToString());
                        products.Add(new Product(productID, productName, productWeight, productPrice,  productAmount, dateOfPackaging, dateOfExpiration));
                    }
                }
                connection.Close();
            }

            return products;
        }

        public List<Product> GetSpecificProducts(string productName, double productWeight, double productPrice, double productAmount, DateTime dateOfPackaging, DateTime dateOfExpiration)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowSpecificProducts", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductName", productName);
                command.Parameters.AddWithValue("@ProductWeight", productWeight);
                command.Parameters.AddWithValue("@ProductPrice", productPrice);
                command.Parameters.AddWithValue("@ProductAmount", productAmount);
                command.Parameters.AddWithValue("@DateOfPackaging", dateOfPackaging);
                command.Parameters.AddWithValue("@DateOfExpiration", dateOfExpiration);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int _productID = int.Parse(reader["ProductID"].ToString());
                        string _productName = reader["ProductName"].ToString();
                        double _productWeight = double.Parse(reader["ProductWeight"].ToString());
                        double _productPrice = double.Parse(reader["ProductPrice"].ToString());
                        double _productAmount = double.Parse(reader["ProductAmount"].ToString());
                        DateTime _dateOfPackaging = DateTime.Parse(reader["DateOfPackaging"].ToString());
                        DateTime _dateOfExpiration = DateTime.Parse(reader["DateOfExpiration"].ToString());
                        products.Add(new Product(_productID, _productName, _productWeight, _productPrice, _productAmount, _dateOfPackaging, _dateOfExpiration));
                    }
                }
            }

            return products;
        }

        public Product GetProduct(int productID)
        {
            Product product = new Product();

            using (SqlConnection connection = new SqlConnection(Database.Instance.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", productID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int _productID = int.Parse(reader["ProductID"].ToString());
                        string productName = reader["ProductName"].ToString();
                        double productWeight = double.Parse(reader["ProductWeight"].ToString());
                        double productPrice = double.Parse(reader["ProductPrice"].ToString());
                        double productAmount = double.Parse(reader["ProductAmount"].ToString());
                        DateTime dateOfPackaging = DateTime.Parse(reader["DateOfPackaging"].ToString());
                        DateTime dateOfExpiration = DateTime.Parse(reader["DateOfExpiration"].ToString());
                        product = new Product(_productID, productName, productWeight, productPrice, productAmount, dateOfPackaging, dateOfExpiration);
                    }
                }
            }

            return product;
        }
    }
}
