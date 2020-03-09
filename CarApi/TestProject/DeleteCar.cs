using CarApi.Controllers;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TestProject
{
    public class DeleteCar
    {
        [Fact]
        public void SimpleDelete()
        {
            // Arrange
            var car = MockInitializer.GetCar();
            var mock = new Mock<ICarRepository>();
            mock.Setup(repo=>repo.Get(car.Id)).Returns(car);
            mock.Setup(repo=>repo.Remove(car.Id));
            var domain = new CarDomain(mock.Object);
            var controller = new CarController(domain);

            // Act
            var result = controller.Delete(car.Id);
            
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void NotFoundObjectForDelete()
        {
            // Arrange
            var car = MockInitializer.GetCar();
            var mock = new Mock<ICarRepository>();
            mock.Setup(repo=>repo.Get(car.Id)).Returns(car);
            mock.Setup(repo=>repo.Remove(car.Id));
            var domain = new CarDomain(mock.Object);
            var controller = new CarController(domain);

            // Act
            var result = controller.Delete(car.Id + "1");
            
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void DeleteCarDescription()
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
                Description = "123123",
                Name = carForGet.Name,
                Id = carForGet.Id,
            };
            var inputModel = 
                @"
                {
                    ""Id"": ""id"",
                    ""Description"": ""123123""
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
        public void SetNullToCarName()
        {
            // Arrange
            var carForGet = new CarModel
            {
                Name = "sdfsdgf",
                Id = "id",
            };
            var inputModel = 
                @"
                {
                    ""Id"": ""id"",
                    ""Name"": null
                }";
            
            var mock = new Mock<ICarRepository>();
            mock.Setup(repo=>repo.Get(carForGet.Id)).Returns(carForGet);
            mock.Setup(repo=>repo.Remove(carForGet.Id));
            var domain = new CarDomain(mock.Object);
            var controller = new CarController(domain);

            // Act
            var result = controller.Update(inputModel);
            
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void SetNullAllParameters()
        {
            // Arrange
            var carForGet = new CarModel
            {
                Description = "sdfdgfsd",
                Name = "sdfdgfsd",
                Id = "id",
            };
            var inputModel = 
                @"
                {
                    ""Id"": ""id"",
                    ""Description"": null,
                    ""Name"": null,
                }";
            
            var mock = new Mock<ICarRepository>();
            mock.Setup(repo=>repo.Get(carForGet.Id)).Returns(carForGet);
            mock.Setup(repo=>repo.Remove(carForGet.Id));
            var domain = new CarDomain(mock.Object);
            var controller = new CarController(domain);

            // Act
            var result = controller.Update(inputModel);
            
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void SetNullToCarDescription()
        {
            // Arrange
            var carForGet = new CarModel
            {
                Description = "sdfdgfsd",
                Id = "id",
            };
            var inputModel = 
                @"
                {
                    ""Id"": ""id"",
                    ""Description"": null
                }";
            
            var mock = new Mock<ICarRepository>();
            mock.Setup(repo=>repo.Get(carForGet.Id)).Returns(carForGet);
            mock.Setup(repo=>repo.Remove(carForGet.Id));
            var domain = new CarDomain(mock.Object);
            var controller = new CarController(domain);

            // Act
            var result = controller.Update(inputModel);
            
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

    }
}