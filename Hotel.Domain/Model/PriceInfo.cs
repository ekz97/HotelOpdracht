using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class PriceInfo
    {
        int AdultPrice { get; set; }
        int ChildPrice { get; set; }
        int Discount { get; set; }
        int AdultAge { get; set; }
    }
}
