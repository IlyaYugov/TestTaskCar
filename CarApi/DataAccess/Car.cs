using Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; }
        
        public static explicit operator Car(CarModel carModel)
        {
            return new Car
            {
                Id = carModel.Id,
                Name = carModel.Name,
                Description = carModel.Description
            };
        }
        public static explicit operator CarModel(Car carModel)
        {
            return new CarModel
            {
                Id = carModel.Id,
                Name = carModel.Name,
                Description = carModel.Description
            };
        }
    }
}