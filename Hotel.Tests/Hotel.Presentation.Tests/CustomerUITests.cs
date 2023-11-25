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
            var customerUI = new CustomerUI("John Doe", "john@example.com", new Address("City", "Street", "Zip", "HouseNumber"), "123456789", 0);
            var member = new Member("Member1", DateTime.Now);

            // Act
            customerUI.MemberList.Add(member);

            // Assert
            Assert.Single(customerUI.MemberList);
            Assert.Contains(member, customerUI.MemberList);
        }

  

        [Fact]
        public void CustomerUI_RemoveMember_Success()
        {
            // Arrange
            var customerUI = new CustomerUI("John Doe", "john@example.com", new Address("City", "Street", "Zip", "HouseNumber"), "123456789", 0);
            var member = new Member("Member1", DateTime.Now);
            customerUI.MemberList.Add(member);

            // Act
            customerUI.MemberList.Remove(member);

            // Assert
            Assert.Empty(customerUI.MemberList);
        }

       




        [Fact]
        public void CustomerUI_AddMember_Valid()
        {
            // Arrange
            var customerUI = new CustomerUI("John Doe", "john@example.com", new Address("City", "Street", "Zip", "HouseNumber"), "123456789", 0);
            var member = new Member("Member1", DateTime.Now);

            // Act
            customerUI.MemberList.Add(member);

            // Assert
            Assert.Contains(member, customerUI.MemberList);
        }

        [Fact]
        public void CustomerUI_RemoveMember_Valid()
        {
            // Arrange
            var customerUI = new CustomerUI("John Doe", "john@example.com", new Address("City", "Street", "Zip", "HouseNumber"), "123456789", 0);
            var member = new Member("Member1", DateTime.Now);
            customerUI.MemberList.Add(member);

            // Act
            customerUI.MemberList.Remove(member);

            // Assert
            Assert.DoesNotContain(member, customerUI.MemberList);
        }


   
    }
}
