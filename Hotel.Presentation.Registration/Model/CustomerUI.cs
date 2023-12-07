using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Hotel.Presentation.Registration.Model
{
    public class CustomerUI /* : INotifyPropertyChanged*/
    {
        public CustomerUI(string name, string email, List<MemberUI>? members)
        {
            Name = name;
            Email = email;
            Members = members;
        }

        public CustomerUI(int id , string name, string email, List<MemberUI>? members)
        {
            Id = id;
            Name = name;
            Email = email;
            Members = members;
        }

        public int Id { get; set; }
        private string _name;
        public string Name { get => _name; set { _name = value;} }
        private string _email;
        public string Email { get => _email; set { _email = value; } } 
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
        public string DisplayString { get { return Name + " | " + Email; } }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       

    }
}
