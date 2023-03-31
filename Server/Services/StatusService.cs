using Newtonsoft.Json;
using Server.Dtos;
using System.Globalization;

namespace Server.Services
{
    public interface IStatusService
    {
        void ReportOffline();
        void ReportOnline();
        StatusDto GetStatus();
    }

    public class StatusService : IStatusService
    {
        private ISettings _settings;

        public StatusService(ISettings settings)
        {
            _settings = settings;
        }

        public void ReportOnline()
        {
            UpdateStatus(status => {
                status.LastTimeOnline = DateTime.Now;
                status.UserAlerted = false;
            });
        }

        public void ReportOffline()
        {
            UpdateStatus(status => {
                status.LastTimeOffline = DateTime.Now;
                status.UserAlerted = true;
            });
        }

        public StatusDto GetStatus()
        {
            if (!File.Exists(_settings.StatusFileName))
            {
                return new StatusDto();
            }

            var json = File.ReadAllText(_settings.StatusFileName);

            if (string.IsNullOrEmpty(json))
            {
                return new StatusDto();
            }

            return JsonConvert.DeserializeObject<StatusDto>(json) ?? throw new JsonException("Not possible to deserialize status.json");
        }

        private void UpdateStatus(Action<StatusDto> updateFunc) 
        {
            var currentStatus = GetStatus();
            updateFunc(currentStatus);
            WriteStatus(currentStatus);
        }

        private void WriteStatus(StatusDto statusDto)
        {
            var json = JsonConvert.SerializeObject(statusDto);
            File.WriteAllText(_settings.StatusFileName, json);
        }
    }
}
