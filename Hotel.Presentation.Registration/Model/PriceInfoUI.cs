using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Activities.Model
{
    public class PriceInfoUI
    {
        public PriceInfoUI(int adultPrice, int childPrice, int discount)
        {
            AdultPrice = adultPrice;
            ChildPrice = childPrice;
            Discount = discount;
        }

        public int AdultPrice { get; set; }
        public int ChildPrice { get; set; }
        public int Discount { get; set; }

        public string DisplayString { get { return $"Adult price: €{AdultPrice}\nChild price: €{ChildPrice}\nDiscount: {Discount}%"; } }
    }
}
