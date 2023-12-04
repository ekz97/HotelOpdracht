using Hotel.Domain.Interfaces;
using Hotel.Persistence.Repositories;
using System.Configuration;

namespace Hotel.Util
{
    public static class RepositoryFactory
    {
    
        public static ICustomerRepository CustomerRepository { get { return new CustomerRepository(ConfigurationManager.ConnectionStrings["HotelDBAmir"].ConnectionString); } }
        public static IOrganiserRepository OrganiserRepository { get { return new OrganisorRepository(ConfigurationManager.ConnectionStrings["HotelDBAmir"].ConnectionString); } }
    }
}