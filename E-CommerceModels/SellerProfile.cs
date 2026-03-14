using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceModels
{
    public class SellerProfile
    {
        public Guid SellerID { get; set; }
        public string SellerName { get; set; }
        public List<Product> ProductName { get; set; } = new List<Product>();
    }
}
