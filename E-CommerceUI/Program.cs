using E_CommerceModels;
using E_CommerceAppService;

namespace E_CommerceUI
{
    internal class Program
    {
        static SellerAppService sellerAppService = new SellerAppService();

        static void Main(string[] args)
        {
            int pick;
            string restart;

            do
            {
                Console.WriteLine("\n===========================");

                Console.WriteLine(" \n");
                Console.WriteLine("=== Welcome to E-Commerce Seller Profile Management System ===");
                Console.WriteLine(" ");
                Console.WriteLine("===========================");
                Console.WriteLine("[1] View Seller Profiles");
                Console.WriteLine("[2] Add Seller Profile");
                Console.WriteLine("[3] Update Seller Profile");
                Console.WriteLine("[4] Delete a Seller Profile");
                Console.WriteLine("===========================");
                Console.WriteLine(" ");
                Console.Write("Choose an option: ");
                pick = Convert.ToInt32(Console.ReadLine());

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
                        DeleteSeller();
                        break;
                    default:
                        Console.WriteLine("=== Invalid option. Please select a valid option ===");
                        break;
                }

                Console.Write("\nDo you want to run the system again? (y/n): ");
                restart = Console.ReadLine().ToLower();

            } while (restart == "y");

        }


        static void ViewSellers()
        {
            Console.WriteLine("\n===== Seller Profiles =====");
            Console.WriteLine("\n===========================\n");


            var sellers = sellerAppService.GetSellers();

            for (int i = 0; i < sellers.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] Seller: {sellers[i].SellerName}");
                Console.WriteLine("Products:");

                if (sellers[i].ProductName != null && sellers[i].ProductName.Count > 0)
                {
                    foreach (var product in sellers[i].ProductName)
                    {
                        Console.WriteLine($"  - {product.ProductName} : {product.ProductPrice}");
                    }
                }
                else
                {
                    Console.WriteLine("\n=== No products yet ===");
                }

                Console.WriteLine("\n===========================\n");
            }
        }

        static void AddSeller()
        {
            Console.WriteLine("\n===== You are ADDING a new Seller Profile =====");
            Console.WriteLine("\n===============================================");

            Console.Write("\nSeller Name: ");
            string name = Console.ReadLine();

            SellerProfile newSeller = new SellerProfile();
            newSeller.SellerID = Guid.NewGuid();
            newSeller.SellerName = name;
            newSeller.ProductName = new List<Product>();

            Console.WriteLine("\n===============================================");

            Console.Write("\nTotal number of Products for " + name + ": ");
            int count = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n===============================================");

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("\n=== Product " + (i + 1) + " ===");
                Console.Write("Product Name: ");
                string productName = Console.ReadLine();
                Console.Write("Price: ");
                double price = double.Parse(Console.ReadLine());

                Product newProduct = new Product();
                newProduct.ProductID = Guid.NewGuid();
                newProduct.ProductName = productName;
                newProduct.ProductPrice = price;

                newSeller.ProductName.Add(newProduct);
            }

            bool success = sellerAppService.CheckSeller(newSeller);

            if (success)
            {
                Console.WriteLine("\n=== Successfully added Seller " + name + " ===");
                Console.WriteLine("\n === Successfully added " + count + " Products ===");
            }
            else
            {
                Console.WriteLine("\n=== Seller " + name + " already exists ===");
            }
        }

        static void UpdateSeller()
        {
            Console.WriteLine("\n===== You are UPDATING a Seller Profile =====");
            Console.WriteLine("\n=============================================");

            Console.Write("\nEnter Seller Name to update: ");
            string name = Console.ReadLine();

            var seller = sellerAppService.GetSellerbyName(name);

            if (seller == null)
            {
                Console.WriteLine("\n=== Seller not found ===");
                return;
            }

            Console.WriteLine("\n=== What do you want to update? ===\n");
            Console.WriteLine("[1] Seller Name");
            Console.WriteLine("[2] Products");
            Console.WriteLine("[3] Both");
            Console.WriteLine("\n===================================");

            Console.Write("\nChoose an option: ");
            int option = Convert.ToInt32(Console.ReadLine());

            string newName = name;

            if (option == 1 || option == 3)
            {
                Console.WriteLine("\n=== You are UPDATING a Seller Name ===");
                Console.Write("\nEnter new Seller Name: ");
                newName = Console.ReadLine();
                Console.WriteLine("\n======================================");
            }

            if (option == 2 || option == 3)
            {
                Console.WriteLine("\n=== Current Products ===");
                for (int i = 0; i < seller.ProductName.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {seller.ProductName[i].ProductName} : {seller.ProductName[i].ProductPrice}");
                }
                Console.WriteLine("\n========================");

                Console.WriteLine("\n=== What do you want to do with products? ===");
                Console.WriteLine("[1] Add a new product");
                Console.WriteLine("[2] Update existing product");
                Console.WriteLine("[3] Delete a product");
                Console.WriteLine("\n=============================================");

                Console.Write("\nChoose an option: ");
                int productOption = Convert.ToInt32(Console.ReadLine());

                switch (productOption)
                {
                    case 1:
                        Console.WriteLine("\n=== You are UPDATING a Product Name ===");

                        Console.Write("Product Name: ");
                        string newProductName = Console.ReadLine();
                        Console.Write("\nPrice: ");
                        double newPrice = double.Parse(Console.ReadLine());

                        Product newProduct = new Product();
                        newProduct.ProductID = Guid.NewGuid();
                        newProduct.ProductName = newProductName;
                        newProduct.ProductPrice = newPrice;

                        seller.ProductName.Add(newProduct);
                        Console.WriteLine("\n=== Product added successfully ===");
                        break;

                    case 2:
                        Console.WriteLine("\n=== You are UPDATING a Product Name ===");

                        Console.Write("Enter product number to update: ");
                        int updateIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                        if (updateIndex >= 0 && updateIndex < seller.ProductName.Count)
                        {
                            Console.WriteLine("\n===================");
                            Console.Write("Enter new Product Name: ");
                            seller.ProductName[updateIndex].ProductName = Console.ReadLine();
                            Console.Write("Enter new Price: ");
                            seller.ProductName[updateIndex].ProductPrice = double.Parse(Console.ReadLine());
                            Console.WriteLine("\n=== Product updated successfully ===");
                        }
                        else
                        {
                            Console.WriteLine("\n=== Invalid product number ===");
                        }
                        break;

                    case 3:
                        Console.Write("Enter product number to delete: ");
                        int deleteIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                        if (deleteIndex >= 0 && deleteIndex < seller.ProductName.Count)
                        {
                            Console.WriteLine("\n=== Successfully deleted " + seller.ProductName[deleteIndex].ProductName + " ===");
                            seller.ProductName.RemoveAt(deleteIndex);
                        }
                        else
                        {
                            Console.WriteLine("\n=== Invalid product number ===");
                        }
                        break;

                    default:
                        Console.WriteLine("=== Invalid option ===");
                        break;
                }
            }

            bool success = sellerAppService.UpdateSeller(name, newName);

            if (success)
                Console.WriteLine("\n=== Successfully updated seller " + name + " ===");
            else
                Console.WriteLine("\n=== Update failed ===");
        }

        static void DeleteSeller()
        {
            Console.WriteLine("\n ===== DELETING A SELLER ===== ");
            Console.WriteLine("\n=================================");

            Console.Write("\nEnter Seller Name to delete: ");
            string name = Console.ReadLine();

            bool success = sellerAppService.DeleteSeller(name);

            Console.WriteLine("\n=================================");

            if (success)
                Console.WriteLine("\n=== Successfully deleted " + name + " ===");
            else
                Console.WriteLine("\n=== Seller " + name + " not found ===");
        }
    }
}
