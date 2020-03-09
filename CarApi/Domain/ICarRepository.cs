using System.Collections.Generic;

namespace Domain
{
    public interface ICarRepository
    {
        List<CarModel> Get(int? skip, int? limit);
        CarModel Get(string id);
        CarModel Create(CarModel car);
        void Update(string id, CarModel carIn);
        void Remove(string id);
    }
}