using Hotel.Domain.Exceptions;
using System.ComponentModel;

namespace Hotel.Domain.Model
{
    public class Member 
    {
        private string _name;
        private DateTime? _birthday;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
        
                }
            }
        }

        public DateTime? Birthday
        {
            get { return _birthday; }
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
      
                }
            }
        }

 

        public Member(string name, DateTime? birthday)
        {
            Name = name;
            Birthday = birthday;
        }
        //private string _name;
        //public string Name { get { return _name; } set { if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("member"); _name = value; } }
        //private DateOnly _birthday;

        //public event PropertyChangedEventHandler? PropertyChanged;

   
        public override bool Equals(object? obj)
        {
            return obj is Member member &&
                   _name == member._name &&
                   _birthday.Equals(member._birthday);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_name, _birthday);
        }


    }
}