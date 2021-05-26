namespace NewsAggregator.Core.Domains.DataSources
{
    public class DataSourceTopic
    {
        public string TopicName { get; set; }
        public int Nb { get; set; }

        public void Increment()
        {
            Nb++;
        }

        public static DataSourceTopic Create(string topicName)
        {
            return new DataSourceTopic
            {
                TopicName = topicName,
                Nb = 1
            };
        }
    }
}
