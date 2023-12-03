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

        public ActivityUI(int? id, DateTime fixture, int nrOfPlaces, int durationDescription, string locationDescription, string explanationDescription, string nameDescription, int childPriceInfo, int discountPriceInfo, int adultAgePriceInfo)
        {
            Id = id;
            Fixture = fixture;
            NrOfPlaces = nrOfPlaces;
            DurationDescription = durationDescription;
            LocationDescription = locationDescription;
            ExplanationDescription = explanationDescription;
            NameDescription = nameDescription;
            ChildPriceInfo = childPriceInfo;
            DiscountPriceInfo = discountPriceInfo;
            AdultAgePriceInfo = adultAgePriceInfo;
        }

        private int? _id;
        public int? Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        private int _durationDescription;
        public int DurationDescription
        {
            get => _durationDescription;
            set { _durationDescription = value; OnPropertyChanged(); }
        }

        private string _locationDescription;
        public string LocationDescription
        {
            get => _locationDescription;
            set { _locationDescription = value; OnPropertyChanged(); }
        }

        private string _explanationDescription;
        public string ExplanationDescription
        {
            get => _explanationDescription;
            set { _explanationDescription = value; OnPropertyChanged(); }
        }

        private string _nameDescription;
        public string NameDescription
        {
            get => _nameDescription;
            set { _nameDescription = value; OnPropertyChanged(); }
        }

        private int _childPriceInfo;
        public int ChildPriceInfo
        {
            get => _childPriceInfo;
            set { _childPriceInfo = value; OnPropertyChanged(); }
        }

        private int _discountPriceInfo;
        public int DiscountPriceInfo
        {
            get => _discountPriceInfo;
            set { _discountPriceInfo = value; OnPropertyChanged(); }
        }

        private int _adultAgePriceInfo;
        public int AdultAgePriceInfo
        {
            get => _adultAgePriceInfo;
            set { _adultAgePriceInfo = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
