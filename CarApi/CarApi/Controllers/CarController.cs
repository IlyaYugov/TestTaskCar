using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly CarDomain _carDomain;

        public CarController(CarDomain carDomain)
        {
            _carDomain = carDomain;
        }

        [HttpGet]
        public IActionResult Get(int? skip, int? limit)
        {
            if (skip <= 0 || limit <= 0)
                return BadRequest();
            
            return Ok(_carDomain.Get(skip,limit));   
        }

        [HttpGet("{id:length(24)}", Name = "GetCar")]
        public IActionResult Get(string id)
        {
            var car = _carDomain.Get(id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpPost]
        public IActionResult Create(CarModel car)
        {
            _carDomain.Create(car);

            return CreatedAtRoute("GetCar", new { id = car.Id }, car);
        }

        /// <summary>
        /// Request exapmle
        ///{
        ///"Id": "string",
        ///"Name": "string",
        ///"Description": "string"
        /// }
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update([FromBody]dynamic value)
        {
            //Parsing data
            Dictionary<string, string> body = 
                JsonConvert.DeserializeObject<Dictionary<string, string>>(Convert.ToString(value));

            var isNameNotForEdit = body
                                   .FirstOrDefault(b 
                                        => b.Key == nameof(CarModel.Name)).Key 
                                            == null;
            var isDescriptionNotForEdit = body
                                    .FirstOrDefault(b 
                                        => b.Key == nameof(CarModel.Description)).Key 
                                            == null;
            var carIn = new CarModel
            {
                Id = body.FirstOrDefault(b=>b.Key == nameof(CarModel.Id)).Value,
                Name = body.FirstOrDefault(b=>b.Key == nameof(CarModel.Name)).Value,
                Description = body.FirstOrDefault(b=>b.Key == nameof(CarModel.Description)).Value
            };
            
            
            if (carIn.Id == null && carIn.Name == null && carIn.Description == null)
                return BadRequest();
            else if (carIn.Id == null) //Work like Create
            {
                var result = _carDomain.Create(carIn);
                return Ok(carIn);
            }
                
            else 
            {
                var car = _carDomain.Get(carIn.Id);
            
                if (car == null)
                {
                    return NotFound();
                }

                if(!isNameNotForEdit)
                    car.Name = carIn.Name;
                if(!isDescriptionNotForEdit)
                    car.Description = carIn.Description;
                
                if (car.Name == null && car.Description == null) //Work like Remove
                {
                    _carDomain.Remove(carIn.Id);
                    return NoContent();
                }

                _carDomain.Update(car.Id, car); //Work like Update
                return Ok(car); 
            }
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var car = _carDomain.Get(id);

            if (car == null)
            {
                return NotFound();
            }

            _carDomain.Remove(car.Id);

            return NoContent();
        }
    }
}