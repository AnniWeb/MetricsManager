using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.Entity;
using MetricsManager.Repository;

namespace MetricsManager.Controller
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly WeatherRepository _repository;

        public WeatherController(WeatherRepository repository, ILogger<WeatherController> logger)
        {
            _logger = logger;
            _repository = repository;
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
            if (_repository.TryAdd(new WeatherEntity {Temperature = temperature, Date = date}))
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
            if (_repository.TryUpdate(new WeatherEntity {Temperature = temperature, Date = date}))
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
            
            if (_repository.Delete((DateTime) dateFrom, (DateTime) dateTo))
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
            return _repository.GetWeatherByPeriod(dateFrom, dateTo);
        }

    }
}
