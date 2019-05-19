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
        public WindowEditOrder(Order order)
        {
            InitializeComponent();
            TextBoxCustomer.Text = order.Customer.CompanyName;
            TextBoxTotalPrice.Text = order.TotalPrice.ToString();
            TextBoxDateOfPurchase.Text = order.DateOfPurchase.ToString();
            orderlines = orderlineRepository.DisplayOrderlines(order, new Offer());
            customer = order.Customer;

            Orderlines.ItemsSource = orderlines;
            
            WindowPickCustomer.eventSendList += WindowPickCustomer_eventSendList;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            WindowShowDialog wsd = new WindowShowDialog();
            
            if (double.TryParse(TextBoxTotalPrice.Text, out double resultTotalPrice) && TextBoxDateOfPurchase.SelectedDate != null)
            {
                order.TotalPrice = double.Parse(TextBoxTotalPrice.Text);
                order.DateOfPurchase = DateTime.Parse(TextBoxDateOfPurchase.Text);
                orderRepository.EditOrder(order);

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
            CollectionViewSource.GetDefaultView(Orderlines.ItemsSource).Refresh();
        }
    }
}
