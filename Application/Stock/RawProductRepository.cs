using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class RawProductRepository
    {
        private List<RawProduct> rawProducts = new List<RawProduct>();
        private RawProduct rawProduct = new RawProduct();

        //Metods
        public void AddRawProduct(RawProduct rawProduct)
        {
            rawProduct.AddRawProduct(rawProduct.RawProductName, rawProduct.RawProductWeight, rawProduct.DateOfPurchase);
        }

        public void EditRawProduct(RawProduct rawProduct)
        {
            rawProduct.EditRawProduct(rawProduct.RawProductID, rawProduct.RawProductName, rawProduct.RawProductWeight, rawProduct.DateOfPurchase);
        }

        public void DeleteRawProduct(int rawProductID)
        {
            rawProduct.DeleteRawProduct(rawProductID);
        }

        public List<RawProduct> DisplayRawProducts()
        {
            return rawProducts = rawProduct.GetRawProducts();
        }

        public List<RawProduct> DisplaySpecificRawProducts(RawProduct rawProduct)
        {
            return rawProducts = rawProduct.GetSpecificRawProducts(rawProduct.RawProductName, rawProduct.RawProductWeight, rawProduct.DateOfPurchase);
        }

        public RawProduct DisplayRawProduct(int rawProductID)
        {
            return rawProduct.GetRawProduct(rawProductID);
        }
    }
}
