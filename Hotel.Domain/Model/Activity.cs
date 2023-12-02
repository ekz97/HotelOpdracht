using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Activity
    {
        int? Id { get; set; }

        DateTime Fixture { get; set; }
        int NrOfPlaces { get; set; }

        Description Description { get; set; }
        PriceInfo PriceInfo { get; set; }
     }
}
