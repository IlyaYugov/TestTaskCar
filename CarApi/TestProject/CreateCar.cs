using CarApi.Controllers;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TestProject
{
    public class CreateCar
    {
        [Fact]
        public void SimpleCreate()
        {
            // Arrange
            var carForCreate = MockInitializer.GetCar();
            var mock = new Mock<ICarRepository>();
            mock.Setup(repo=>repo.Create(carForCreate));
            var domain = new CarDomain(mock.Object);
            var controller = new CarController(domain);

            // Act
            var result = controller.Create(carForCreate);
            
            // Assert
            var viewResult = Assert.IsType<CreatedAtRouteResult>(result);
            var model = Assert.IsAssignableFrom<CarModel>(viewResult.Value);
            Assert.Equal(carForCreate, model);
        }
        [Fact]
        public void CreateFromPut()
        {
            // Arrange
            var inputModel = 
                @"
                {
                    ""Name"": ""string"",
                    ""Description"": ""string""
                }";
            var carForCreate = new CarModel
            {
                Id = "string",
                Name = "string",
                Description = "string"
            };
            
            var mock = new Mock<ICarRepository>();
            mock.Setup(repo=>repo.Create(carForCreate));
            var domain = new CarDomain(mock.Object);
            var controller = new CarController(domain);

            // Act
            var result = controller.Update(inputModel);
            
            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<CarModel>(viewResult.Value);
            Assert.True(carForCreate.Name == model.Name 
                        && carForCreate.Description == model.Description);
        }
    }
}