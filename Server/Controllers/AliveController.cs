using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AliveController : ControllerBase
    {
        private readonly ISettings _settings;

        public AliveController(ISettings settings) 
        {
            _settings = settings;
        }

        [HttpPost]
        public void Post()
        {
            System.IO.File.WriteAllText(_settings.AliveFileName, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
        }
    }
}
