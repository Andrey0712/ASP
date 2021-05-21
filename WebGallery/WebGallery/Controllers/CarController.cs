using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGallery.Entities;
using WebGallery.Entities.Data;
using WebGallery.Models;

namespace WebGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly EFDataContext _context;

        public CarsController(EFDataContext context)//внедрение зависимости через конструктор services.AddDbContext<EFDataContext>(opt=>opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));//подключение контекста
        {
            _context = context;
        }
        [HttpGet]
        [Route("search")]
        public IActionResult SearchCars()
        {
            var list = _context.Cars.ToList();
            return Ok(list);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddCar([FromBody]  CarViewModels car)
        {
            var rez = new Car
            {
            Mark = car.Mark,
            Model = car.Model,
            Image = car.Image,
            Fuel = car.Fuel,
            Сapacity = car.Сapacity,
            Year = car.Year

        };
           _context.Cars.Add(rez);
           _context.SaveChanges();
            return Ok(new { message = "Додано" });
        }

        
        [HttpDelete]
        [Route("del")]
        
        public IActionResult DeleteCar(long id)
        {
            var del_car = _context.Cars.FirstOrDefault(x => x.Id == id);

            if (del_car!= null)
            {
                _context.Cars.Remove(del_car);
            _context.SaveChanges();
               
            }

            return Ok(new { message = "Удалено" });

            
        }

        [HttpPut]
        [Route("edit")]
        public IActionResult Update( [FromBody] CarViewModels car, long id)
        {

            
            var res = _context.Cars.FirstOrDefault(x => x.Id == id);

            if (res != null)
            {
                
                res.Mark = car.Mark;
                res.Model = car.Model;
                res.Image = car.Image;
                res.Fuel = car.Fuel;
                res.Сapacity = car.Сapacity;
                res.Year = car.Year;
               
                _context.SaveChanges();
            }

            return Ok(new { result = $"Отредактированно автомобиль под ID № {id}" });
        }
    }
}
