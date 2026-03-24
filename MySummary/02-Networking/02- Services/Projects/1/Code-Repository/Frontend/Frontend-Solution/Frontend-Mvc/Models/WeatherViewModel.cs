namespace Frontend_Mvc.Models
{
    public class WeatherViewModel
    {
        public List<WeatherForecast>? Forecasts { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
