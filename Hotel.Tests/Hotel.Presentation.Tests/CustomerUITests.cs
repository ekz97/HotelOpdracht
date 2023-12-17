using Hotel.Domain.Model;
using Hotel.Presentation.Customer.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace Hotel.Tests.Hotel.Presentation.Tests
{
    public class CustomerUITests
    {
        [Fact]
        public void CustomerUI_AddMember_Success()
        {
            // Arrange
            
            var customerUI = new CustomerUI("John Doe", "john@example.com","City Street Zip HouseNumber", "123456789", 0);
            var member = new MemberUI("Member1", DateTime.Now);
            customerUI.Members = new List<MemberUI>();

            // Act
            customerUI.Members.Add(member);

            // Assert
            Assert.Single(customerUI.Members);
            Assert.Contains(member, customerUI.Members);
        }

  

        [Fact]
        public void CustomerUI_RemoveMember_Success()
        {
            // Arrange
            var customerUI = new CustomerUI("John Doe", "john@example.com", "City Street Zip HouseNumber", "123456789", 0);
            var member = new MemberUI("Member1", DateTime.Now);
            customerUI.Members = new List<MemberUI>();

            // Act
            customerUI.Members.Add(member);
            customerUI.Members.Remove(member);

            // Assert
            Assert.Empty(customerUI.Members);

            
        }


       










    }
}
