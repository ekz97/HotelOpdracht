using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Hotel.Presentation.Customer.Model
{
    public class CustomerUI : INotifyPropertyChanged
    {
        public CustomerUI(string name, string email, string address, string phone, int nrOfMembers)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            NrOfMembers = nrOfMembers;      
        }

        public CustomerUI(int? id, string name, string email, string address, string phone, int nrOfMembers, List<MemberUI>? members)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            NrOfMembers = nrOfMembers;
            Members = members;
        }

        public int? Id { get; set; }
        private string _name;
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        private string _email;
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }
        private string _address;
        public string Address { get => _address; set { _address = value; OnPropertyChanged(); } }
        private string _phone;
        public string Phone { get => _phone; set { _phone = value; OnPropertyChanged(); } }
        public int NrOfMembers { get; set; }
        private List<MemberUI> _members;
        public List<MemberUI> Members
        {
            get { return _members; }
            set
            {
                _members = value;
                OnPropertyChanged(nameof(Members));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string transformAddress()
        {
            Match match = Regex.Match(Address, @"^(.*?) \[(\d+)\] - (.*?) - (.*?)$");

            if (match.Success)
            {
                return $"{match.Groups[1].Value}|{match.Groups[2].Value}|{match.Groups[3].Value}|{match.Groups[4].Value}";
            }
            return Address;
        }

    }
}
