using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGallery.Models;

namespace WebGallery.Controllers
{
    [Route("api/[controller]")]//запрос на сервер по api
    [ApiController]
    public class GirlsController : ControllerBase//название контролера
    {
        //[HttpGet]//без него swagger не идет
        //public IActionResult GetData()
        //{
        //    return Ok(new { Name = "Sveta", Age = 25 });
        //}
        //[HttpGet]
        //[Route("all")]//добавляем к маршруту для нового запроса
        //public IActionResult GetData1()
        //{
        //    return Ok(new { Name = "Kate", Age = 35 });
        //}

        [HttpGet]
        [Route("search")]
        public IActionResult SearchGrils()
        {
            var list = new List<GirlVM>()
            {
                new GirlVM
                {
                    Name = "Саша Маргалова",
                    Age=33,
                    Height=198,
                    Weight=95,
                    Image="1.jpg"
                },
                new GirlVM
                {
                    Name = "Наташа Ростова",
                    Age=18,
                    Height=172,
                    Weight=102,
                    Image = "2.jpg"
                },
                new GirlVM
                {
                    Name = "Маша Мишина",
                    Age=23,
                    Height=188,
                    Weight=60,
                    Image="3.jpg"
                },
            };
            return Ok(list);
        }
    }
}
