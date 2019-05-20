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
        private List<Orderline> orderlines = new List<Orderline>();
        private Order order = new Order();
        private Orderline orderline = new Orderline();
        private OrderlineRepository orderlineRepository = new OrderlineRepository();
        private Domain.Customer customer = new Domain.Customer();
        private ProductRepository productRepository = new ProductRepository();
        private Product product = new Product();
        public WindowEditOrder(Order order)
        {
            InitializeComponent();
            WindowProductAmount.eventSendProductAmount += WindowProductAmount_eventSendProductAmount;

            this.order = order;
            TextBoxCustomer.Text = order.Customer.CompanyName;
            TextBoxTotalPrice.Text = order.TotalPrice.ToString();
            TextBoxDateOfPurchase.Text = order.DateOfPurchase.ToString();
            customer = order.Customer;
            if (order.Active == true)
            {
                RadioButtonIsOrder.IsChecked = true;
                RadioButtonIsOffer.IsEnabled = false;
            }
            else
            {
                RadioButtonIsOffer.IsChecked = true;
                Orderlines.IsEnabled = true;
            }

            Update();

            WindowPickCustomer.eventSendList += WindowPickCustomer_eventSendList;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            WindowShowDialog wsd = new WindowShowDialog();

            if (double.TryParse(TextBoxTotalPrice.Text, out double resultTotalPrice) && TextBoxDateOfPurchase.SelectedDate != null)
            {
                order.TotalPrice = double.Parse(TextBoxTotalPrice.Text);
                order.DateOfPurchase = DateTime.Parse(TextBoxDateOfPurchase.Text);
                if (RadioButtonIsOrder.IsChecked == true)
                {
                    order.Active = true;
                }
                else if (RadioButtonIsOffer.IsChecked == true)
                {
                    order.Active = false;
                }
                orderRepository.EditOrder(order);

                for (int i = 0; i < orderlines.Count; i++)
                {
                    if (order.Active == true)
                    {
                        product = orderline.Product;
                        product.ProductAmount -= orderline.Amount;
                        productRepository.EditProduct(product);
                    }

                }

                wsd.LabelShowDialog.Content = "Ordren blev redigeret";
                wsd.ShowDialog();

                this.Close();
            }
            else
            {
                wsd.LabelShowDialog.Content = "Der var en fejl man";
                wsd.ShowDialog();
            }
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

        private void Update()
        {
            orderlines = orderlineRepository.DisplayOrderlines(order);
            Orderlines.ItemsSource = orderlines;
            CollectionViewSource.GetDefaultView(Orderlines.ItemsSource).Refresh();
        }
        
        private void Orderlines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((Orderline)Orderlines.SelectedItem) != null)
            {
                orderline = ((Orderline)Orderlines.SelectedItem);
                product = orderline.Product;
            }
        }

        private void Orderlines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WindowProductAmount wpa = new WindowProductAmount(product);
            wpa.ShowDialog();
            Update();
        }

        void WindowProductAmount_eventSendProductAmount(double amount, bool sendProduct)
        {
            orderline = new Orderline(orderline.OrderlineNumber, orderline.OrderID, orderline.Product, amount);
            orderlineRepository.EditOrderline(orderline);
        }
    }
}
