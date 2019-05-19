using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain
{
    public class Orderline : DB.Database
    {
        public int OrderlineNumber { get; set; }
        public int OrderID { get; set; }
        public int OfferID{ get; set; }
        public Product Product { get; set; }
        public double Amount { get; set; }


        public Orderline(int orderlineNumber, int orderID, int offerID, Product product, double amount)
        {
            this.OrderlineNumber = orderlineNumber;
            this.OrderID = orderID;
            this.OfferID = offerID;
            this.Product = product;
            this.Amount = amount;
        }
        public Orderline()
        {

        }

        public void AddOrderline(int orderlineNumber, int orderID, int offerID, Product product, double amount)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("AddOrderlines", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("OrderID", orderID);
                command.Parameters.AddWithValue("OfferID", offerID);
                command.Parameters.AddWithValue("ProductID", product.ProductID);
                command.Parameters.AddWithValue("Amount", amount);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void EditOrderline(int orderlineNumber, int orderID, int offerID, Product product, double amount)
        {

        }

        public Orderline GetOrderline(int orderlineNumber)
        {
            return null;
        }

        public List<Orderline> GetOrderlines(int orderID, int offerID)
        {
            List<Orderline> orderlines = new List<Orderline>();
            Product tempProduct = new Product();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ShowOrderLines", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderID", orderID);
                command.Parameters.AddWithValue("@OfferID", offerID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int orderlineID = int.Parse(reader["LineID"].ToString());
                        int _orderID = int.Parse(reader["OrderID"].ToString());
                        int _offerID = int.Parse(reader["OfferID"].ToString());
                        Product product = tempProduct.GetProduct(int.Parse(reader["ProductID"].ToString()));
                        double amount = double.Parse(reader["Amount"].ToString());
                        orderlines.Add(new Orderline(orderlineID, _orderID, _offerID, product, amount));
                    }
                }
            }

            return orderlines;
        }

        public void DeleteOrderline(int orderlineNumber)
        {

        }
    }  
}
