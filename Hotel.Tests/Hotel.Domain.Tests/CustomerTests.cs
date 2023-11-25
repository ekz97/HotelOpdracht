using Hotel.Domain.Exceptions;
using Hotel.Domain.Model;
using System;
using Xunit;

namespace Hotel.Tests.Hotel.Domain.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void Customerr_ValidConstruction()
        {
            // Arrange
            var contactInfo = new ContactInfo("john@example.com", "123456789", new Address("City", "Street", "Zip", "HouseNumber"));

            // Act
            var customer = new Customerr("John Doe", contactInfo);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal("John Doe", customer.Name);
            Assert.Equal(contactInfo, customer.Contact);
            Assert.Empty(customer.GetMembers());
        }

        [Fact]
        public void Customerr_InvalidName_Null()
        {
            // Arrange
            var contactInfo = new ContactInfo("john@example.com", "123456789", new Address("City", "Street", "Zip", "HouseNumber"));

            // Act & Assert
            Assert.Throws<CustomerException>(() => new Customerr(null, contactInfo));
        }

        [Fact]
        public void Customerr_InvalidName_TooLong()
        {
            // Arrange
            var contactInfo = new ContactInfo("john@example.com", "123456789", new Address("City", "Street", "Zip", "HouseNumber"));
            var longName = new string('a', 501); // Name longer than 500 characters

            // Act & Assert
            Assert.Throws<CustomerException>(() => new Customerr(longName, contactInfo));
        }

        [Fact]
        public void Customerr_AddMember_Success()
        {
            // Arrange
            var contactInfo = new ContactInfo("john@example.com", "123456789", new Address("City", "Street", "Zip", "HouseNumber"));
            var customer = new Customerr("John Doe", contactInfo);
            var member = new Member("MemberName", DateTime.Now);

            // Act
            customer.AddMember(member);

            // Assert
            Assert.Contains(member, customer.GetMembers());
        }

        [Fact]
        public void Customerr_AddMember_Duplicate()
        {
            // Arrange
            var contactInfo = new ContactInfo("john@example.com", "123456789", new Address("City", "Street", "Zip", "HouseNumber"));
            var customer = new Customerr("John Doe", contactInfo);
            var member = new Member("MemberName", DateTime.Now);

            // Act
            customer.AddMember(member);

            // Assert
            Assert.Throws<CustomerException>(() => customer.AddMember(member));
        }

        [Fact]
        public void Customerr_RemoveMember_Success()
        {
            // Arrange
            var contactInfo = new ContactInfo("john@example.com", "123456789", new Address("City", "Street", "Zip", "HouseNumber"));
            var customer = new Customerr("John Doe", contactInfo);
            var member = new Member("MemberName", DateTime.Now);
            customer.AddMember(member);

            // Act
            customer.RemoveMember(member);

            // Assert
            Assert.DoesNotContain(member, customer.GetMembers());
        }

        [Fact]
        public void Customerr_RemoveMember_NotExists()
        {
            // Arrange
            var contactInfo = new ContactInfo("john@example.com", "123456789", new Address("City", "Street", "Zip", "HouseNumber"));
            var customer = new Customerr("John Doe", contactInfo);
            var member = new Member("MemberName", DateTime.Now);

            // Act & Assert
            Assert.Throws<CustomerException>(() => customer.RemoveMember(member));
        }
    }
}
