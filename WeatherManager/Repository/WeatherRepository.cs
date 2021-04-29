using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using WeatherManager.Entity;

namespace WeatherManager.Repository
{
    public class WeatherRepository
    {
        private readonly string _file = "weather_date.json";
        private List<WeatherEntity> _weather = new List<WeatherEntity>();

        public WeatherRepository()
        {
            ReadData();
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

        public bool TryAdd(WeatherEntity weather)
        {
            if (!weather.IsValid() || Find(weather) != null)
            {
                return false;
            }
            _weather.Add(weather);
            UpdateData();
            return true;
        }
        
        public bool TryUpdate(WeatherEntity weather)
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

        private void ReadData()
        {
            if (!File.Exists(_file))
            {
                UpdateData();
            }
            var data = File.ReadAllText(_file);
            _weather = JsonSerializer.Deserialize<List<WeatherEntity>>(data);
        }

        private void UpdateData()
        {
            var data = JsonSerializer.Serialize(_weather);
            File.WriteAllText(_file, data);
        }

        private WeatherEntity? Find(WeatherEntity findWeather)
        {
            return _weather.Find(el => findWeather.Date == el.Date);
        }
    }
}