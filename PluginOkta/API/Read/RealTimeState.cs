using System;

namespace PluginOkta.API.Read
{
    public class RealTimeState
    {
        public DateTime LastReadTime { get; set; } = DateTime.MinValue;
        public long JobVersion { get; set; } = 0;
    }
}