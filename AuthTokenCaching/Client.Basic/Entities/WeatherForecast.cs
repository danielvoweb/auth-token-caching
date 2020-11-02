using System;

namespace Client.Basic.Entities
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public int Centigrade { get; set; }
        public int Fahrenheit { get; set; }
        public string Summary { get; set; }
    }
}