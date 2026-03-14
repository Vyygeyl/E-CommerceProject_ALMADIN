using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceModels
{
    public class Product
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
    }
}
