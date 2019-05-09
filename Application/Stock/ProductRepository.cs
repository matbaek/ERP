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
            return product.GetProducts();
        }

        public List<Product> DisplaySpecificProducts(Product product)
        {
            return product.GetSpecificProducts(product.ProductName, product.ProductWeight, product.ProductPrice, product.ProductAmount, product.DateOfPackaging, product.DateOfExpiration);
        }
        public Product DisplayProduct(int productID)
        {
            return product.GetProduct(productID);
        }

    }
}
