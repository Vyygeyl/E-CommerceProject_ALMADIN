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
                Console.WriteLine("Welcome to E-Commerce Seller Management System");
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
                        // Update
                        break;
                    case 3:
                        // Delete
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

            Console.Write("\nTotal number of Products for " + name + ":");

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
                Console.WriteLine("\nSuccessfully added Seller" +name);
                Console.WriteLine("Successfully added " + count + " Products");
            }
            else
            {
                Console.WriteLine("\nSeller " + name + "already exists!");
            }
        }
    }
}