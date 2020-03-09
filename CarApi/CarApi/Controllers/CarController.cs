using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        public ActionResult<List<CarModel>> Get(int? skip, int? limit)
        {
            if (skip.HasValue && skip <= 0 || limit.HasValue && skip.HasValue && limit <= skip)
                return BadRequest();
            
            return _carDomain.Get(skip,limit);   
        }

        [HttpGet("{id:length(24)}", Name = "GetCar")]
        public ActionResult<CarModel> Get(string id)
        {
            var car = _carDomain.Get(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPost]
        public ActionResult<CarModel> Create(CarModel car)
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
            Dictionary<string, string> body = 
                JsonConvert.DeserializeObject<Dictionary<string, string>>(Convert.ToString(value));

            var isNameDefault = body
                                   .FirstOrDefault(b 
                                        => b.Key == nameof(CarModel.Name)).Key 
                                            == null;
            var isDescriptionDefault = body
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
            else if (carIn.Id == null)
                _carDomain.Create(carIn);
            else
            {
                var car = _carDomain.Get(carIn.Id);
            
                if (car == null)
                {
                    return NotFound();
                }
            
                                
                if(isNameDefault)
                    carIn.Name = null;
                if(isDescriptionDefault)
                    carIn.Description = null;
                _carDomain.Update(carIn.Id, carIn);
            }
            return NoContent();
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
        
        // private string GetDocumentContents()
        // {
        //     var tt = Request.BodyReader.ToJson();
        //     string documentContents;
        //     using (var reader = new StreamReader(Request.BodyReader.ToJson()))
        //     {
        //         documentContents = reader.ReadToEnd();
        //
        //         // Do something
        //     }
        //     return documentContents;
        // }
        private List<string> GetListOfStringsFromStream(Stream requestBody)
        {
            // Build up the request body in a string builder.
            StringBuilder builder = new StringBuilder();

            // Rent a shared buffer to write the request body into.
            byte[] buffer = ArrayPool<byte>.Shared.Rent(4096);

            while (true)
            {
                var bytesRemaining = requestBody.Read(buffer, offset: 0, buffer.Length);
                if (bytesRemaining == 0)
                {
                    break;
                }

                // Append the encoded string into the string builder.
                var encodedString = Encoding.UTF8.GetString(buffer, 0, bytesRemaining);
                builder.Append(encodedString);
            }

            ArrayPool<byte>.Shared.Return(buffer);

            var entireRequestBody = builder.ToString();

            // Split on \n in the string.
            return new List<string>(entireRequestBody.Split("\n"));
        }

    }
}