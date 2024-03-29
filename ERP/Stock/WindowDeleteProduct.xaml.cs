﻿using System;
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
using Application;
using Domain;

namespace ERP.Stock
{
    /// <summary>
    /// Interaction logic for WindowDeleteProduct.xaml
    /// </summary>
    public partial class WindowDeleteProduct : Window
    {
        private ProductRepository productRepository = new ProductRepository();
        private Product product;
        public WindowDeleteProduct(Product product)
        {
            InitializeComponent();
            this.product = product;
            LabelDeleteProduct.Content = $"Er du sikker på at du vil slette '{product.ProductName}'?";
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            productRepository.DeleteProduct(product.ProductID);
            this.Close();
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
