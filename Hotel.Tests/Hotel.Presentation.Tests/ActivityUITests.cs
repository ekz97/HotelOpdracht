 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Tests.Hotel.Presentation.Tests
{
    public class ActivityUITests
    {
        [Fact]
        public void ActivityUI_ConstructWithFixtureAndPlaces_Success()
        {
            // Arrange
            string fixture = "Fixture1";
            int nrOfPlaces = 10;

            // Act
            //var activityUI = new ActivityUI(fixture, nrOfPlaces);

            // Assert
            //Assert.Equal(fixture, activityUI.Fixture);
            //Assert.Equal(nrOfPlaces, activityUI.NrOfPlaces);
            //Assert.Null(activityUI.Description);
            //Assert.Null(activityUI.PriceInfo);
        }

        // Test om te controleren of PropertyChanged event wordt getriggerd voor Fixture
        [Fact]
        public void ActivityUI_SetFixture_PropertyChangedEventTriggered()
        {
            // Arrange
            //var activityUI = new ActivityUI("Fixture3", 8);
            bool eventTriggered = false;
            //activityUI.PropertyChanged += (sender, args) => { eventTriggered = true; };

            //// Act
            //activityUI.Fixture = "UpdatedFixture";

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void ActivityUI_SetNrOfPlaces_PropertyChangedEventTriggered()
        {
            // Arrange
            //var activityUI = new ActivityUI("Fixture4", 5);
            bool eventTriggered = false;
            //activityUI.PropertyChanged += (sender, args) => { eventTriggered = true; };

            //// Act
            //activityUI.NrOfPlaces = 10;

            // Assert
            Assert.True(eventTriggered);
        }


        [Fact]
        public void ActivityUI_ConstructWithDescriptionAndPriceInfo_Success()
        {
            //// Arrange
            //var descriptionUI = new DescriptionUI(/* initialisatie indien nodig */);
            //var priceInfoUI = new PriceInfoUI(/* initialisatie indien nodig */);

            // Act
            //var activityUI = new ActivityUI(1, "Fixture", 5, descriptionUI, priceInfoUI);

            //// Assert
            //Assert.Equal(descriptionUI, activityUI.Description);
            //Assert.Equal(priceInfoUI, activityUI.PriceInfo);
        }
    }
}
