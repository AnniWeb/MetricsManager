using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherManager.Entity;
using WeatherManager.Repository;

namespace WeatherManager.Controller
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
        /// <remarks>
        /// Пример запроса:
        /// POST /Todo
        /// {
        ///     "date": "2021-01-01",
        ///     "temperature": -10
        /// }
        /// </remarks>
        /// <param name="date">Дата и время наблюдения</param>
        /// <param name="temperature">Температура</param>
        /// <returns></returns>
        /// <response code="201">Данные сохранены</response>
        /// <response code="400">Ошибка сохранения</response>
        [HttpPost]
        public IActionResult Create([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            var weatherRow = new WeatherEntity {Temperature = temperature, Date = date};
            if (_repository.TryAdd(weatherRow))
            {
                return Created("Данные успешно сохранены", weatherRow);
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
