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

namespace ERP.Offers
{
    /// <summary>
    /// Interaction logic for WindowPickProductAmountOffer.xaml
    /// </summary>
    public partial class WindowPickProductAmountOffer : Window
    {
        private double amount;

        public delegate void SendProductAmountOffer(double productAmount, bool checkProduct);
        public static event SendProductAmountOffer eventSendProductAmountOffer;

        public WindowPickProductAmountOffer()
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
            eventSendProductAmountOffer(amount, true);
            this.Close();
        }
    }
}

