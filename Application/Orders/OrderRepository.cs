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

        }

        public void EditOrder(Order order)
        {

        }

        public void DeleteOrder(int orderID)
        {

        }

        public List<Order> DisplayOrders()
        {
            return order.GetOrders();
        }

        public List<Order> DisplaySpecificOrders(Order order)
        {
            return order.GetSpecificOrders(order.CustomerID, order.DateOfPurchase, order.TotalPrice);
        }
        public Order DisplayOrder(int orderID)
        {
            return order.GetOrder(orderID);
        }
    }
}
