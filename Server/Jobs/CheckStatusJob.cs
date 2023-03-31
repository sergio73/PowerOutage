using Quartz;
using Server.Services;

namespace Server.Jobs
{
    public class CheckStatusJob : IJob
    {
        private INotificationService _notificationService;
        private IStatusService _onlineService;
        private ISettings _settings;

        public CheckStatusJob(INotificationService notificationService, IStatusService onlineService, ISettings settings)
        {
            _notificationService = notificationService;
            _onlineService = onlineService;
            _settings = settings;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var status = _onlineService.GetStatus();

            if (status.UserAlerted)
            {
                if (!status.LastTimeOnline.HasValue || !status.LastTimeOffline.HasValue)
                {
                    return;
                }

                var shouldAlert = status.LastTimeOnline > status.LastTimeOffline;
                if (shouldAlert)
                {
                    _onlineService.SetUserAlerted(false);
                    await _notificationService.NotifyPowerRestore();
                }
            }
            else
            {
                if (!status.LastTimeOnline.HasValue)
                {
                    return;
                }
                var shouldAlert = (DateTime.Now - status.LastTimeOnline).Value.TotalMinutes > _settings.TimeToAlert;
                if (shouldAlert)
                {
                    _onlineService.ReportOffline();
                    _onlineService.SetUserAlerted(true);

                    await _notificationService.NotifyPowerOutage();
                }
            }
        }
    }
}
