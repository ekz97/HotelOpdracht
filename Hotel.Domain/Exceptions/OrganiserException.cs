using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Exceptions
{
    public class OrganiserException : Exception
    {
        public OrganiserException(string? message) : base(message)
        {
        }

        public OrganiserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
