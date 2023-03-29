using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService) 
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task Post()
        {
            await _notificationService.NotifyPowerOutage();
        }
             
    }
}
