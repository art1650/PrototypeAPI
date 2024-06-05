using Microsoft.AspNetCore.Mvc;
using PrototypeAPI.Client;
using PrototypeAPI.Model;

namespace PrototypeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class NameController : Controller
    {
        public class FavoriteRequest
        {
            public string Valcode { get; set; }
        }

        private readonly ILogger<DateCourseController> _logger;

        public NameController(ILogger<DateCourseController> logger)
        {
            _logger = logger;
        }
        [HttpGet(Name = "GetName")]
        public IActionResult ExchangeName(string Valcode)
        {
            try
            {
                string s = "20240602";
                Database db = new Database();
                CourseClients client = new CourseClients();
                List<DateCourse> nameValkode = client.GetCoursByDate(s, Valcode).Result;
                db.InsertDataCourseAsync(nameValkode[0], Valcode, s);
                return Ok(nameValkode[0].txt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching exchange rate.");
                return StatusCode(500, "Internal server error");
            }
           
        }

        [HttpPost(Name = "PostFavorite")]
        
        public IActionResult FavoriteCurrency([FromBody] FavoriteRequest request)
        {
            try
            {
                Database db = new Database();
                db.InsertFavoriteValcodeAsync(request.Valcode);
                return Ok("Ваша валюта була збережена");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while forecasting currency.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{valcode}", Name = "DeleteFavorite")]
        public IActionResult DeleteFavoriteCurrency(string valcode)
        {
            try
            {
                Database db = new Database();
                db.DeleteFavoriteValcodeAsync(valcode);
                return Ok("Ваша валюта була видалена");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting currency.");
                return StatusCode(500, "Internal server error");
            }
        }

        
    }


}
