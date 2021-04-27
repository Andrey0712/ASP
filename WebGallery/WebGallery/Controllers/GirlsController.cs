using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGallery.Controllers
{
    [Route("api/[controller]")]//запрос на сервер по api
    [ApiController]
    public class GirlsController : ControllerBase//название контролера
    {
        public IActionResult GetData()
        {
            return Ok(new { Name = "Sveta", Age = 25 });
        }

        [Route("all")]//добавляем к маршруту для нового запроса
        public IActionResult GetData1()
        {
            return Ok(new { Name = "Kate", Age = 35 });
        }
    }
}
