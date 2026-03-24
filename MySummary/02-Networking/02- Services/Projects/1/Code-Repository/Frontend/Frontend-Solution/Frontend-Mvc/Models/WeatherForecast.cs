namespace Frontend_Mvc.Models
{
    public class WeatherForecast
    {
        public string Date { get; set; } = string.Empty;
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string? Summary { get; set; }
    }
}
