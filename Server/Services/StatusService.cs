﻿using Newtonsoft.Json;
using Server.Dtos;
using System.Globalization;

namespace Server.Services
{
    public interface IStatusService
    {
        void ReportOffline();
        void ReportOnline();
        void SetUserAlerted(bool alerted);
        StatusDto GetStatus();
    }

    public class StatusService : IStatusService
    {
        private ISettings _settings;

        private object _lock = new object();

        public StatusService(ISettings settings)
        {
            _settings = settings;
        }

        public void ReportOnline()
        {
            UpdateStatus(status => status.LastTimeOnline = DateTime.Now);
        }

        public void SetUserAlerted(bool alerted)
        {
            UpdateStatus(status => status.UserAlerted = alerted);
        }

        public void ReportOffline()
        {
            UpdateStatus(status => status.LastTimeOffline = DateTime.Now);
        }

        public StatusDto GetStatus()
        {
            lock (_lock)
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
        }

        private void UpdateStatus(Action<StatusDto> updateFunc) 
        {
            var currentStatus = GetStatus();
            updateFunc(currentStatus);
            WriteStatus(currentStatus);
        }

        private void WriteStatus(StatusDto statusDto)
        {
            lock (_lock)
            {
                var json = JsonConvert.SerializeObject(statusDto);
                File.WriteAllText(_settings.StatusFileName, json);
            }
        }
    }
}
