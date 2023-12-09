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
    public class OrganiserManager
    {
        private IOrganiserRepository _organiserRepository;


        public OrganiserManager(IOrganiserRepository organiserRepository)
        {
            _organiserRepository = organiserRepository;
        }

        public IReadOnlyList<Organiser> GetOrganisers()
        {
            try
            {
                return _organiserRepository.GetOrganisers();
            }
            catch (OrganiserManagerException ex)
            {
                throw new OrganiserManagerException("GetCustomers",ex);
            }
        }

    }
}
