using E_CommerceModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_CommerceDataService
{
    public class SellerInMemoryData : INTSellerDataService
    {
        public List<SellerProfile> SellerList = new List<SellerProfile>();

        public SellerInMemoryData()
        {
            Product Bakery1 = new Product { ProductID = Guid.NewGuid(), ProductName = "Crinkles", ProductPrice = 7.00 };
            Product Bakery2 = new Product { ProductID = Guid.NewGuid(), ProductName = "Brownie", ProductPrice = 49.99 };

            SellerProfile Almadin_Bakery = new SellerProfile();
            Almadin_Bakery.SellerID = Guid.NewGuid();
            Almadin_Bakery.SellerName = "Bake my Day";
            Almadin_Bakery.ProductName = new List<Product>();
            Almadin_Bakery.ProductName.Add(Bakery1);
            Almadin_Bakery.ProductName.Add(Bakery2);
            SellerList.Add(Almadin_Bakery);

            Product Art1 = new Product { ProductID = Guid.NewGuid(), ProductName = "Paint Brush", ProductPrice = 79.99 };
            Product Art2 = new Product { ProductID = Guid.NewGuid(), ProductName = "Canvas", ProductPrice = 99.99 };
            Product Art3 = new Product { ProductID = Guid.NewGuid(), ProductName = "Paint", ProductPrice = 29.99 };

            SellerProfile ArtStore = new SellerProfile();
            ArtStore.SellerID = Guid.NewGuid();
            ArtStore.SellerName = "Paints & Pains";
            ArtStore.ProductName = new List<Product>();
            ArtStore.ProductName.Add(Art1);
            ArtStore.ProductName.Add(Art2);
            ArtStore.ProductName.Add(Art3);
            SellerList.Add(ArtStore);
        }

        // CREATE
        public void Add(SellerProfile seller)
        {
            SellerList.Add(seller);
        }

        // READ
        public List<SellerProfile> GetSellers()
        {
            return SellerList;
        }

        // READ - GUID
        public SellerProfile? GetbyId(Guid ID)
        {
            return SellerList.FirstOrDefault(s => s.SellerID == ID);
        }

        // READ - Name
        public SellerProfile? GetbyName(string sellerName)
        {
            return SellerList.FirstOrDefault(s => s.SellerName == sellerName);
        }

        // CHECK
        public bool SellerExists(string sellerName)
        {
            return SellerList.Any(s => s.SellerName == sellerName);
        }

        // DELETE
        public void Delete(Guid ID)
        {
            var seller = GetbyId(ID);
            if (seller != null)
            {
                SellerList.Remove(seller);
            }
        }

        // UPDATE
        public void Update(SellerProfile seller)
        {
            var existing = GetbyId(seller.SellerID);
            if (existing != null)
            {
                existing.SellerName = seller.SellerName;
                existing.ProductName = seller.ProductName;
            }
        }
    }
}