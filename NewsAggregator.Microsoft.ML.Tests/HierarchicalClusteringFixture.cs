using Microsoft.ML;
using Microsoft.ML.HierarchicalClustering;
using Microsoft.ML.Trainers;
using System;
using System.Collections.Generic;
using Xunit;

namespace NewsAggregator.Microsoft.ML.Tests
{
    public class HierarchicalClusteringFixture
    {
        [Fact]
        public void When_Calculate_Eucledian_Distance()
        {
            var vector1 = new double[] { 1 };
            var vector2 = new double[] { 3 };
            var result = DistanceCalculationHelper.CalculateEucledianDistance(vector1, vector2);
            Assert.Equal(2, result);
        }

        [Fact]
        public void When_Calculate_Manhattan_Distance()
        {
            var vector1 = new double[] { 1 };
            var vector2 = new double[] { 3 };
            var result = DistanceCalculationHelper.CalculateManhattanDistance(vector1, vector2);
            Assert.Equal(2, result);
        }

        [Fact]
        public void When_Calculate_Max_Distance()
        {
            var vector1 = new double[] { 1 };
            var vector2 = new double[] { 3 };
            var result = DistanceCalculationHelper.CalculateMaxDistance(vector1, vector2);
            Assert.Equal(2, result);

        }

        [Fact]
        public void When_Calculate_Canberra_Distance()
        {
            var vector1 = new double[] { 1 };
            var vector2 = new double[] { 3 };
            var result = DistanceCalculationHelper.CalculateCanberraDistance(vector1, vector2);
            Assert.Equal(0.5, result);
        }

        [Fact]
        public void When_Train_HierarchicalClusteringModel_Single_Eucledian()
        {
            var mlContext = new MLContext();
            var options = new Options
            {
                VectorColumnName = "NbTopics",
                LinkageFunction = LinkageFunctionTypes.Single,
                DistanceMeasurement = DistanceMeasurementTypes.Euclidian
            };
            var trainingData = mlContext.Data.LoadFromEnumerable(BuildArmyRepartitions());
            var pipeline = mlContext.Clustering.Trainers.HierarchicalClustering(options);
            var model = pipeline.Fit(trainingData);
            Assert.Equal(5, model.Model.Cluster.Distance);
        }

        [Fact]
        public void When_Train_HierarchicalClusteringModel_Complete_Eucledian()
        {
            var mlContext = new MLContext();
            var options = new Options
            {
                VectorColumnName = "NbTopics",
                LinkageFunction = LinkageFunctionTypes.Complete,
                DistanceMeasurement = DistanceMeasurementTypes.Euclidian
            };
            var trainingData = mlContext.Data.LoadFromEnumerable(BuildArmyRepartitions());
            var pipeline = mlContext.Clustering.Trainers.HierarchicalClustering(options);
            var model = pipeline.Fit(trainingData);
            Assert.Equal(7.81, Math.Round(model.Model.Cluster.Distance, 2));
        }

        private class TopicRepartition
        {
            public int Topic { get; set; }
            public double[] NbTopics { get; set; }
        }

        private static IEnumerable<TopicRepartition> BuildArmyRepartitions()
        {
            var result = new List<TopicRepartition>
            {
                new TopicRepartition { NbTopics = new double[] { 8, 3 } },
                new TopicRepartition { NbTopics = new double[] { 5, 3 } },
                new TopicRepartition { NbTopics = new double[] { 6, 4 } },
                new TopicRepartition { NbTopics = new double[] { 1, 6 } },
                new TopicRepartition { NbTopics = new double[] { 2, 8 } }
            };
            return result;
        }
    }
}
