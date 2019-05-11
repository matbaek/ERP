using System;
using System.Collections.Generic;
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
