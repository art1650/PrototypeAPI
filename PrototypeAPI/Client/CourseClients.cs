using Newtonsoft.Json;
using PrototypeAPI.Model;

namespace PrototypeAPI.Client
{
    public class CourseClients
    {
        private static string _address;
        public HttpClient _client;

        public CourseClients()
        {
            _address = Const.Address;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
        }

        public async Task<List<DateCourse>> GetCoursByDate(string Date, string Valcode)
        {
            HttpResponseMessage responce = await _client.GetAsync($"/NBUStatService/v1/statdirectory/exchange?valcode={Valcode}&date={Date}&json");
            responce.EnsureSuccessStatusCode();
            string content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<DateCourse>>(content);
            return result;
        }
    }
}
