using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebGallery.Models
{
    public class CarViewModels
    {
        [Required(ErrorMessage = "Неуказанна марка автомобиля")]
        public string Mark { get; set; }
        /// <summary>
        /// модель
        /// </summary>
        [Required(ErrorMessage = "Неуказанна модель автомобиля")]
        public string Model { get; set; }
        /// <summary>
        /// Рік випуску
        /// </summary>
        [Range(1950, 2021, ErrorMessage = "Не допустиме значення для року автомобіля")]
        public int Year { get; set; }
        /// <summary>
        /// Пальне
        /// </summary>
        [Required(ErrorMessage = "Укажите тип топлива")]
        public string Fuel { get; set; }
        /// <summary>
        /// Об'єм
        /// </summary>
        [Required(ErrorMessage = "Укажите объем двигателя")]
        public float Сapacity { get; set; }
        [StringLength(255)]
        public string Image { get; set; }
    }
}
