using Application;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ERP.Orders
{
    /// <summary>
    /// Interaction logic for PageOrder.xaml
    /// </summary>
    public partial class PageOrder : Page
    {
        private OrderRepository orderRepository = new OrderRepository();
        private Order order;

        public PageOrder()
        {
            InitializeComponent();
            WindowSearchOrder.eventSendList += WindowsSearchOrder_eventSendList;
            Update();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            WindowSearchOrder wso = new WindowSearchOrder();
            wso.ShowDialog();
        }

        private void ButtonAddOrder_Click(object sender, RoutedEventArgs e)
        {
            WindowAddOrder wao =  new WindowAddOrder();
            wao.ShowDialog();
            Update();
        }

        private void ButtonEditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (order != null)
            {
                WindowEditOrder weo = new WindowEditOrder(order);
                weo.ShowDialog();
                Update();
            }
            else
            {
                WindowShowDialog wsd = new WindowShowDialog();
                wsd.LabelShowDialog.Content = "Ingen ordre er valgt!";
                wsd.ShowDialog();
            }
        }

        private void ButtonDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (order != null)
            {
                WindowDeleteOrder weo = new WindowDeleteOrder(order);
                weo.ShowDialog();
                Update();
            }
            else
            {
                WindowShowDialog wsd = new WindowShowDialog();
                wsd.LabelShowDialog.Content = "Ingen ordre er valgt!";
                wsd.ShowDialog();
            }
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            PageMenu pm = new PageMenu();
            this.NavigationService.Navigate(pm);
        }

        private void Update()
        {
            List<Order> items = orderRepository.DisplayOrders();
            Orders.ItemsSource = items;
        }

        private void Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Order)(Orders.SelectedItem) != null)
            {
                order = ((Order)Orders.SelectedItem);
            }
        }

        void WindowsSearchOrder_eventSendList(List<Order> items)
        {
            Orders.ItemsSource = items;
        }
    }
}
