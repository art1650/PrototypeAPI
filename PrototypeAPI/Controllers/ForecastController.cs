using Microsoft.AspNetCore.Mvc;
using PrototypeAPI.Client;
using PrototypeAPI.Model;

namespace PrototypeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForecastController : Controller
    {

        private readonly ILogger<DateCourseController> _logger;

        public ForecastController(ILogger<DateCourseController> logger)
        {
            _logger = logger;
        }
        [HttpGet(Name = "GetForecastt")]
        public IActionResult ForecastCurrency(string Date, string Valcode)
        {
            try
            {
                Database db = new Database();

                // Конвертація і корекція дати
                int Date1 = int.Parse(Date);
                Date1 = Date1 - 10000;
                string date1s = Date1.ToString();
                CourseClients client1 = new CourseClients();
                List<DateCourse> dateCourse1 = client1.GetCoursByDate(date1s, Valcode).Result;

                int Date2 = int.Parse(Date);
                Date2 = Date2 - 9999;
                string date2s = Date2.ToString();
                CourseClients client2 = new CourseClients();
                List<DateCourse> dateCourse2 = client2.GetCoursByDate(date2s, Valcode).Result;

                string result;
                if (dateCourse1[0].rate < dateCourse2[0].rate)
                {
                    result = "За нашими даними курс завтра виросте";
                }
                else
                {
                    result = "За нашими прогнозами завтра курс впаде";
                }

                db.InsertDataCourseAsync(dateCourse1[0], Valcode, Date);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while forecasting currency.");
                return StatusCode(500, "Internal server error");
            }
        }



    }
}
