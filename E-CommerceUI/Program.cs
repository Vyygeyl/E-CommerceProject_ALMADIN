using E_CommerceAppService;
using E_CommerceModels;
using System;
using System.Collections.Generic;

namespace E_CommerceUI
{
    internal class Program
    {
        static SellerAppService sellerAppService = new SellerAppService();

        static void Main(string[] args)
        {
            string restart;

            do
            {
                Console.WriteLine("\n===========================");
                Console.WriteLine(" E-Commerce Seller Management");
                Console.WriteLine("===========================");
                Console.WriteLine("  [1] View Seller Profiles");
                Console.WriteLine("  [2] Add Seller Profile");
                Console.WriteLine("  [3] Update Seller Name");
                Console.WriteLine("  [4] Manage Products");
                Console.WriteLine("  [5] Delete Seller");
                Console.WriteLine("===========================");

                Console.Write("\n  Choose an option: ");

                if (int.TryParse(Console.ReadLine(), out int pick))
                {
                    switch (pick)
                    {
                        case 1:
                            ViewSellers();
                            break;
                        case 2:
                            AddSeller();
                            break;
                        case 3:
                            UpdateSeller();
                            break;
                        case 4:
                            ManageProducts();
                            break;
                        case 5:
                            DeleteSeller();
                            break;
                        default:
                            Console.WriteLine("\n  >> Invalid option. Choose 1-5.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\n  >> Invalid input. Please enter a number.");
                }

                while (true)
                {
                    Console.Write("\n  Run again? (y/n): ");
                    restart = Console.ReadLine()?.ToLower().Trim();

                    if (restart == "y" || restart == "n") break;

                    Console.WriteLine("  >> Please enter 'y' for Yes or 'n' for No.");
                }

            } while (restart == "y");
        }

        static void ViewSellers()
        {
            Console.WriteLine("\n===========================");
            Console.WriteLine("  View Seller Profiles");
            Console.WriteLine("===========================");

            var sellers = sellerAppService.GetSellers();

            for (int i = 0; i < sellers.Count; i++)
            {
                Console.WriteLine($"\n  [{i + 1}] {sellers[i].SellerName}");
                Console.WriteLine("  Products:");
                Console.WriteLine("  --------");

                if (sellers[i].ProductName != null && sellers[i].ProductName.Count > 0)
                {
                    foreach (var product in sellers[i].ProductName)
                    {
                        Console.WriteLine($"    Name: {product.ProductName}  |  Price: {product.ProductPrice}");
                    }
                }
                else
                {
                    Console.WriteLine("    No products listed.");
                }
            }
            Console.WriteLine("\n===========================");
        }

        static void AddSeller()
        {
            Console.WriteLine("\n===========================");
            Console.WriteLine("  Add Seller Profile");
            Console.WriteLine("===========================");

            Console.Write("\n  Seller Name: ");
            string name = Console.ReadLine().Trim();

            Console.Write("\n  Number of products: ");
            if (!int.TryParse(Console.ReadLine(), out int count))
            {
                Console.WriteLine("\n  >> Invalid number.");
                return;
            }

            SellerProfile newSeller = new SellerProfile();
            newSeller.SellerID = Guid.NewGuid();
            newSeller.SellerName = name;
            newSeller.ProductName = new List<Product>();

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\n  Product {i + 1}");
                Console.WriteLine("  ---------------");
                Console.Write("  Name: ");
                string productName = Console.ReadLine().Trim();

                Console.Write("  Price: ");
                if (!double.TryParse(Console.ReadLine(), out double price) || price < 0)
                {
                    Console.WriteLine("\n  >> Invalid price. Change to 0");
                    price = 0;
                }

                Product newProduct = new Product();
                newProduct.ProductID = Guid.NewGuid();
                newProduct.ProductName = productName;
                newProduct.ProductPrice = price;

                newSeller.ProductName.Add(newProduct);
            }

            bool success = sellerAppService.CheckSeller(newSeller);
            if (success)
            {
                Console.WriteLine($"\n  >> Seller \"{name}\" added successfully.");
            }
            else
            {
                Console.WriteLine($"\n  >> Failed to add seller. Check if name is valid or exists.");
            }
        }

        static void UpdateSeller()
        {
            Console.WriteLine("\n===========================");
            Console.WriteLine("  Update Seller Name");
            Console.WriteLine("===========================");

            Console.Write("\n  Seller name to update: ");
            string name = Console.ReadLine().Trim();

            var seller = sellerAppService.GetSellerbyName(name);
            if (seller == null)
            {
                Console.WriteLine("\n  >> Seller not found.");
                return;
            }

            Console.Write("  New name: ");
            string newName = Console.ReadLine().Trim();

            if (sellerAppService.UpdateSeller(name, newName))
            {
                Console.WriteLine($"\n  >> \"{name}\" renamed to \"{newName}\"");
            }
            else
            {
                Console.WriteLine("\n  >> Update failed. Name cannot be empty.");
            }
        }

