using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EventManager.Controllers;
using EventManager.Models;
using Xunit;

namespace EventManager.Tests
{
    public class EventsControllerTest
    {
        [Fact]
        public void Get_ViewResult_Index_Test()
        {
            //Arrange
            EventsController controller = new EventsController();

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Get_ModelList_Index_Test()
        {
            //Arrange
            ViewResult indexView = new EventsController().Index() as ViewResult;

            //Act
            var result = indexView.ViewData.Model;

            //Assert
            Assert.IsType<List<Event>>(result);
        }
    }
}