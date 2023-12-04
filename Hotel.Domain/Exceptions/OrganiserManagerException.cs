using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Exceptions
{
    public class OrganiserManagerException : Exception
    {
        public OrganiserManagerException(string? message) : base(message)
        {
        }

        public OrganiserManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
