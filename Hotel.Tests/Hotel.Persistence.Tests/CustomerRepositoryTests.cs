using Hotel.Domain.Interfaces;
using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using Hotel.Persistence.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Tests.Hotel.Persistence.Tests
{



    public class CustomerRepositoryTests
    {
        private Mock<ICustomerRepository> mockCustomerRepository;
        private CustomerManager customerManager;

        public CustomerRepositoryTests()
        {
            mockCustomerRepository = new Mock<ICustomerRepository>();
            customerManager = new CustomerManager(mockCustomerRepository.Object);
        }

        [Fact]
        public void Test_GetCustomers_InvalidFilter()
        {
            // Arrange
            string filter = "invalid filter";
            mockCustomerRepository.Setup(r => r.GetCustomers(filter)).Throws(new CustomerRepositoryException("GetCustomers"));

            // Act & Assert
            var ex = Assert.Throws<CustomerRepositoryException>(() => customerManager.GetCustomers(filter));
            Assert.IsType<CustomerRepositoryException>(ex);
        }


        [Fact]
        public void Test_AddCustomer_Valid()
        {
            // Arrange
            var address = new Address("City", "Street", "Zip", "HouseNumber");
            var contactInfo = new ContactInfo("john@example.com", "123456789", address);
            var newCustomer = new Customerr("John Doe", contactInfo);

            // Act
            customerManager.AddCustomer(newCustomer);

            // Assert
            mockCustomerRepository.Verify(r => r.AddCustomer(newCustomer), Times.Once);
        }


        [Fact]
        public void Test_GetCustomerById_Valid()
        {
            // Arrange
            int customerId = 1;
            var expectedCustomer = new Customerr(customerId, "John Doe", new ContactInfo("john@example.com", "123456789", new Address("City", "Street", "Zip", "HouseNumber")));
            mockCustomerRepository.Setup(r => r.GetCustomerById(customerId)).Returns(expectedCustomer);

            // Act
            var result = customerManager.GetCustomer(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCustomer.Id, result.Id);
            Assert.Equal(expectedCustomer.Name, result.Name);
            Assert.Equal(expectedCustomer.Contact.Email, result.Contact.Email);
            // Add other relevant assertions to verify the correct customer is retrieved
        }

        //[Fact]
        //public void Test_UpdateCustomer_Valid()
        //{
        //    // Arrange
        //    int customerId = 1;
        //    var address = new Address("UpdatedCity", "UpdatedStreet", "UpdatedZip", "UpdatedHouseNumber");
        //    var contactInfo = new ContactInfo("updated@example.com", "987654321", address);
        //    var updatedCustomer = new Customerr(customerId, "Updated Name", contactInfo);

        //    // Act
        //    customerManager.UpdateCustomer(updatedCustomer);

        //    // Assert
        //    mockCustomerRepository.Verify(r => r.UpdateCustomer(updatedCustomer), Times.Once);
        //    // Add other relevant assertions to verify the update is performed correctly
        //}

        [Fact]
        public void Test_DeleteCustomer_Valid()
        {
            // Arrange
            int customerId = 1;

            // Act
            customerManager.DeleteCustomer(customerId);

            // Assert
            mockCustomerRepository.Verify(r => r.DeleteCustomer(customerId), Times.Once);
            // Add other relevant assertions to verify the deletion is performed correctly
        }






    }
}
