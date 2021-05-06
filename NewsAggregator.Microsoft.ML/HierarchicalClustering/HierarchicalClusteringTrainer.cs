using Microsoft.ML.Data;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ML.Trainers
{
    public class HierarchicalClusteringTrainer : ITrainerEstimator<HierarchicalClusteringPredictionTransformer, HierarchicalModelParameters>
    {
        private readonly HierarchicalClustering.Options _options;

        public HierarchicalClusteringTrainer(HierarchicalClustering.Options options)
        {
            _options = options;
        }

        public TrainerInfo Info { get; }

        public HierarchicalClusteringPredictionTransformer Fit(IDataView input)
        {
            // Documentation :
            // http://eric.univ-lyon2.fr/~ricco/cours/slides/en/cah.pdf
            // https://online.stat.psu.edu/stat555/node/85/
            List<double[]> a = new List<double[]>();
            var clusters = new List<Cluster>();
            var vectorColumn = input.Schema[_options.VectorColumnName];
            int n = 0, i = 0;
            // Fetch data.
            using (var cursor = input.GetRowCursor(new[] { vectorColumn }))
            {
                VBuffer<double> cursorVectors = default;
                var cursorVectorsGetter = cursor.GetGetter<VBuffer<double>>(vectorColumn);
                while (cursor.MoveNext())
                {
                    cursorVectorsGetter(ref cursorVectors);
                    a.Add(cursorVectors.GetValues().ToArray());
                }
            }

            // Calculate similarity.
            var c = CalculateSimilarity(a);

            // Get clusters
            int nbNodes = 0;
            while (nbNodes < a.Count)
            {
                var coordinate = new KeyValuePair<int, int>();
                coordinate = Min(c);
                var firstElt = a.ElementAt(coordinate.Key);
                var secondElt = a.ElementAt(coordinate.Value);
                var existingCluster = clusters.FirstOrDefault(cl => cl.ContainsDatapoint(firstElt) || cl.ContainsDatapoint(secondElt));
                Cluster cluster;
                if (existingCluster != null)
                {
                    if (existingCluster.ContainsDatapoint(firstElt))
                    {
                        cluster = new Cluster(secondElt);
                    }
                    else
                    {
                        cluster = new Cluster(firstElt);
                    }

                    cluster.SetChild(existingCluster);
                    clusters.Add(cluster);
                    clusters.Remove(existingCluster);
                    nbNodes += 1;
                }
                else
                {
                    cluster = new Cluster(a.ElementAt(coordinate.Key), a.ElementAt(coordinate.Value));
                    clusters.Add(cluster);
                    nbNodes += 2;
                }

                cluster.SetSimilarity(c[coordinate.Key, coordinate.Value]);
                c = Remove(coordinate.Key, coordinate.Value, c);
            }

            // Corriger problème calcul sur les distances...
            var rootCluster = new RootCluster(clusters.ElementAt(0), clusters.ElementAt(1), _options.LinkageFunction, _options.DistanceMeasurement);
            return new HierarchicalClusteringPredictionTransformer(new HierarchicalModelParameters(rootCluster));
        }

        public SchemaShape GetOutputSchema(SchemaShape inputSchema)
        {
            return null;
        }

        public double[,] CalculateSimilarity(List<double[]> data)
        {
            // Documentation :
            // https://r-snippets.readthedocs.io/en/latest/real_analysis/metrics.html
            var nbRow = data.Count();
            double[,] c = new double[nbRow, nbRow];
            for(int i = 0; i < nbRow; i++)
            {
                var firstVector = data.ElementAt(i);
                for (int j = 0; j < nbRow; j++)
                {
                    var secondVector = data.ElementAt(j);
                    double distance = DistanceCalculationHelper.Calculate(_options.DistanceMeasurement, firstVector, secondVector);
                    c[i, j] = distance;
                }
            }

            return c;
        }

        public static KeyValuePair<int, int> Min(double[,] c)
        {
            double min = double.MaxValue;
            int x = 0, y = 0;
            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(0); j++)
                {
                    if (i != j && c[i, j] < min && c[i, j] != -1)
                    {
                        min = c[i, j];
                        x = i;
                        y = j;
                    }
                }
            }

            return new KeyValuePair<int, int>(x, y);
        }

        #region Linkage functions



        #endregion

        private static double[,] Remove(int row, int column, double[,] arr)
        {
            double[,] result = new double[arr.GetLength(0), arr.GetLength(1)];
            for (int i = 0, j = 0; i < arr.GetLength(0); i++)
            {
                for (int k = 0, u = 0; k < arr.GetLength(1); k++)
                {
                    if (k == column && i == row || k == row && i == column)
                    {
                        result[j, u] = -1;
                    }
                    else
                    {
                        result[j, u] = arr[i, k];
                    }

                    u++;
                }

                j++;
            }

            return result;
        }
    }
}
