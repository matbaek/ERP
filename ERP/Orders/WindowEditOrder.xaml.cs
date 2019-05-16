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
    /// Interaction logic for WindowEditOrder.xaml
    /// </summary>
    public partial class WindowEditOrder : Window
    {
        private OrderRepository orderRepository = new OrderRepository();
        private ProductRepository productRepository = new ProductRepository();
        private List<Orderline> orderlines = new List<Orderline>();
        private Order order = new Order();
        private Orderline orderline = new Orderline();
        private OrderlineRepository orderlineRepository = new OrderlineRepository();
        private List<Object> tempList = new List<Object>();
        public WindowEditOrder(Order order)
        {
            InitializeComponent();
            TextBoxCustomer.Text = order.Customer.CompanyName;
            TextBoxTotalPrice.Text = order.TotalPrice.ToString();
            TextBoxDateOfPurchase.Text = order.DateOfPurchase.ToString();
            WindowPickProduct.eventSendProduct += WindowPickProduct_eventSendProduct;
            WindowPickCustomer.eventSendList += WindowPickCustomer_eventSendList;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            WindowShowDialog wsd = new WindowShowDialog();

            if (double.TryParse(TextBoxTotalPrice.Text, out double resultTotalPrice) && TextBoxDateOfPurchase.SelectedDate != null)
            {
                order.TotalPrice = double.Parse(TextBoxTotalPrice.Text);
                order.DateOfPurchase = DateTime.Parse(TextBoxDateOfPurchase.Text);
                orderRepository.AddOrder(order);

                for (int i = 0; i < orderlines.Count; i++)
                {
                    orderline.Amount = orderlines[i].Amount;
                    orderline.ProductID = orderlines[i].ProductID;
                    orderline.OrderID = order.GetLastOrderID();
                    orderlineRepository.AddOrderline(orderline);
                }

                wsd.LabelShowDialog.Content = "Ordren blev tilføjet";
                wsd.ShowDialog();

                this.Close();
            }
            else
            {
                wsd.LabelShowDialog.Content = "Der var en fejl man";
                wsd.ShowDialog();
            }
        }

        private void ButtonAdd_Product_Click(object sender, RoutedEventArgs e)
        {
            WindowPickProduct wpp = new WindowPickProduct();
            wpp.ShowDialog();
        }

        void WindowPickProduct_eventSendProduct(Product item, double amount)
        {
            orderlines.Add(new Orderline(0, 0, item.ProductID, amount));

            tempList.Add(new { ProductName = item.ProductName, Price = item.ProductPrice.ToString(), Amount = amount.ToString() });
            Order.ItemsSource = tempList;

            Update();
        }

        void WindowPickCustomer_eventSendList(Domain.Customer items)
        {
            TextBoxCustomer.Text = items.CompanyName;
            order.Customer = items;
        }

        private void TextBoxCustomer_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WindowPickCustomer wpc = new WindowPickCustomer();
            wpc.ShowDialog();
        }

        private void UpdateTotalPrice()
        {
            double totalPrice = 0;
            for (int i = 0; i < orderlines.Count; i++)
            {
                totalPrice += productRepository.DisplayProduct(orderlines[i].ProductID).ProductPrice * orderlines[i].Amount;
            }
            TextBoxTotalPrice.Text = totalPrice.ToString();
        }

        private void Update()
        {
            CollectionViewSource.GetDefaultView(Order.ItemsSource).Refresh();
            UpdateTotalPrice();
        }

        private void Orderlines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBoxCustomer.Text = order.Customer.CompanyName;
            TextBoxTotalPrice.Text = order.TotalPrice.ToString();
            TextBoxDateOfPurchase.Text = order.DateOfPurchase.ToString(); 
        }

        private void Orderlines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Order)(Order.SelectedItem) != null)
            {
                order = ((Order)Order.SelectedItem);
            }
        }
    }
}
