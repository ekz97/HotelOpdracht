using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Organiser
    {
        public int? Id { get; set; }
        public ContactInfo Contact { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 500)
                    throw new CustomerException("Invalid name");

                _name = value;
            }
        }


        public Organiser(int? id, string name, ContactInfo contact) : this(name, contact)
        {
            Id = id;
        }

        public Organiser(string name, ContactInfo contact)
        {
            Name = name;
            Contact = contact;
        }

        
    }
}
