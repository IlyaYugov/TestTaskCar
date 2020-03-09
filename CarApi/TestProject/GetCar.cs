using System.Collections.Generic;
using CarApi.Controllers;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TestProject
{
    public class GetCar
    {
        [Fact]
        public void Get()
        {
            // Arrange
            string id = "id";
            var mock = new Mock<ICarRepository>();
            mock.Setup(repo=>repo.Get(id)).Returns(MockInitializer.GetCar());
            var domain = new CarDomain(mock.Object);
            var controller = new CarController(domain);

            // Act
            var result = controller.Get(id);
            
            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<CarModel>(viewResult.Value);
            Assert.Equal(MockInitializer.GetCar(), model);
        }
        [Fact]
        public void NotFoundCar()
        {
            // Arrange
            string id = "id";
            string id2 = "id2";
            var mock = new Mock<ICarRepository>();
            mock.Setup(repo=>repo.Get(id)).Returns(MockInitializer.GetCar());
            var domain = new CarDomain(mock.Object);
            var controller = new CarController(domain);

            // Act
            var result = controller.Get(id2);
            
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}