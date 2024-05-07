using Microsoft.AspNetCore.Mvc;
using PrototypeAPI.Client;
using PrototypeAPI.Model;

namespace PrototypeAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class DateCourseController : ControllerBase
    {
        private readonly ILogger<DateCourseController> _logger;

        public DateCourseController (ILogger<DateCourseController> logger)
        {
            _logger = logger;
        }
        [HttpGet(Name = "GetRate")]
        public float ExchangeRate(string Date, string Valcode)
        {
            CourseClients client = new CourseClients();
            List<DateCourse> dateCourse = client.GetCoursByDate(Date, Valcode).Result;
            return dateCourse[0].rate;
        }
    }
}
