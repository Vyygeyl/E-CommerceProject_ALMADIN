using E_CommerceDataService;
using E_CommerceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceAppService
{
    public class SellerAppService
    {
        SellerDataService sellerDataService = new SellerDataService();

        // ADD
        public bool CheckSeller(SellerProfile seller)
        {
            if (seller == null || string.IsNullOrWhiteSpace(seller.SellerName)) 
            {
                return false;
            }

            seller.SellerName = seller.SellerName.Trim();

            if (sellerDataService.SellerExists(seller.SellerName))
            {
                return false;
            }

            sellerDataService.Add(seller);
            return true;
        }

        // READ
        public List<SellerProfile> GetSellers()
        {
            return sellerDataService.GetSellers();
        }

        // READ - Name (user)
        public SellerProfile? GetSellerbyName(string sellerName)
        {
            if (string.IsNullOrWhiteSpace(sellerName))
            {
                return null;
            }

            var seller = sellerDataService.GetbyName(sellerName.Trim());

            if (seller == null || seller.SellerID == Guid.Empty)
            {
                return null;
            }

            return seller;
        }

        // UPDATE
        public bool UpdateSeller(string sellerName, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                return false;
            }

            var seller = GetSellerbyName(sellerName);
            if (seller == null)
            {
                return false;
            }

            seller.SellerName = newName.Trim();
            sellerDataService.Update(seller);
            return true;
        }

        // DELETE
        public bool DeleteSeller(string sellerName)
        {
            if (string.IsNullOrWhiteSpace(sellerName))
            {
                return false;
            }

            var seller = GetSellerbyName(sellerName);

            if (seller == null)
            {
                return false;
            }

            sellerDataService.Delete(seller.SellerID);
            return true;
        }

        // ADD PRODUCT
        public bool AddProduct(string sellerName, Product product)
        {
            var seller = GetSellerbyName(sellerName);
            if (seller == null)
            {
                return false;
            }

            if (product == null || string.IsNullOrWhiteSpace(product.ProductName) || product.ProductPrice < 0)
            {
                return false;
            }

            product.ProductName = product.ProductName.Trim();
            product.ProductID = Guid.NewGuid();
            seller.ProductName.Add(product);
            sellerDataService.Update(seller);
            return true;
        }

        // UPDATE PRODUCT
        public bool UpdateProduct(string sellerName, int index, string newName, double newPrice)
        {
            var seller = GetSellerbyName(sellerName);

            if (seller == null || index < 0 || index >= seller.ProductName.Count)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(newName) || newPrice < 0) return false;

            seller.ProductName[index].ProductName = newName.Trim();
            seller.ProductName[index].ProductPrice = newPrice;

            sellerDataService.Update(seller);
            return true;
        }

        // DELETE PRODUCT
        public bool DeleteProduct(string sellerName, int index)
        {
            var seller = GetSellerbyName(sellerName);
            if (seller == null || index < 0 || index >= seller.ProductName.Count)
            {
                return false;
            }

            seller.ProductName.RemoveAt(index);
            sellerDataService.Update(seller);
            return true;
        }
    }
}