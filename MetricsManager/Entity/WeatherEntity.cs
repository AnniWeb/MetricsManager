using System;

namespace MetricsManager.Entity
{
    public class WeatherEntity
    {
        private DateTime _date;
        private int _temperature;

        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }

        public int Temperature
        {
            get => _temperature;
            set => _temperature = value;
        }

        public bool IsValid()
        {
            return _date != null && _date != new DateTime(0) && _temperature != null; 
        }
    }
}