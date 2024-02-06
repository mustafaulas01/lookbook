using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dto
{
    public class GetB2BProductsResponse
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PicturePath { get; set; }
        public string Gender { get; set; }
        public string MainGroup { get; set; }
        public string SubGroup { get; set; }
        public string Model { get; set; }
    }
}