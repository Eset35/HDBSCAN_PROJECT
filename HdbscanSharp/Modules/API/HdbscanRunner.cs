using HdbscanSharp.Hdbscanstar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HdbscanSharp.Distance;

namespace HdbscanSharp.Runner
{
    public class HdbscanRunner
    {
        public static HdbscanResult Run<T>(HdbscanParameters<T> parameters)
        {
            var numPoints = parameters.DataSet?.Length ?? parameters.Distances.Length;

            PrecomputeNormalMatrixDistancesIfApplicable(parameters, numPoints);
            var sparseDistance = PrecomputeSparseMatrixDistancesIfApplicable(parameters, numPoints);
            var internalDistanceFunc = DetermineInternalDistanceFunc(parameters, sparseDistance, numPoints);

            // вычисляем дистанции
            var coreDistances = HdbscanAlgorithm.CalculateCoreDistances(
                internalDistanceFunc,
                numPoints,
                parameters.MinPoints);

            // строим минимальное остовное дерево
            var mst = HdbscanAlgorithm.ConstructMst(
                internalDistanceFunc,
                numPoints,
                coreDistances,
                true);
            mst.QuicksortByEdgeWeight();

            var pointNoiseLevels = new double[numPoints];
            var pointLastClusters = new int[numPoints];
            var hierarchy = new List<int[]>();

            // вычсиляем иерархию и кластерное дерево
            var clusters = HdbscanAlgorithm.ComputeHierarchyAndClusterTree(
                mst,
                parameters.MinClusterSize,
                hierarchy,
                pointNoiseLevels,
                pointLastClusters);

            // обрезание кластеров
            var infiniteStability = HdbscanAlgorithm.PropagateTree(clusters);
            var prominentClusters = HdbscanAlgorithm.FindProminentClusters(
                clusters,
                hierarchy,
                numPoints);

            // формирование результатов
            var scores = HdbscanAlgorithm.CalculateOutlierScores(
                clusters,
                pointNoiseLevels,
                pointLastClusters,
                coreDistances);

            return new HdbscanResult
            {
                Labels = prominentClusters,
                OutliersScore = scores,
                HasInfiniteStability = infiniteStability
            };
        }

        private static Func<int, int, double> DetermineInternalDistanceFunc<T>(HdbscanParameters<T> parameters,
            IReadOnlyDictionary<int, double> sparseDistance,
            int numPoints)
        {
            if (sparseDistance != null)
                return (a, b) =>
                {
                    if (a < b)
                        return sparseDistance.ContainsKey(a * numPoints + b) ? sparseDistance[a * numPoints + b] : 1;

                    return sparseDistance.ContainsKey(b * numPoints + a) ? sparseDistance[b * numPoints + a] : 1;
                };

            if (parameters.CacheDistance)
                return (a, b) => parameters.Distances[a][b];

            return (a, b) =>
                parameters.DistanceFunction.ComputeDistance(a, b, parameters.DataSet[a], parameters.DataSet[b]);
        }

        private static Dictionary<int, double> PrecomputeSparseMatrixDistancesIfApplicable<T>(
            HdbscanParameters<T> parameters, int numPoints)
        {
           return null;
        }

        private static void PrecomputeNormalMatrixDistancesIfApplicable<T>(HdbscanParameters<T> parameters, int numPoints)
        {
            if (parameters.Distances == null && parameters.CacheDistance && parameters.DataSet is double[][])
            {
                var distances = new double[numPoints][];
                for (var i = 0; i < distances.Length; i++)
                {
                    distances[i] = new double[numPoints];
                }

                if (parameters.MaxDegreeOfParallelism is 0 or > 1)
                {
                    var size = numPoints * numPoints;

                    var maxDegreeOfParallelism = parameters.MaxDegreeOfParallelism;
                    if (maxDegreeOfParallelism == 0)
                    {
                        maxDegreeOfParallelism = Environment.ProcessorCount;
                    }

                    var option = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = Math.Max(1, maxDegreeOfParallelism)
                    };

                    Parallel.For(0, size, option, index =>
                    {
                        var i = index % numPoints;
                        var j = index / numPoints;
                        if (i < j)
                        {
                            var distance = parameters.DistanceFunction.ComputeDistance(
                                i,
                                j,
                                parameters.DataSet[i],
                                parameters.DataSet[j]);
                            distances[i][j] = distance;
                            distances[j][i] = distance;
                        }
                    });
                }
                else
                {
                    for (var i = 0; i < numPoints; i++)
                    {
                        for (var j = 0; j < i; j++)
                        {
                            var distance = parameters.DistanceFunction.ComputeDistance(
                                i,
                                j,
                                parameters.DataSet[i],
                                parameters.DataSet[j]);
                            distances[i][j] = distance;
                            distances[j][i] = distance;
                        }
                    }
                }

                parameters.Distances = distances;
            }
        }
    }
}