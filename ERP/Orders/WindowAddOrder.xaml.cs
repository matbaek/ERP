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
        private Order order = new Order();
        private List<Order> nonActiveOrders = new List<Order>();
        private Orderline orderline = new Orderline();
        private List<Orderline> orderlines = new List<Orderline>();
        private OrderlineRepository orderlineRepository = new OrderlineRepository();
        private ProductRepository productRepository = new ProductRepository();
        private Product product = new Product();
        public WindowAddOrder()
        {
            InitializeComponent();
            WindowPickProduct.eventSendProduct += WindowPickProduct_eventSendProduct;
            WindowPickCustomer.eventSendList += WindowPickCustomer_eventSendList;
            
            nonActiveOrders.AddRange(orderRepository.DisplayNonActiveOrders());
            ComboBoxOffers.Items.Add("");
            for (int i = 0; i < nonActiveOrders.Count; i++)
            {
                ComboBoxOffers.Items.Add($"{nonActiveOrders[i].OrderID}: {nonActiveOrders[i].Customer.CompanyName} - {nonActiveOrders[i].TotalPrice} kr");
            }
            UpdateTotalPrice();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            WindowShowDialog wsd = new WindowShowDialog();

            if (double.TryParse(TextBoxTotalPrice.Text, out double resultTotalPrice) && TextBoxDateOfPurchase.SelectedDate != null && (RadioButtonIsOrder.IsChecked == true || RadioButtonIsOffer.IsChecked == true))
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
                int comboBoxNumber = ComboBoxOffers.SelectedIndex;
                if (comboBoxNumber == 0) {
                    orderRepository.AddOrder(order);
                }
                else
                {
                    orderRepository.EditOrder(order);
                }

                for (int i = 0; i < orderlines.Count; i++)
                {
                    orderlines[i].OrderID = orderRepository.DisplayLastOrderID();
                    orderline.OrderID = orderlines[i].OrderID;
                    orderline.Product = orderlines[i].Product;
                    orderline.Amount = orderlines[i].Amount;
                    orderlineRepository.AddOrderline(orderline);

                    if (order.Active == true)
                    {
                        product = orderline.Product;
                        product.ProductAmount -= orderline.Amount;
                        productRepository.EditProduct(product);
                    }

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
            orderlines.Add(new Orderline(0, order.OrderID, item, amount));
            
            Orderlines.ItemsSource = orderlines;

            Update();
        }

        void WindowPickCustomer_eventSendList(Domain.Customer customer)
        {
            TextBoxCustomer.Text = customer.CompanyName;
            order.Customer = customer;
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
                totalPrice += orderlines[i].Product.ProductPrice * orderlines[i].Amount;
            }
            TextBoxTotalPrice.Text = totalPrice.ToString();
        }

        private void Update() 
        {
            CollectionViewSource.GetDefaultView(Orderlines.ItemsSource).Refresh();
            UpdateTotalPrice();
        }

        private void ComboBoxOffers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int comboBoxNumber = ComboBoxOffers.SelectedIndex - 1;
            if(comboBoxNumber != -1)
            {
                order = nonActiveOrders[comboBoxNumber];
                TextBoxCustomer.Text = $"{nonActiveOrders[comboBoxNumber].Customer.CompanyName}";
                TextBoxDateOfPurchase.Text = $"{nonActiveOrders[comboBoxNumber].DateOfPurchase}";
                TextBoxTotalPrice.Text = $"{nonActiveOrders[comboBoxNumber].TotalPrice}";
                Orderlines.ItemsSource = orderlineRepository.DisplayOrderlines(nonActiveOrders[comboBoxNumber]);

                RadioButtonIsOrder.IsChecked = true;
                RadioButtonIsOffer.IsEnabled = false;
            }
            else
            {
                order = new Order();
                TextBoxCustomer.Text = $"";
                TextBoxDateOfPurchase.Text = $"";
                TextBoxTotalPrice.Text = $"";
                Orderlines.ItemsSource = null;

                RadioButtonIsOrder.IsChecked = false;
                RadioButtonIsOffer.IsChecked = false;
                RadioButtonIsOffer.IsEnabled = true;
            }
        }
    }
}