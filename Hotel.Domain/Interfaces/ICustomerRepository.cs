using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customerr customer);
        void UpdateCustomer(Customerr customer);
        void DeleteCustomer(int id);
        IReadOnlyList<Customerr> GetCustomers(string filter);
        Customerr GetCustomerById(int? id);
    }
}
