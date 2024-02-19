using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class BasketItem
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string PicturePath { get; set; }
        public string Category { get; set; }

        public string SubCategory { get; set; }
    }
}