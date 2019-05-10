using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Application;
using Domain;

namespace ERP.Orders
{
    /// <summary>
    /// Interaction logic for WindowAddOrder.xaml
    /// </summary>
    public partial class WindowAddOrder : Window
    {
        private ProductRepository productRepository = new ProductRepository();
        private OrderRepository orderRepository = new OrderRepository();
        private List<Product> products = new List<Product>();
        public WindowAddOrder()
        {
            InitializeComponent();
            products.Add(new Product(99, "Danikkelias", 120, 999, 23, DateTime.Now, DateTime.Now));
            WindowPickProduct.eventSendList += WindowPickProduct_eventSendList;
            WindowPickCustomer.eventSendList += WindowPickCustomer_eventSendList;
           
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            WindowShowDialog wsd = new WindowShowDialog();

            if(double.TryParse(TextBoxTotalPrice.Text, out double resultTotalPrice) && TextBoxDateOfPurchase.SelectedDate != null)
            {
                order.TotalPrice = double.Parse(TextBoxTotalPrice.Text);
                order.DateOfPurchase = DateTime.Parse(TextBoxDateOfPurchase.Text);
                orderRepository.AddOrder(order);

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

        private void TextBoxWeight_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonAdd_Product_Click(object sender, RoutedEventArgs e)
        {
            WindowPickProduct wpp = new WindowPickProduct();
            wpp.ShowDialog();

        }

        void WindowPickProduct_eventSendList(Product items)
        {
            products.Add(items);
            Orderlines.ItemsSource = products;
        }

        void WindowPickCustomer_eventSendList(Domain.Customer items)
        {
            TextBoxCustomer.Text = items.CompanyName;
        }

        private void TextBoxCustomer_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WindowPickCustomer wpc = new WindowPickCustomer();
            wpc.ShowDialog();
        }
    }
}
