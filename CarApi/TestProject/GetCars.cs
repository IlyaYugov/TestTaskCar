using System;
using System.Collections.Generic;
using CarApi.Controllers;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TestProject
{
    public class GetCars
    {
        [Fact]
        public void GetAll()
        {
            // Arrange
            var mock = new Mock<ICarRepository>();
            mock.Setup(repo=>repo.Get(null,null)).Returns(MockInitializer.GetCarList());
            var domain = new CarDomain(mock.Object);
            var controller = new CarController(domain);

            // Act
            var result = controller.Get(null, null);
            
            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<List<CarModel>>(viewResult.Value);
            Assert.Equal(MockInitializer.GetCarList().Count, model.Count);
        }
        
        [Fact]
        public void SkipLessThanZero()
        {
            // Arrange
            int? skip = -1;
            int? limit = 1;
            var mock = new Mock<ICarRepository>();
            mock.Setup(repo=>repo.Get(skip,limit)).Returns(MockInitializer.GetCarList);
            var domain = new CarDomain(mock.Object);
            var controller = new CarController(domain);

            // Act
            var result = controller.Get(skip, limit);
            
            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public void LimitLessThanZero()
        {
            // Arrange
            int? skip = 1;
            int? limit = -1;
            var mock = new Mock<ICarRepository>();
            mock.Setup(repo=>repo.Get(skip,limit)).Returns(MockInitializer.GetCarList);
            var domain = new CarDomain(mock.Object);
            var controller = new CarController(domain);

            // Act
            var result = controller.Get(skip, limit);
            
            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}