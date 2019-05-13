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
        private List<Orderline> orderlines = new List<Orderline>();
        private Order order = new Order();
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
        }

        void WindowProductAmount_eventSendProductAmount(double amount)
        {
            products[products.Count - 1].ProductAmount = amount;
            UpdateTotalPrice();
            Update();
        }

        void WindowPickCustomer_eventSendList(Domain.Customer items)
        {
            TextBoxCustomer.Text = items.CompanyName;
            order.CustomerID = items.CustomerID;
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
                totalPrice += products[i].ProductAmount * products[i].ProductPrice;
            }
            TextBoxTotalPrice.Text = totalPrice.ToString();
        }

        private void Update() 
        {
            CollectionViewSource.GetDefaultView(Orderlines.ItemsSource).Refresh(); 
        }
    }
}