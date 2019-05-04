using System;
using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;

namespace Application
{
    public class ProductRepository
    {
        private List<Product> products = new List<Product>();
        private Product product = new Product();

        //Methods
        public void AddProduct(Product product)
        {
            product.AddProduct(product.ProductName, product.ProductWeight, product.ProductPrice, product.ProductAmount, product.DateOfPackaging, product.DateOfExpiration);
        }

        public void EditProduct(Product product)
        {
            product.EditProduct(product.ProductID, product.ProductName, product.ProductWeight, product.ProductPrice, product.ProductAmount, product.DateOfPackaging, product.DateOfExpiration);
        }

        public void DeleteProduct(int productID)
        {
            product.DeleteProduct(productID);
        }

        public List<Product> DisplayProducts()
        {
            return products = product.GetProducts();
        }

        public List<Product> DisplaySpecificProducts(Product product)
        {
            return products = product.GetSpecificProducts(product.ProductName, product.ProductWeight, product.ProductPrice, product.ProductAmount, product.DateOfPackaging, product.DateOfExpiration);
        }
        public Product DisplayProduct(int productID)
        {
            return product.GetProduct(productID);
        }

    }
}
