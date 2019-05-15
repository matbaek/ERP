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
using Domain;
using Application;

namespace ERP.Orders
{
    /// <summary>
    /// Interaction logic for WindowSearchOrder.xaml
    /// </summary>
    public partial class WindowSearchOrder : Window
    {
        private OrderRepository orderRepository = new OrderRepository();

        public delegate void SendList(List<Order> items);
        public static event SendList eventSendList;

        public WindowSearchOrder()
        {
            InitializeComponent();
        }

        private void ButtonSearchOrdre_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            WindowShowDialog wsd = new WindowShowDialog();

            if (double.TryParse(TextBoxOrderID.Text, out double resultOrderID) || (double.TryParse(TextBoxCustomer.Text, out double resultCustomer) || double.TryParse(TextBoxTotalPrice.Text, out double resultTotalPrice) || DatePickerDateOfPurchase.SelectedDate != null))
            {
                if (double.TryParse(TextBoxOrderID.Text, out resultOrderID) == false)
                {
                    order.OrderID = 0;
                }
                else
                {
                    order.OrderID = int.Parse(TextBoxOrderID.Text);
                }

                if (double.TryParse(TextBoxCustomer.Text, out resultCustomer) == false)
                {
                    order.CustomerID = 0;
                }
                else
                {
                    order.CustomerID = int.Parse(TextBoxCustomer.Text);
                }

                if (double.TryParse(TextBoxTotalPrice.Text, out resultTotalPrice) == false)
                {
                    order.TotalPrice = 0;
                }
                else
                {
                    order.TotalPrice = double.Parse(TextBoxTotalPrice.Text);
                }

                if (DatePickerDateOfPurchase.SelectedDate == null)
                {
                    order.DateOfPurchase = new DateTime(1973, 1, 1, 12, 0, 0);
                }
                else
                {
                    order.DateOfPurchase = DateTime.Parse(DatePickerDateOfPurchase.Text);
                }

                eventSendList(orderRepository.DisplaySpecificOrders(order));
                this.Close();
            }

            else
            {
                wsd.LabelShowDialog.Content = "Der var en fejl i 'Order nummer', 'Kunde', 'Samlet pris' eller 'Pakke dato'";
                wsd.ShowDialog();
            }
        }
    }
}
