using System;
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
        private OrderRepository orderRepository = new OrderRepository();
        public WindowAddOrder()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            WindowShowDialog wsd = new WindowShowDialog();

            if(int.TryParse(TextBoxCustomerId.Text, out int resultId) && double.TryParse(TextBoxTotalPrice.Text, out double resultTotalPrice) && TextBoxDateOfPurchase.SelectedDate != null)
            {
                order.CustomerID = int.Parse(TextBoxCustomerId.Text);
                order.TotalPrice = double.Parse(TextBoxTotalPrice.Text);
                order.DateOfPurchase = DateTime.Parse(TextBoxDateOfPurchase.Text);
                orderRepository.AddOrder(order);

                wsd.LabelShowDialog.Content = "Ordren blev tilføjet";
                wsd.ShowDialog();

                this.Close();
            }
            else
            {
                wsd.LabelShowDialog.Content = "Der var en fejl i 'ID' eller 'Sum'";
                wsd.ShowDialog();
            }
        }

        private void TextBoxWeight_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
