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
        public int ProductID { get; set; }
        public int Amount { get; set; }


        public Orderline(int orderlineNumber, int orderID, int productID, int amount)
        {
            this.OrderlineNumber = orderlineNumber;
            this.OrderID = orderID;
            this.ProductID = productID;
            this.Amount = amount;
        }
        public Orderline()
        {

        }

        public void AddOrderline(int orderlineNumber, int orderID, int productID, int amount)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("AddOrderline", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("OrderID", orderID);
                command.Parameters.AddWithValue("ProductID", productID);
                command.Parameters.AddWithValue("ProductAmount", amount);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void EditOrderline(int orderlineNumber, int orderID, int productID, int amount)
        {

        }

        public Orderline GetOrderline(int orderlineNumber)
        {
            return null;
        }

        public List<Orderline> GetOrderlines()
        {
            return null;
        }

        public List<Orderline> GetSpecificOrderlines(int orderlineNumber, int orderID, int productID, int amount)
        {
            return null;
        }

        public void DeleteOrderline(int orderlineNumber)
        {

        }
    }  
}
