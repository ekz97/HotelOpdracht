using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hotel.Presentation.Activities.Model
{
    public class ActivityUI : INotifyPropertyChanged
    {
        public ActivityUI(DateTime fixture, int nrOfPlaces)
        {
            Fixture = fixture;
            NrOfPlaces = nrOfPlaces;
        }

        public ActivityUI(int? id, DateTime fixture, int nrOfPlaces, int durationDescription, string locationDescription, string explanationDescription, string nameDescription,int adultPriceInfo, int childPriceInfo, int discountPriceInfo)
        {
            Id = id;
            Fixture = fixture;
            NrOfPlaces = nrOfPlaces;
            DurationDescription = durationDescription;
            LocationDescription = locationDescription;
            ExplanationDescription = explanationDescription;
            NameDescription = nameDescription;
            AdultPriceInfo = adultPriceInfo;
            ChildPriceInfo = childPriceInfo;
            DiscountPriceInfo = discountPriceInfo;
        }

        private int? _id;
        public int? Id
        {
            get => _id;
            set { _id = value; }
        }

        private DateTime _fixture;
        public DateTime Fixture
        {
            get => _fixture;
            set
            {
                if (value < DateTime.Now)
                    throw new ActivityException("Fixture date cannot be in the past");
                else
                {
                    _fixture = value;
              
                }
            }
        }

        private int _nrOfPlaces;
        public int NrOfPlaces
        {
            get => _nrOfPlaces;
            set
            {
                if (value <= 0)
                    throw new ActivityException("Number of places should be greater than zero");
                else
                {
                    _nrOfPlaces = value;
       
                }
            }
        }

        private int _durationDescription;
        public int DurationDescription
        {
            get => _durationDescription;
            set { _durationDescription = value;}
        }

        private string _locationDescription;
        public string LocationDescription
        {
            get => _locationDescription;
            set { _locationDescription = value; }
        }

        private string _explanationDescription;
        public string ExplanationDescription
        {
            get => _explanationDescription;
            set { _explanationDescription = value;  }
        }

        private string _nameDescription;
        public string NameDescription
        {
            get => _nameDescription;
            set { _nameDescription = value;}
        }

        private int _adultPriceInfo;
        public int AdultPriceInfo
        {
            get => _adultPriceInfo;
            set { _adultPriceInfo = value; }
        }

        private int _childPriceInfo;
        public int ChildPriceInfo
        {
            get => _childPriceInfo;
            set { _childPriceInfo = value;  }
        }

        private int _discountPriceInfo;
        public int DiscountPriceInfo
        {
            get => _discountPriceInfo;
            set { _discountPriceInfo = value;}
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
