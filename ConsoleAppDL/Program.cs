using Hotel.Domain.Model;
using Hotel.Persistence.Repositories;

namespace ConsoleAppDL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string conn = "Data Source=localhost;Initial Catalog=HotelDonderdag;Integrated Security=True;TrustServerCertificate=true";
            CustomerRepository repo = new CustomerRepository(conn);
            ////var x = repo.GetCustomers("ge");
            //Customer c = new Customer("Bart", new ContactInfo("bart@yahoo", "013456", new Address("Gent", "Kerkstraat", "9000", "185")));
            //c.AddMember(new Member("paul", new DateOnly(2000, 5, 8)));
            //c.AddMember(new Member("rudy", new DateOnly(1987, 1, 1)));
            //repo.AddCustomer(c);

            var customer = repo.GetCustomers("bart");


            foreach(var p in customer)
            {
                Console.WriteLine(p.Name.ToString());
                Console.WriteLine(p.Id.ToString());
                IReadOnlyList<Member> members = p.GetMembers();
                for (int i=0; i < members.Count; i++)
                {
                    Console.WriteLine(members[i].Name.ToString());
                    Console.WriteLine(members[i].Birthday.ToString());
                  

                }
            }

            OrganisorRepository organisorRepository = new OrganisorRepository(conn);

            var organisers = organisorRepository.GetOrganisers();


            foreach(var o in organisers)
            {
                Console.WriteLine(o.Name.ToString());
                Console.WriteLine(o.Id.ToString());
    
            }
            
           


        }
    }
}