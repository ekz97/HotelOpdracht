using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Customerr
    {
        public int Id { get; set; }

        public ContactInfo Contact { get; set; }
        private List<Member> _members = new List<Member>();
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


        public Customerr(int id, string name, ContactInfo contact) : this(name, contact)
        {
            Id = id;
        }

        public Customerr(string name, ContactInfo contact)
        {
            Name = name;
            Contact = contact;
        }

        public IReadOnlyList<Member> GetMembers() { return _members.AsReadOnly(); }

        public void AddMember(Member member)
        {
            if (!_members.Contains(member))
                _members.Add(member);
            else
                throw new CustomerException("AddMember");
        }

        public void RemoveMember(Member member)
        {
            if (_members.Contains(member))
                _members.Remove(member);
            else
                throw new CustomerException("RemoveMember");
        }
    }

}
