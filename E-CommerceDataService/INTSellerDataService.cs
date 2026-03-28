using E_CommerceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceDataService
{
    public interface INTSellerDataService
    {
        void Add(SellerProfile seller);
        SellerProfile? GetbyId(Guid id);
        SellerProfile? GetbyName(string sellerName);
        List<SellerProfile> GetSellers();
        bool SellerExists(string sellerName);
        void Update(SellerProfile seller);
        void Delete(Guid id);
    }
}
