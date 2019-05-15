﻿using System;
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
        private ProductRepository productRepository = new ProductRepository();
        private List<Orderline> orderlines = new List<Orderline>();
        private Order order = new Order();
        private Orderline orderline = new Orderline();
        private OrderlineRepository orderlineRepository = new OrderlineRepository();
        private List<Object> tempList = new List<Object>();
        public WindowAddOrder()
        {
            InitializeComponent();
            WindowPickProduct.eventSendProduct += WindowPickProduct_eventSendProduct;
            WindowPickCustomer.eventSendList += WindowPickCustomer_eventSendList;
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
            Orderlines.ItemsSource = tempList;

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
            for (int i = 0; i < orderlines.Count; i++)
            {
                totalPrice += productRepository.DisplayProduct(orderlines[i].ProductID).ProductPrice * orderlines[i].Amount;
            }
            TextBoxTotalPrice.Text = totalPrice.ToString();
        }

        private void Update() 
        {
            CollectionViewSource.GetDefaultView(Orderlines.ItemsSource).Refresh();
            UpdateTotalPrice();
        }
    }
}