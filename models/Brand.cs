using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12TPIPROJECT.models
{
    public class Brand
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }

        public Brand(int brandID, string brandName)
        {
            BrandID = brandID;
            BrandName = brandName;
        }
    }
}
