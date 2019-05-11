using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Application;
using Domain;

namespace ERP.Orders
{
    /// <summary>
    /// Interaction logic for WindowAddOrder.xaml
    /// </summary>
    public partial class WindowAddOrder : Window
    {
        private OrderRepository orderRepository = new OrderRepository();
        private List<Product> products = new List<Product>();
        public WindowAddOrder()
        {
            InitializeComponent();
            WindowPickProduct.eventSendProduct += WindowPickProduct_eventSendProduct;
            WindowPickCustomer.eventSendList += WindowPickCustomer_eventSendList;
            WindowProductAmount.eventSendProductAmount += WindowProductAmount_eventSendProductAmount;
            UpdateTotalPrice();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            WindowShowDialog wsd = new WindowShowDialog();

            if (double.TryParse(TextBoxTotalPrice.Text, out double resultTotalPrice) && TextBoxDateOfPurchase.SelectedDate != null)
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

        private void ButtonAdd_Product_Click(object sender, RoutedEventArgs e)
        {
            WindowPickProduct wpp = new WindowPickProduct();
            wpp.ShowDialog();

        }

        void WindowPickProduct_eventSendProduct(Product item)
        {
            products.Add(item);
            Orderlines.ItemsSource = products;
            CollectionViewSource.GetDefaultView(Orderlines.ItemsSource).Refresh();
            UpdateTotalPrice();

        }

        void WindowProductAmount_eventSendProductAmount(double amount)
        {
            //Noget med at den de override product amount her???
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

        private void UpdateTotalPrice()
        {
            double totalPrice = 0;
            for (int i = 0; i < products.Count; i++)
            {
                totalPrice += products[i].ProductPrice;
            }
            TextBoxTotalPrice.Text = totalPrice.ToString();
        }
    }
}