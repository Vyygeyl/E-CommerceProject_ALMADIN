using E_CommerceModels;
using System.Text.Json;

namespace E_CommerceDataService
{
    public class SellerDataService
    {
        private List<SellerProfile> SellerList = new List<SellerProfile>();
        private string _jsonFileName;

        public SellerDataService()
        {
            _jsonFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/sellersProfile.json";
            AddJsonFile();
        }

        private void AddJsonFile()
        {
            RetrieveDataFromJsonFile();
            if (SellerList.Count <= 0)
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

                SaveDataToJsonFile();
            }
        }

        private void SaveDataToJsonFile()
        {
            using (var outputStream = File.OpenWrite(_jsonFileName))
            {
                JsonSerializer.Serialize<List<SellerProfile>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true })
                    , SellerList);
            }
        }

        private void RetrieveDataFromJsonFile()
        {
            using (var jsonFileReader = File.OpenText(_jsonFileName))
            {
                SellerList = JsonSerializer.Deserialize<List<SellerProfile>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true })
                    .ToList();
            }
        }

        // CREATE
        public void Add(SellerProfile seller)
        {
            SellerList.Add(seller);
            SaveDataToJsonFile();
        }

        // READ
        public List<SellerProfile> GetSellers()
        {
            RetrieveDataFromJsonFile();
            return SellerList;
        }

        // READ - GUID
        public SellerProfile? GetbyId(Guid ID)
        {
            RetrieveDataFromJsonFile();
            return SellerList.FirstOrDefault(s => s.SellerID == ID);
        }

        // READ - Name
        public SellerProfile? GetbyName(string sellerName)
        {
            RetrieveDataFromJsonFile();
            return SellerList.FirstOrDefault(s => s.SellerName == sellerName);
        }

        // CHECK
        public bool SellerExists(string sellerName)
        {
            RetrieveDataFromJsonFile();
            return SellerList.Any(s => s.SellerName == sellerName);

        }

        // DELETE
        public void Delete(Guid ID)
        {
            RetrieveDataFromJsonFile();
            var seller = GetbyId(ID);
            if (seller != null)
            {
                SellerList.Remove(seller);
            }
            SaveDataToJsonFile();

        }

        // UPDATE

        public void Update(SellerProfile seller)
        {
            RetrieveDataFromJsonFile();
            var existing = GetbyId(seller.SellerID);
            if (existing != null)
            {
                existing.SellerName = seller.SellerName;
                existing.ProductName = seller.ProductName;
            }
            SaveDataToJsonFile();

        }
    }

}
