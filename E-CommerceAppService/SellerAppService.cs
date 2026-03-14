using E_CommerceDataService;
using E_CommerceModels;
using System;
using System.Collections.Generic;
using System.Security.Principal;
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

    }
}
