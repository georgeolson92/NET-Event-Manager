using EventManager.Models;
using Xunit;

namespace EventManager.Tests
{
    public class ItemTest
    {
        [Fact]
        public void GetTitleTest()
        {
            //Arrange
            var item = new Event();
            item.Title = "A band playing a show";

            //Act
            var result = item.Title;

            //Assert
            Assert.Equal("A band playing a show", result);
        }
    }
}