using Hotel.Domain.Exceptions;
using Hotel.Domain.Model;
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

        public ActivityUI(int? id, DateTime fixture, int nrOfPlaces, DescriptionUI description, PriceInfoUI priceInfo)
        {
            Id = id;
            Fixture = fixture;
            NrOfPlaces = nrOfPlaces;
            Description = description;
            PriceInfo = priceInfo;
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

        public DescriptionUI Description { get; set; }

        public PriceInfoUI PriceInfo { get; set; }

      
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
