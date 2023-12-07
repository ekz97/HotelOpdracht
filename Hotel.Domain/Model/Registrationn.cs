using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Registrationn
    {
        public Registrationn(List<Member> members, Activity activity)
        {
            Members = members;
            Activity = activity;
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (value <= 0)
                    throw new RegistrationException("Invalid registration ID");
                else
                    _id = value;
            }
        }
        public List<Member> Members { get; set; }

        public Activity Activity { get; set; }  

        public double CalcCost()
        {
            int totalPrice = 0;
            foreach (var member in Members)
            {
                TimeSpan age = DateTime.Now - member.Birthday;
                if(Convert.ToInt32(age.TotalDays/365.25) >= Activity.PriceInfo.AdultAge)
                {
                    totalPrice += Activity.PriceInfo.AdultPrice;
                }
                else
                {
                    totalPrice += Activity.PriceInfo.ChildPrice;
                }
            }
            double discountMultiplier = 1.0 - (Activity.PriceInfo.Discount / 100.0);
            return totalPrice * discountMultiplier;
        }
    }
}
