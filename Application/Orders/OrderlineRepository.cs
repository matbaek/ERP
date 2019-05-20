using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application
{
    public class OrderlineRepository
    {
        private Orderline orderline = new Orderline();

        //Methods
        public void AddOrderline(Orderline orderline)
        {
            orderline.AddOrderline(orderline.OrderlineNumber, orderline.OrderID, orderline.Product, orderline.Amount);
        }

        public void EditOrderline(Orderline orderline)
        {
            orderline.EditOrderline(orderline.OrderlineNumber, orderline.OrderID, orderline.Product, orderline.Amount);
        }

        public void DeleteOrderline(int orderlineID)
        {

        }

        public List<Orderline> DisplayOrderlines(Order order)
        {
            return orderline.GetOrderlines(order.OrderID);
        }

    public Orderline DisplayOrderline(int orderlineNumber)
        {
            return orderline.GetOrderline(orderlineNumber);
        }
    }
}
