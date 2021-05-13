using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebGallery.Entities.Data
{
   
        /// <summary>
        /// Автомобіль
        /// </summary>
        [Table("tblCars")]
        public class Car
        {
            [Key]
            public long Id { get; set; }
            /// <summary>
            /// марка
            /// </summary>
            [Required, StringLength(255)]
            public string Mark { get; set; }
            /// <summary>
            /// модель
            /// </summary>
            [Required, StringLength(255)]
            public string Model { get; set; }
            /// <summary>
            /// Рік випуску
            /// </summary>
            public int Year { get; set; }
            /// <summary>
            /// Пальне
            /// </summary>
            [Required, StringLength(255)]
            public string Fuel { get; set; }
            /// <summary>
            /// Об'єм
            /// </summary>
            public float Сapacity { get; set; }
            [StringLength(255)]
            public string Image { get; set; }
        }
   
}
