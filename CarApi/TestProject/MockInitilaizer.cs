using System.Collections.Generic;
using Domain;

namespace TestProject
{
    public static class MockInitializer
    {
        public static List<CarModel> GetCarList()
        {
            var cars = new List<CarModel>();
            for (int i = 0; i < 15; i++)
            {
                cars.Add(new CarModel
                {
                    Id = i.ToString(),
                    Description = i.ToString() + i.ToString(),
                    Name = i.ToString() + i.ToString() + i.ToString()
                });
            }

            return cars;
        }
        
        public static CarModel GetCar()
        {
            var car = new CarModel
            {
                Description = "sdfdgfsd",
                Name = "sdfsdgf",
                Id = "sfdgdff"
            };
            return car;
        }
    }
}