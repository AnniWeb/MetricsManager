using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using MetricsManager.Entity;

namespace MetricsManager.Repository
{
    public class WeatherRepository
    {
        protected readonly string _file = "weather_date.json";
        protected List<WeatherEntity> _weather = new List<WeatherEntity>();

        public WeatherRepository()
        {
            ReadData();
        }

        protected void ReadData()
        {
            if (!File.Exists(_file))
            {
                UpdateData();
            }
            var data = File.ReadAllText(_file);
            _weather = JsonSerializer.Deserialize<List<WeatherEntity>>(data);
        }

        protected void UpdateData()
        {
            var data = JsonSerializer.Serialize(_weather);
            File.WriteAllText(_file, data);
        }

        protected WeatherEntity? Find(WeatherEntity findWeather)
        {
            return _weather.Find(el => findWeather.Date == el.Date);
        }

        public List<WeatherEntity> GetWeatherByPeriod(DateTime? dateFrom, DateTime? dateTo)
        {
            return _weather.Where(
                el => (dateFrom == null || el.Date >= dateFrom) 
                      && (dateTo == null || el.Date <= dateTo)
            ).OrderBy(el => el.Date).ToList();
        }
        
        public bool Delete(DateTime dateFrom, DateTime dateTo)
        {
            _weather.RemoveAll(el => (el.Date >= dateFrom && el.Date <= dateTo)); 
            UpdateData();
            return true;
        }

        public bool Add(WeatherEntity weather)
        {
            if (!weather.IsValid() || Find(weather) != null)
            {
                return false;
            }
            _weather.Add(weather);
            UpdateData();
            return true;
        }
        
        public bool Update(WeatherEntity weather)
        {
            var weatherOld = Find(weather);
            if (weather.IsValid() && weatherOld != null)
            {
                _weather.Remove(weatherOld);
                _weather.Add(weather);
                UpdateData();
                return true;
            }
            return false;
        }
    }
}