using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarApi.Models;
using CarApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public ActionResult<List<Car>> Get() =>
            _carService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCar")]
        public ActionResult<Car> Get(string id)
        {
            var car = _carService.Get(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPost]
        public ActionResult<Car> Create(Car car)
        {
            _carService.Create(car);

            return CreatedAtRoute("GetCar", new { id = car.Id.ToString() }, car);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Car varIn)
        {
            var book = _carService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _carService.Update(id, varIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var car = _carService.Get(id);

            if (car == null)
            {
                return NotFound();
            }

            _carService.Remove(car.Id);

            return NoContent();
        }
    }
}