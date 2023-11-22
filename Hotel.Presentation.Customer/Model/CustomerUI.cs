using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Customer.Model
{
    public class CustomerUI : INotifyPropertyChanged
    {
        public CustomerUI(string name, string email, Address address, string phone, int nrOfMembers)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            NrOfMembers = nrOfMembers;
            MemberList = new List<Member>();
        }

        public CustomerUI(int? id, string name, string email, Address address, string phone, int nrOfMembers,List<Member> members)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            NrOfMembers = nrOfMembers;
            MemberList = members ?? new List<Member>();
        }

        public int? Id { get; set; }
        private string _name;
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
        private string _email;
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(); } }
        private Address _address { get; set; }
        public Address Address { get { return _address; } set { _address = value; OnPropertyChanged(); } }
        private string _phone;
        public string Phone { get { return _phone; } set {_phone=value; OnPropertyChanged();} }
        public int NrOfMembers { get; set; }
        private List<Member> _memberList;
        public List<Member> MemberList { get { return _memberList ; } set{ _memberList = value; OnPropertyChanged(nameof(MemberList));} }
        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
