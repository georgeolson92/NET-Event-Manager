using EventManager.Models;
using Xunit;

namespace EventManager.Tests
{
    public class VenueTest
    {
        [Fact]
        public void GetNameTest()
        {
            //Arrange
            var item = new Venue();
            item.Name = "Roseland Theater";

            //Act
            var result = item.Name;

            //Assert
            Assert.Equal("Roseland Theater", result);
        }
    }
}