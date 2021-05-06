using Microsoft.ML.HierarchicalClustering;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ML.Trainers
{
    public class Cluster
    {
        public Cluster(double[] firstDatapoint)
        {
            FirstDatapoint = firstDatapoint;
        }

        public Cluster(double[] firstDatapoint, double[] secondDatapoint)
        {
            FirstDatapoint = firstDatapoint;
            SecondDatapoint = secondDatapoint;
        }

        public double[] FirstDatapoint { get; set; }
        public double[] SecondDatapoint { get; set; }
        public Cluster Child { get; set; }
        public double Similarity { get; set; }
        public double Distance { get; set; }

        public List<double[]> GetAllDatapoints()
        {
            var result = new List<double[]>
            {
                FirstDatapoint
            };
            if (SecondDatapoint != null)
            {
                result.Add(SecondDatapoint);
            }
            else
            {
                result.AddRange(Child.GetAllDatapoints());
            }

            return result;
        }

        public bool ContainsDatapoint(double[] datapoint)
        {
            return datapoint.SequenceEqual(FirstDatapoint) ||
                (SecondDatapoint != null && datapoint.SequenceEqual(SecondDatapoint)) ||
                (Child != null && Child.ContainsDatapoint(datapoint));
        }

        public void SetChild(Cluster child)
        {
            Child = child;
        }

        public void SetSimilarity(double similarity)
        {
            Similarity = similarity;
        }

        public void ComputeDistance(LinkageFunctionTypes linkageFunction, DistanceMeasurementTypes distanceMeasurement)
        {
            if (Child == null)
            {
                Distance = LinkageFunctionHelper.Calculate(linkageFunction, distanceMeasurement, new List<double[]> { FirstDatapoint }, new List<double[]> { SecondDatapoint }); ;
                return;
            }

            Distance = LinkageFunctionHelper.Calculate(linkageFunction, distanceMeasurement, new List<double[]> { FirstDatapoint }, Child.GetAllDatapoints());
            Child.ComputeDistance(linkageFunction, distanceMeasurement);
        }
    }
}
