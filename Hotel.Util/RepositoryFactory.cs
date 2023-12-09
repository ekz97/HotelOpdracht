using Hotel.Domain.Interfaces;
using Hotel.Persistence.Repositories;
using System.Configuration;

namespace Hotel.Util
{
    public static class RepositoryFactory
    {
        public static ICustomerRepository CustomerRepository { get { return new CustomerRepository(ConfigurationManager.ConnectionStrings["HotelDBLiam"].ConnectionString); } }
        public static IOrganiserRepository OrganiserRepository { get { return new OrganisorRepository(ConfigurationManager.ConnectionStrings["HotelDBLiam"].ConnectionString); } }
        public static IActivityRepository ActivityRepository { get { return new ActivityRepository(ConfigurationManager.ConnectionStrings["HotelDBLiam"].ConnectionString); } }
        public static IRegistrationRepository RegistrationRepository { get { return new RegistrationRepository(ConfigurationManager.ConnectionStrings["HotelDBLiam"].ConnectionString); } }
    }
}