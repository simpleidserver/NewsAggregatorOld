using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NewsAggregator.ML.Helpers
{
    public class CSVFileReader
    {
        public bool TryGetRecord<T>(string filePath, Func<T, bool> callback, out T result, int pagination = 500, char separator = ',') where T : class
        {
            result = null;
            if(!File.Exists(filePath))
            {
                return false;
            }

            foreach(var records in Read<T>(filePath, pagination, separator))
            {
                result = records.FirstOrDefault(rec => callback(rec));
                if (result != null)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<ICollection<T>> Read<T>(string filePath, int pagination = 500, char separator = ',') where T: class
        {
            var result = new List<T>();
            using (var file = new StreamReader(filePath))
            {
                string line;
                int nb = 0;
                while((line = file.ReadLine()) != null)
                {
                    var splitted = line.Split(separator);
                    result.Add(Extract<T>(splitted));
                    if (nb % pagination == 0)
                    {
                        nb++;
                        yield return result;
                    }

                    nb++;
                }
            }

            yield return result;
        }

        private static T Extract<T>(IEnumerable<string> cells) where T : class
        {
            var properties = typeof(T).GetProperties();
            var result = Activator.CreateInstance(typeof(T));
            foreach(var property in properties)
            {
                var attributes = property.GetCustomAttributes(true);
                var attribute = attributes.FirstOrDefault(a => a is LoadColumnAttribute);
                if (attribute == null)
                {
                    continue;
                }

                var loadColumnAttribute = attribute as LoadColumnAttribute;
                var sourcesField = typeof(LoadColumnAttribute).GetFields(
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
                    .First(p => p.Name == "Sources");
                var source = (sourcesField.GetValue(loadColumnAttribute) as List<TextLoader.Range>).First();
                property.SetValue(result, cells.ElementAt(source.Min));
            }

            return (T)result;
        }
    }
}