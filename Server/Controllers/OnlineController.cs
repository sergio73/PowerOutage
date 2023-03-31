using Microsoft.AspNetCore.Mvc;
using Quartz;
using Server.Jobs;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineController : ControllerBase
    {
        private readonly IStatusService _statusService;
        private readonly ISchedulerFactory _schedulerFactory;

        public OnlineController(IStatusService statusService, ISchedulerFactory schedulerFactory) 
        {
            _statusService = statusService;
            _schedulerFactory = schedulerFactory;
        }

        [HttpPost]
        public async Task Post()
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            _statusService.ReportOnline();
            await scheduler.TriggerJob(new JobKey(nameof(CheckStatusJob)));
        }
    }
}