        static void ManageProducts()
        {
            Console.WriteLine("\n===========================");
            Console.WriteLine("  Manage Products");
            Console.WriteLine("===========================");

            Console.Write("\n  Seller name: ");
            string name = Console.ReadLine().Trim();

            var seller = sellerAppService.GetSellerbyName(name);
            if (seller == null)
            {
                Console.WriteLine("\n  >> Seller not found.");
                return;
            }

            if (seller.ProductName == null || seller.ProductName.Count == 0)
            {
                Console.WriteLine("\n  [ No products listed ]");
            }
            else
            {
                Console.WriteLine($"\n  {seller.SellerName} Shop products:\n");
                for (int i = 0; i < seller.ProductName.Count; i++)
                {
                    Console.WriteLine($"  [{i + 1}] Name: {seller.ProductName[i].ProductName}  |  Price: {seller.ProductName[i].ProductPrice}");
                }
            }

            Console.WriteLine("\n===========================");
            Console.WriteLine("  [1] Add Product");
            Console.WriteLine("  [2] Update Product");
            Console.WriteLine("  [3] Delete Product");
            Console.WriteLine("===========================");
            Console.Write("\n  Choose an option: ");

            if (int.TryParse(Console.ReadLine(), out int option))
            {
                switch (option)
                {
                    case 1:
                        Console.Write("  Name: ");
                        string pName = Console.ReadLine().Trim();
                        Console.Write("  Price: ");
                        if (!double.TryParse(Console.ReadLine(), out double pPrice) || pPrice < 0)
                        {
                            Console.WriteLine("\n  >> Invalid price. Changed to 0");
                            pPrice = 0;
                        }

                        Product p = new Product();
                        p.ProductID = Guid.NewGuid();
                        p.ProductName = pName;
                        p.ProductPrice = pPrice;

                        if (sellerAppService.AddProduct(name, p))
                        {
                            Console.WriteLine("\n  >> Product added.");
                        }
                        else
                        {
                            Console.WriteLine("\n  >> Failed to add product.");
                        }
                        break;

                    case 2:
                        if (seller.ProductName.Count == 0)
                        {
                            Console.WriteLine("\n  >> There is no product to Update.");
                            break;
                        }

                        Console.Write("  Product number: ");
                        if (!int.TryParse(Console.ReadLine(), out int uIndex))
                        {
                            Console.WriteLine("\n  >> Invalid product number.");
                            break;
                        }

                        Console.Write("  New name: ");
                        string uName = Console.ReadLine().Trim();

                        Console.Write("  New price: ");
                        if (!double.TryParse(Console.ReadLine(), out double uPrice) || uPrice < 0)
                        {
                            Console.WriteLine("\n  >> Invalid price. Changed to 0");
                            uPrice = 0;
                        }

                        if (sellerAppService.UpdateProduct(name, uIndex - 1, uName, uPrice))
                        {
                            Console.WriteLine("\n  >> Product updated.");
                        }
                        else
                        {
                            Console.WriteLine("\n  >> Update failed.");
                        }
                        break;

                    case 3:
                        if (seller.ProductName.Count == 0)
                        {
                            Console.WriteLine("\n  >> There is no product to Delete.");
                            break;
                        }

                        Console.Write("  Product number: ");
                        if (!int.TryParse(Console.ReadLine(), out int dIndex))
                        {
                            Console.WriteLine("\n  >> Invalid input. Please enter a number.");
                            break;
                        }

                        if (sellerAppService.DeleteProduct(name, dIndex - 1))
                        {
                            Console.WriteLine("\n  >> Product deleted.");
                        }
                        else
                        {
                            Console.WriteLine("\n  >> Invalid product number.");
                        }
                        break;
                }
            }
        }

        static void DeleteSeller()
        {
            Console.WriteLine("\n===========================");
            Console.WriteLine("  Delete Seller");
            Console.WriteLine("===========================");

            Console.Write("\n  Seller name: ");
            string name = Console.ReadLine().Trim();

            if (sellerAppService.DeleteSeller(name))
            {
                Console.WriteLine($"\n  >> Seller \"{name}\" deleted.");
            }
            else
            {
                Console.WriteLine($"\n  >> Seller \"{name}\" not found.");
            }
        }
    }
}