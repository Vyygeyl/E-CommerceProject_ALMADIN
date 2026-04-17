using E_CommerceModels;
using System.Text.Json;

namespace E_CommerceDataService
{
    public class SellerDataService : INTSellerDataService
    {
        // SellerInMemoryData / SellerJsonData / SellerDbData  
        INTSellerDataService dataService = new SellerInMemoryData();

        // CREATE
        public void Add(SellerProfile seller)
        {
            dataService.Add(seller);
        }

        // READ
        public List<SellerProfile> GetSellers()
        {
            return dataService.GetSellers();
        }

        // READ - GUID
        public SellerProfile? GetbyId(Guid ID)
        {
            return dataService.GetbyId(ID);
        }

        // READ - Name
        public SellerProfile? GetbyName(string sellerName)
        {
            return dataService.GetbyName(sellerName);
        }

        // CHECK
        public bool SellerExists(string sellerName)
        {
            return dataService.SellerExists(sellerName);
        }

        // DELETE
        public void Delete(Guid ID)
        {
            dataService.Delete(ID);
        }

        // UPDATE
        public void Update(SellerProfile seller)
        {
            dataService.Update(seller);
        }
    }
}