using EventManager.Models;
using Xunit;

namespace EventManager.Tests
{
    public class ItemTest
    {
        [Fact]
        public void GetDescriptionTest()
        {
            //Arrange
            var item = new Event();

            //Act
            item.Description = "Wash the dog";
            var result = item.Description;

            //Assert
            Assert.Equal("Wash the dog", result);
        }
    }
}