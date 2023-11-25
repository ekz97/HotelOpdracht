using Hotel.Domain.Model;
using Hotel.Presentation.Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Hotel.Tests.Hotel.Presentation.Tests
{
    public class CustomerUITests
    {
        [Fact]
        public void CustomerUI_ConstructorWithNameAndEmail_ShouldSetProperties()
        {
            // Arrange
            string name = "John Doe";
            string email = "john@example.com";
            Address address = new Address("City", "Street", "Zip", "HouseNumber");
            string phone = "123456789";
            int nrOfMembers = 0;

            // Act
            CustomerUI customerUI = new CustomerUI(name, email, address, phone, nrOfMembers);

            // Assert
            Assert.Equal(name, customerUI.Name);
            Assert.Equal(email, customerUI.Email);
            Assert.Equal(address, customerUI.Address);
            Assert.Equal(phone, customerUI.Phone);
            Assert.Equal(nrOfMembers, customerUI.NrOfMembers);
            Assert.NotNull(customerUI.MemberList);
            Assert.Empty(customerUI.MemberList);
        }

        [Fact]
        public void CustomerUI_ConstructorWithIdAndMembers_ShouldSetProperties()
        {
            // Arrange
            int? id = 1;
            string name = "John Doe";
            string email = "john@example.com";
            Address address = new Address("City", "Street", "Zip", "HouseNumber");
            string phone = "123456789";
            int nrOfMembers = 3;
            List<Member> members = new List<Member>
                {
                    new Member("Member1", DateTime.Now),
                    new Member("Member2", DateTime.Now),
                    new Member("Member3", DateTime.Now)
                };

            // Act
            CustomerUI customerUI = new CustomerUI(id, name, email, address, phone, nrOfMembers, members);

            // Assert
            Assert.Equal(id, customerUI.Id);
            Assert.Equal(name, customerUI.Name);
            Assert.Equal(email, customerUI.Email);
            Assert.Equal(address, customerUI.Address);
            Assert.Equal(phone, customerUI.Phone);
            Assert.Equal(nrOfMembers, customerUI.NrOfMembers);
            Assert.NotNull(customerUI.MemberList);
            Assert.Equal(members.Count, customerUI.MemberList.Count);

            // Validate member details
            for (int i = 0; i < nrOfMembers; i++)
            {
                Assert.Equal(members[i].Name, customerUI.MemberList[i].Name);
                Assert.Equal(members[i].Birthday, customerUI.MemberList[i].Birthday);
            }
        }


    }
}
