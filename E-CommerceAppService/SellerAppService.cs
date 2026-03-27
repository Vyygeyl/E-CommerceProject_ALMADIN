using E_CommerceDataService;
using E_CommerceModels;
using System;
using System.Text;

namespace E_CommerceAppService
{
    public class SellerAppService
    {
        SellerDataService sellerDataService = new SellerDataService();

        // ADD
        public bool CheckSeller(SellerProfile seller)
        {
            if(sellerDataService.SellerExists(seller.SellerName))
            return false;

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
            return sellerDataService.GetbyName(sellerName);
        }

        // UPDATE
        public bool UpdateSeller(string sellerName, string newName)
        {
            var seller = GetSellerbyName(sellerName);
            if (seller == null)
                return false;
            seller.SellerName = newName;
            sellerDataService.Update(seller);
            return true;
        }

        // DELETE
        public bool DeleteSeller(string sellerName)
        {
            var seller = GetSellerbyName(sellerName);
            if (seller == null)
                return false;
            sellerDataService.Delete(seller.SellerID);
            return true;
        }
    }
}
