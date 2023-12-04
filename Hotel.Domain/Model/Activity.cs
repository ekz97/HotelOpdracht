using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Activity
    {
        public int Id { get; set; }

        private DateTime _fixture;
        public DateTime Fixture
        {
            get { return _fixture; }
            set
            {
                if (value < DateTime.Now)
                    throw new ActivityException("Fixture date cannot be in the past");
                else
                    _fixture = value;
            }
        }

        private int _nrOfPlaces;
        public int NrOfPlaces
        {
            get { return _nrOfPlaces; }
            set
            {
                if (value <= 0)
                    throw new ActivityException("Number of places should be greater than zero");
                else
                    _nrOfPlaces = value;
            }
        }

        public Description Description { get; set; }
        public PriceInfo PriceInfo { get; set; }

        public Activity(int id , DateTime fixture, int nrOfPlaces , Description description , PriceInfo priceInfo)
        {
            Id = id;
            Fixture = fixture;
            NrOfPlaces = nrOfPlaces;
            Description = description;
            PriceInfo = priceInfo;
        }
    }
}




