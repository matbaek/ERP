using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class OrderRepository
    {
        private Order order = new Order();

        //Methods
        public void AddOrder(Order order)
        {
            order.AddOrder(order.Customer, order.DateOfPurchase, order.TotalPrice, order.Active);
        }

        public void EditOrder(Order order)
        {
            order.EditOrder(order.OrderID, order.Customer, order.DateOfPurchase, order.TotalPrice, order.Active);
        }

        public void DeleteOrder(int orderID)
        {
            order.DeleteOrder(orderID);
        }

        public List<Order> DisplayOrders()
        {
            return order.GetOrders();
        }

        public List<Order> DisplayNonActiveOrders(bool status)
        {
            return order.GetNonActiveOrders(status);
        }

        public List<Order> DisplaySpecificOrders(Order order)
        {
            return order.GetSpecificOrders(order.OrderID, order.Customer, order.DateOfPurchase, order.TotalPrice, order.Active);
        }

        public Order DisplayOrder(int orderID)
        {
            return order.GetOrder(orderID);
        }

        public int DisplayLastOrderID()
        {
            return order.GetLastOrderID();
        }
    }
}
