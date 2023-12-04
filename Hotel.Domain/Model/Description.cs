using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Description
    {
        private int _duration;
        public int Duration
        {
            get { return _duration; }
            set
            {
                if (value <= 0)
                    throw new DescriptionException("Duration should be greater than zero");
                else
                    _duration = value;
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        private string _explanation;
        public string Explanation
        {
            get { return _explanation; }
            set { _explanation = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 500)
                    throw new DescriptionException("Invalid name");
                else
                    _name = value;
            }
        }

        public Description(int duration, string location, string explanation, string name)
        {
            Duration = duration;
            Location = location;
            Explanation = explanation;
            Name = name;
        }
    }
}
