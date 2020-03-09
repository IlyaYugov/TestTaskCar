using CarApi.Controllers;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TestProject
{
    public class SetNullParameters
    {
        [Fact]
        public void SetNullToCarName()
        {
            // Arrange
            var carForGet = new CarModel
            {
                Description = "sdfdgfsd",
                Name = "sdfsdgf",
                Id = "id",
            };
            var resCar = new CarModel
            {
                Description = carForGet.Description,
                Name = null,
                Id = carForGet.Id,
            };
            var inputModel = 
                @"
                {
                    ""Id"": ""id"",
                    ""Name"": null
                }";
            
            var mock = new Mock<ICarRepository>();
            mock.Setup(repo=>repo.Get(carForGet.Id)).Returns(carForGet);
            mock.Setup(repo=>repo.Update(carForGet.Id,resCar));
            var domain = new CarDomain(mock.Object);
            var controller = new CarController(domain);

            // Act
            var result = controller.Update(inputModel);
            
            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<CarModel>(viewResult.Value);
            Assert.Equal(resCar, model);
        }
        [Fact]
        public void SetNullToCarDescription()
        {
            // Arrange
            var carForGet = new CarModel
            {
                Description = "sdfdgfsd",
                Name = "sdfsdgf",
                Id = "id",
            };
            var resCar = new CarModel
            {
                Description = null,
                Name = carForGet.Name,
                Id = carForGet.Id,
            };
            var inputModel = 
                @"
                {
                    ""Id"": ""id"",
                    ""Description"": null
                }";
            
            var mock = new Mock<ICarRepository>();
            mock.Setup(repo=>repo.Get(carForGet.Id)).Returns(carForGet);
            mock.Setup(repo=>repo.Update(carForGet.Id,resCar));
            var domain = new CarDomain(mock.Object);
            var controller = new CarController(domain);

            // Act
            var result = controller.Update(inputModel);
            
            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<CarModel>(viewResult.Value);
            Assert.Equal(resCar, model);
        }
    }
}