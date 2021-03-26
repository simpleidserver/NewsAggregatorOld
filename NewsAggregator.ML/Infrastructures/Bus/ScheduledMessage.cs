using System;

namespace NewsAggregator.ML.Infrastructures.Bus
{
    public class ScheduledMessage
    {
        public string QueueName { get; set; }
        public string SerializedContent { get; set; }
        public DateTime ElapsedTime { get; set; }
    }
}
