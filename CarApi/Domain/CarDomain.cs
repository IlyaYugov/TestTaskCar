using System.Collections.Generic;

namespace Domain
{
    public class CarDomain
    {
        private readonly ICarRepository _carRepository;

        public CarDomain(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public List<CarModel> Get(int? skip, int? limit) => _carRepository.Get(skip,limit);

        public CarModel Get(string id) => _carRepository.Get(id);

        public CarModel Create(CarModel car) => _carRepository.Create(car);

        public CarModel Update(string id, CarModel carIn)
        {
            return _carRepository.Update(id, carIn);
        }

        public void Remove(CarModel carIn) => Remove(carIn.Id);
        public void Remove(string id) => _carRepository.Remove(id);
    }
}