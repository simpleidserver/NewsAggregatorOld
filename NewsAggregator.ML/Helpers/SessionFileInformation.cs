using System;

namespace NewsAggregator.ML.Helpers
{
    public class SessionFileInformation
    {
        public SessionFileInformation(string personId, string sessionId, string filePath,  DateTime createDateTime)
        {
            PersonId = personId;
            SessionId = sessionId;
            FilePath = filePath;
            CreateDateTime = createDateTime;
        }

        public string PersonId { get; private set; }
        public string SessionId { get; private set; }
        public string FilePath { get; private set; }
        public DateTime CreateDateTime { get; private set; }
    }
}
