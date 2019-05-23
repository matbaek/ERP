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

namespace ERP.Accounting
{
    /// <summary>
    /// Interaction logic for WindowAddInvoice.xaml
    /// </summary>
    public partial class WindowAddInvoice : Window
    {
        private OrderRepository orderRepository = new OrderRepository();
        private InvoiceRepository invoiceRepository = new InvoiceRepository();
        private Invoice invoice = new Invoice();
        private List<Order> activeOrders = new List<Order>();
        private Orderline orderline = new Orderline();
        private List<Orderline> orderlines = new List<Orderline>();
        private OrderlineRepository orderlineRepository = new OrderlineRepository();
        private ProductRepository productRepository = new ProductRepository();
        private Product product = new Product();
        public WindowAddInvoice()
        {
            InitializeComponent();

            activeOrders.AddRange(orderRepository.DisplayNonActiveOrders(true));
            ComboBoxOrders.Items.Add("");
            for (int i = 0; i < activeOrders.Count; i++)
            {
                ComboBoxOrders.Items.Add($"{activeOrders[i].OrderID}: {activeOrders[i].Customer.CompanyName} - {activeOrders[i].TotalPrice} kr");
            }

            ComboBoxFormOfDelivery.Items.Add("Afhentning");
            ComboBoxFormOfDelivery.Items.Add("GLS");

            ComboBoxFormOfPayment.Items.Add("Dankort");
            ComboBoxFormOfPayment.Items.Add("Kontant");
            ComboBoxFormOfPayment.Items.Add("MobilePay");

            UpdateTotalPrice();
        }

        private void Update()
        {
            CollectionViewSource.GetDefaultView(Orderlines.ItemsSource).Refresh();
            UpdateTotalPrice();
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

        private void ComboBoxOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int comboBoxNumber = ComboBoxOrders.SelectedIndex - 1;
            if (comboBoxNumber != -1)
            {
                invoice.Order = activeOrders[comboBoxNumber];
                TextBoxCustomer.Text = $"{invoice.Order.Customer.CompanyName}";
                TextBoxTotalPrice.Text = $"{invoice.Order.TotalPrice}";
                Orderlines.ItemsSource = orderlineRepository.DisplayOrderlines(invoice.Order);
            }
            else
            {
                invoice.Order = new Order();
                TextBoxCustomer.Text = $"";
                TextBoxTotalPrice.Text = $"";
                Orderlines.ItemsSource = null;
            }
        }

        private void ComboBoxFormOfDelivery_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            invoice.FormOfDelivery = ComboBoxFormOfDelivery.SelectedItem.ToString();
        }

        private void ComboBoxFormOfPayment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            invoice.FormOfPayment = ComboBoxFormOfPayment.SelectedItem.ToString();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowShowDialog wsd = new WindowShowDialog();

            if (TextBoxCustomer.Text != "" && ComboBoxFormOfDelivery.SelectedIndex != -1 && ComboBoxFormOfPayment.SelectedIndex != -1 && TextBoxDateOfDelivery.SelectedDate != null)
            {
                invoice.DateOfDelivery = DateTime.Parse(TextBoxDateOfDelivery.Text);

                invoiceRepository.AddInvoice(invoice);

                wsd.LabelShowDialog.Content = "Faktura blev dannet";
                wsd.ShowDialog();

                this.Close();
            }
            else
            {
                wsd.LabelShowDialog.Content = "Der var en fejl man";
                wsd.ShowDialog();
            }
        }
    }
}
