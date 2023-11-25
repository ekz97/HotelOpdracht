using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hotel.Domain.Model
{
    public class ContactInfo
    {
        private string _email;
        private string _phone;
        private Address _address;

        public ContactInfo(string email, string phone, Address address)
        {
            Email = email;
            Phone = phone;
            Address = address;
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                    throw new CustomerException("Invalid email");

                _email = value;
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new CustomerException("Invalid phone number");

                _phone = value;
            }
        }

        public Address Address
        {
            get => _address;
            set
            {
                if (value == null)
                    throw new CustomerException("Invalid address");

                _address = value;
            }
        }
    }

}
