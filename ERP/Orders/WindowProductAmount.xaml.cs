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
    /// Interaction logic for WindowProductAmount.xaml
    /// </summary>
    public partial class WindowProductAmount : Window
    {
        private double amount;

        public delegate void SendProductAmount(double productAmount, bool checkProduct);
        public static event SendProductAmount eventSendProductAmount;

        public WindowProductAmount()
        {
            InitializeComponent();
            TextBoxProductAmount.Focus();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            amount = double.Parse(TextBoxProductAmount.Text);
            eventSendProductAmount(amount, true);
            this.Close();
        }
    }
}
