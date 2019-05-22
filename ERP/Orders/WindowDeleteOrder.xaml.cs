using Application;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ERP.Orders
{
    /// <summary>
    /// Interaction logic for WindowDeleteOrder.xaml
    /// </summary>
    public partial class WindowDeleteOrder : Window
    {
        private OrderlineRepository orderlineRepository = new OrderlineRepository();
        private ProductRepository productRepository = new ProductRepository();
        private OrderRepository orderRepository = new OrderRepository();
        private Order order;
        public WindowDeleteOrder(Order order)
        {
            InitializeComponent();
            this.order = order;
            LabelDeleteOrder.Content = $"Er du sikker på at du vil slette ordren '{order.OrderID}'?";
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            List<Orderline> orderlines = orderlineRepository.DisplayOrderlines(order);
            for (int i = 0; i < orderlines.Count; i++)
            {
                Product product = orderlines[i].Product;
                product.ProductAmount += orderlines[i].Amount;
                productRepository.EditProduct(product);
                orderlineRepository.DeleteOrderline(orderlines[i].OrderlineNumber);
            }
            orderRepository.DeleteOrder(order.OrderID);
            this.Close();
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
