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
            string restart = "true";

            do
            {
                Console.WriteLine(" ");
                Console.WriteLine("Welcome to E-Commerce Seller Profile Management System");
                Console.WriteLine(" ");
                Console.WriteLine("===================");
                Console.WriteLine("0. View Seller Profiles");
                Console.WriteLine("1. Add Seller Profile");
                Console.WriteLine("2. Update Seller Profile");
                Console.WriteLine("3. Delete a Seller Profile");
                Console.WriteLine("===================");
                Console.WriteLine(" ");
                Console.Write("Choose an option: ");
                pick = Convert.ToInt32(Console.ReadLine());

                switch (pick)
                {
                    case 0:
                        ViewSellers();
                        break;
                    case 1:
                        AddSeller();
                        break;
                    case 2:
                        UpdateSeller();
                        break;
                    case 3:
                        DeleteSeller();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please select a valid option.");
                        break;
                }

            } while (restart == "true");
        }

        static void ViewSellers()
        {
            Console.WriteLine("\nSeller Profiles");
            Console.WriteLine(" ");

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
                    Console.WriteLine("No products yet.");
                }

                Console.WriteLine(" ");
            }
        }

        static void AddSeller()
        {
            Console.WriteLine("\nYou are ADDING a new Seller Profile:");
            Console.Write("Seller Name: ");
            string name = Console.ReadLine();

            SellerProfile newSeller = new SellerProfile();
            newSeller.SellerID = Guid.NewGuid();
            newSeller.SellerName = name;
            newSeller.ProductName = new List<Product>();

            Console.Write("\nTotal number of Products for " + name + ": ");
            int count = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("\nProduct " + (i + 1));
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
                Console.WriteLine("\nSuccessfully added Seller " + name);
                Console.WriteLine("Successfully added " + count + " Products");
            }
            else
            {
                Console.WriteLine("\nSeller " + name + " already exists!");
            }
        }

        static void UpdateSeller()
        {
            Console.WriteLine("\nUPDATING A SELLER:");
            Console.Write("Enter Seller Name to update: ");
            string name = Console.ReadLine();

            var seller = sellerAppService.GetSellerbyName(name);

            if (seller == null)
            {
                Console.WriteLine("\nSeller not found!");
                return;
            }

            Console.WriteLine("\nWhat do you want to update?");
            Console.WriteLine("[1] Seller Name");
            Console.WriteLine("[2] Products");
            Console.WriteLine("[3] Both");
            Console.Write("Choose an option: ");
            int option = Convert.ToInt32(Console.ReadLine());

            string newName = name;

            if (option == 1 || option == 3)
            {
                Console.Write("Enter new Seller Name: ");
                newName = Console.ReadLine();
            }

            if (option == 2 || option == 3)
            {
                Console.WriteLine("\nCurrent Products:");
                for (int i = 0; i < seller.ProductName.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {seller.ProductName[i].ProductName} : {seller.ProductName[i].ProductPrice}");
                }

                Console.WriteLine("\nWhat do you want to do with products?");
                Console.WriteLine("[1] Add a new product");
                Console.WriteLine("[2] Update existing product");
                Console.WriteLine("[3] Delete a product");
                Console.Write("Choose an option: ");
                int productOption = Convert.ToInt32(Console.ReadLine());

                switch (productOption)
                {
                    case 1:
                        Console.Write("Product Name: ");
                        string newProductName = Console.ReadLine();
                        Console.Write("Price: ");
                        double newPrice = double.Parse(Console.ReadLine());

                        Product newProduct = new Product();
                        newProduct.ProductID = Guid.NewGuid();
                        newProduct.ProductName = newProductName;
                        newProduct.ProductPrice = newPrice;

                        seller.ProductName.Add(newProduct);
                        Console.WriteLine("\nProduct added successfully!");
                        break;

                    case 2:
                        Console.Write("Enter product number to update: ");
                        int updateIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                        if (updateIndex >= 0 && updateIndex < seller.ProductName.Count)
                        {
                            Console.Write("Enter new Product Name: ");
                            seller.ProductName[updateIndex].ProductName = Console.ReadLine();
                            Console.Write("Enter new Price: ");
                            seller.ProductName[updateIndex].ProductPrice = double.Parse(Console.ReadLine());
                            Console.WriteLine("\nProduct updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid product number!");
                        }
                        break;

                    case 3:
                        Console.Write("Enter product number to delete: ");
                        int deleteIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                        if (deleteIndex >= 0 && deleteIndex < seller.ProductName.Count)
                        {
                            Console.WriteLine("\nSuccessfully deleted " + seller.ProductName[deleteIndex].ProductName + "!");
                            seller.ProductName.RemoveAt(deleteIndex);
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid product number!");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }

            bool success = sellerAppService.UpdateSeller(name, newName);

            if (success)
                Console.WriteLine("\nSuccessfully updated seller " + name + "!");
            else
                Console.WriteLine("\nUpdate failed!");
        }

        static void DeleteSeller()
        {
            Console.WriteLine("\nDELETING A SELLER:");
            Console.Write("Enter Seller Name to delete: ");
            string name = Console.ReadLine();

            bool success = sellerAppService.DeleteSeller(name);

            if (success)
                Console.WriteLine("\nSuccessfully deleted " + name + "!");
            else
                Console.WriteLine("\nSeller " + name + " not found!");
        }
    }
}
