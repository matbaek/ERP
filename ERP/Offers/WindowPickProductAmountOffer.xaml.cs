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

namespace ERP.Offers
{
    /// <summary>
    /// Interaction logic for WindowPickProductAmountOffer.xaml
    /// </summary>
    public partial class WindowPickProductAmountOffer : Window
    {
        private double amount;
        private Product product = new Product();

        public delegate void SendProductAmountOffer(double productAmount, bool checkProduct);
        public static event SendProductAmountOffer eventSendProductAmountOffer;

        public WindowPickProductAmountOffer(Product product)
        {
            InitializeComponent();
            TextBoxProductAmount.Focus();
            this.product = product;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            amount = double.Parse(TextBoxProductAmount.Text);

            if (amount <= product.ProductAmount)
            {
                eventSendProductAmountOffer(amount, true);
                this.Close();
            }
            else
            {
                WindowShowDialog wsd = new WindowShowDialog();
                wsd.LabelShowDialog.Content = "Så mange vare er der ikke på lager";
                wsd.ShowDialog();
            }
        }
    }
}

