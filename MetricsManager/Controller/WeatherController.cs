using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.Entity;
using MetricsManager.Repository;

namespace MetricsManager.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(ILogger<WeatherController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Возможность сохранить температуру в указанное время
        /// </summary>
        /// <param name="date">Дата и время наблюдения</param>
        /// <param name="temperature">Температура</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            var data = new WeatherRepository();
            if (data.Add(new WeatherEntity {Temperature = temperature, Date = date}))
            {
                return Ok("Данные успешно сохранены");
            }
            return BadRequest("Не удалось сохранить");
        }
        
        /// <summary>
        /// Возможность отредактировать показатель температуры в указанное время
        /// </summary>
        /// <param name="date">Дата и время наблюдения</param>
        /// <param name="temperature">Температура</param>
        /// <returns></return
        [HttpPut]
        public IActionResult Update([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            var data = new WeatherRepository();
            if (data.Update(new WeatherEntity {Temperature = temperature, Date = date}))
            {
                return Ok("Данные успешно обновлены");
            }
            return BadRequest("Не удалось обновить данные");
        }
        
        /// <summary>
        /// Возможность удалить показатель температуры в указанный промежуток времени
        /// </summary>
        /// <param name="dateFrom">Начало периода</param>
        /// <param name="dateTo">Окончание периода</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            
            if (dateFrom == null || dateTo == null)
            {
                return BadRequest("Некорректный диапазон");
            }
            
            if (dateFrom > dateTo)
            {
                return BadRequest("Некорректный диапазон");
            }

            
            var data = new WeatherRepository();
            if (data.Delete((DateTime) dateFrom, (DateTime) dateTo))
            {
                return Ok("Данные успешно удалены");
            }
            return BadRequest("Не удалось удалить данные");
        }
        
        /// <summary>
        /// Возможность прочитать список показателей температуры за указанный промежуток времени
        /// </summary>
        /// <param name="dateFrom">Начало периода</param>
        /// <param name="dateTo">Окончание периода</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherEntity> Get([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            var data = new WeatherRepository();
            return data.GetWeatherByPeriod(dateFrom, dateTo);
        }

    }
}
