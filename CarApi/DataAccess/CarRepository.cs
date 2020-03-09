using System.Collections.Generic;
using System.Linq;
using Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess
{
    public class CarRepository : ICarRepository
    {
        private readonly IMongoCollection<Car> _cars;

        public CarRepository(ICarstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _cars = database.GetCollection<Car>(settings.CarsCollectionName);
        }

        public List<CarModel> Get(int? skip, int? limit)
        {
            limit ??= 100;
            var carModels = _cars
                .Find(car => true)
                .Skip(skip)
                .Limit(limit)
                .ToList()
                .Select(c => (CarModel) c)
                .ToList();
            
            return carModels;
        }

        public CarModel Get(string id)
        {
            var car =_cars
                .Find(c => c.Id == id).FirstOrDefault();
            return (CarModel)car;
        }

        public CarModel Create(CarModel car)
        {
            if (car.Id == null)
                car.Id = ObjectId.GenerateNewId().ToString();
            _cars.InsertOne((Car)car);
            return car;
        }

        public CarModel Update(string id, CarModel carIn)
        {
            _cars.ReplaceOne(car => car.Id == id, (Car)carIn);
            return carIn;
        }
            

        public void Remove(string id) => 
            _cars.DeleteOne(car => car.Id == id);
    }
}