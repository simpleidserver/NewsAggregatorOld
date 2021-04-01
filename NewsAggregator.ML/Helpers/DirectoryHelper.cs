using System.Collections.Generic;
using System.IO;

namespace NewsAggregator.ML.Helpers
{
    public static class DirectoryHelper
    {
        public static void CreateFile(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        public static string GetDirectoryPath()
        {
            return Path.GetDirectoryName(typeof(DirectoryHelper).Assembly.Location);
        }

        public static string GetLDAArticles(string language)
        {
            return Path.Combine(GetDirectoryPath(), $"articles-lda-{language}.zip");
        }

        public static string GetWordEmbeddingZipFilePath(string language)
        {
            return Path.Combine(GetDirectoryPath(), $"wordEmbedding-{language}.zip");
        }

        public static string GetWordEmbeddingFilePath(string language)
        {
            return Path.Combine(GetDirectoryPath(), $"wordEmbedding-{language}.vec");
        }

        public static string GetTrainedWordEmbeddingFilePath(string language)
        {
            return Path.Combine(GetDirectoryPath(), $"wordEmbeddingTrained-{language}.zip");
        }

        public static string GetCSVSessionFilePath(string personId, string sessionId)
        {
            return Path.Combine(GetDirectoryPath(), $"session-{personId}-{sessionId}.csv");
        }

        public static IEnumerable<SessionFileInformation> GetSessionFiles()
        {
            var files = Directory.GetFiles(GetDirectoryPath(), "session-*.csv");
            foreach(var file in files)
            {
                var fileName = Path.GetFileName(file);
                fileName = fileName.Replace(".csv", "");
                var splitted = fileName.Split('-');
                yield return new SessionFileInformation(splitted[1], splitted[2], file, File.GetCreationTime(file));
            }
        }

        public static string GetFilePath(string fileName)
        {
            return Path.Combine(GetDirectoryPath(), fileName);
        }
    }
}
