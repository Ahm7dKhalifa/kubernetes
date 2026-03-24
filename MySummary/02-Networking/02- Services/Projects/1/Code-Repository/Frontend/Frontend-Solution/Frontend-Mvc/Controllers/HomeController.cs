using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using Frontend_Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Frontend_Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var backendUrl = _configuration["BackendServiceUrl"];
            var model = new WeatherViewModel();

            try
            {
                var response = await _httpClient.GetAsync(backendUrl);
                
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    model.Forecasts = JsonSerializer.Deserialize<List<WeatherForecast>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    model.ErrorMessage = null;
                }
                else
                {
                    model.ErrorMessage = $"Failed to fetch data. Status: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Error: {ex.Message}";
                _logger.LogError(ex, "Failed to fetch weather data from {Url}", backendUrl);
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
