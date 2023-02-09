using Entities;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddOrEdit([Bind("EmployeeId,FullName,EmpCode,Position,OfficeLocation")] Customer employee)
		{
			if (ModelState.IsValid)
			{
				if (employee.CustomerId == 0)
				//	_context.Add(employee);
				//else
				//	_context.Update(employee);
				//await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return Ok(employee);
		}
	}
}