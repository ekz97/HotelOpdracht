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
        public ActivityUI(string fixture, int nrOfPlaces)
        {
            Fixture = fixture;
            NrOfPlaces = nrOfPlaces;
        }

        public ActivityUI(int id, string fixture, int nrOfPlaces, DescriptionUI description, PriceInfoUI priceInfo)
        {
            Id = id;
            Fixture = fixture;
            NrOfPlaces = nrOfPlaces;
            Description = description;
            PriceInfo = priceInfo;
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _fixture;
        public string Fixture
        {
            get { return _fixture; }
            set
            {
                _fixture = value;
            }
        }

        private int _nrOfPlaces;
        public int NrOfPlaces
        {
            get { return _nrOfPlaces; }
            set
            {
                _nrOfPlaces = value;
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
