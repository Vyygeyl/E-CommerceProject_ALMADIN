using E_CommerceModels;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace E_CommerceDataService
{

    public class SellerDataService
    {
        public List <SellerProfile> SellerList = new List<SellerProfile> ();

        public SellerDataService()
        {
            Product Bakery1 = new Product { ProductID = Guid.NewGuid(), ProductName = "Crinkles", ProductPrice = 7.00 };
            Product Bakery2 = new Product { ProductID = Guid.NewGuid(), ProductName = "brownie", ProductPrice = 49.99 };

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

        // CHECK
        public bool SellerExists(string sellerName)
        {
            return SellerList.Any(s => s.SellerName == sellerName);

        }

    }
}
