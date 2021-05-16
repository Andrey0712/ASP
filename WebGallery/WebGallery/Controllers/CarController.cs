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
        public IActionResult AddCar([FromBody] Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
            return Ok(new { message = "Додано" });
        }
       
        #region Delete
        [HttpDelete("{id}")]
        [Route("del")]
        public IActionResult Delete(long id)
        {
            var del_item = _context.Cars.Find(id);

            if (del_item == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(del_item);
            _context.SaveChanges();

            return NoContent();
        }

        #endregion
    }
}
