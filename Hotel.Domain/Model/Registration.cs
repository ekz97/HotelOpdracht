using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Registration
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (value <= 0)
                    throw new RegistrationException("Invalid registration ID");
                else
                    _id = value;
            }
        }
    }
}
