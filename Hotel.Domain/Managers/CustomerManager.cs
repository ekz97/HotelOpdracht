using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class CustomerManager
    {
        private ICustomerRepository _customerRepository;
     

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IReadOnlyList<Customerr> GetCustomers(string filter)
        {
            try
            {
                return _customerRepository.GetCustomers(filter);
            }
            catch(CustomerManagerException ex)
            {
                throw new CustomerManagerException("GetCustomers");
            }
        }


   
        public Customerr GetCustomer(int? id)
        {
            try
            {
                return _customerRepository.GetCustomerById(id);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("AddCustomer", ex);
            }
        }

        public void AddCustomer(Customerr customer)
        {
            try
            {
                _customerRepository.AddCustomer(customer);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("AddCustomer", ex);
            }
        }




        public void UpdateCustomer(Customerr customer)
        {
            try
            {
                _customerRepository.UpdateCustomer(customer);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("UpdateCustomer", ex);
            }
        }

        public void DeleteCustomer(int id)
        {
            try
            {
                _customerRepository.DeleteCustomer(id);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("UpdateCustomer", ex);
            }
        }





    }
}
