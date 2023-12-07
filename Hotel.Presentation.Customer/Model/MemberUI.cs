using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Customer.Model
{
    public class MemberUI
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }

        public MemberUI(string name, DateTime birthday)
        {
            Name = name;
            Birthday = birthday;
        }

        public override bool Equals(object? obj)
        {
            return obj is MemberUI uI &&
                   Name == uI.Name &&
                   Birthday == uI.Birthday;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Birthday);
        }
    }
}
