using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class PriceInfo
    {
        private int _adultPrice;
        public int AdultPrice
        {
            get { return _adultPrice; }
            set
            {
                if (value < 0)
                    throw new PriceInfoException("Price cannot be negative");
                else
                    _adultPrice = value;
            }
        }

        private int _childPrice;
        public int ChildPrice
        {
            get { return _childPrice; }
            set
            {
                if (value < 0)
                    throw new PriceInfoException("Price cannot be negative");
                else
                    _childPrice = value;
            }
        }
        private int _discount;
        public int Discount
        {
            get { return _discount; }
            set
            {
                if (value < 0 || value > 100) 
                    throw new PriceInfoException("Discount should be between 0 and 100");
                else
                    _discount = value;
            }
        }


        public int AdultAge { get; private set; } = 18;

        public PriceInfo(int adultPrice, int childPrice, int discount)
        {
            AdultPrice = adultPrice;
            ChildPrice = childPrice;
            Discount = discount;
           
        }
    }
}

