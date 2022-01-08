using System;
using System.Collections.Generic;
using System.Linq;
using HdbscanSharp.Utils;

namespace HdbscanSharp.Hdbscanstar
{
	public class HdbscanAlgorithm
	{
		//Вычисление корневой дистанции для каждой точки
		public static double[] CalculateCoreDistances(
			Func<int, int, double> distances,
			int numPoints,
			int k)
		{
			var numNeighbors = k - 1;
			var coreDistances = new double[numPoints];

			if (k == 1)
			{
				for (var point = 0; point < numPoints; point++)
				{
					coreDistances[point] = 0;
				}
				return coreDistances;
			}

			for (var point = 0; point < numPoints; point++)
			{
				var kNNDistances = new double[numNeighbors];  
				for (var i = 0; i < numNeighbors; i++)
				{
					kNNDistances[i] = double.MaxValue;
				}

				for (var neighbor = 0; neighbor < numPoints; neighbor++)
				{
					if (point == neighbor)
						continue;

                    var distance = distances(point, neighbor);
                    
					var neighborIndex = numNeighbors;
					while (neighborIndex >= 1 && distance < kNNDistances[neighborIndex - 1])
					{
						neighborIndex--;
					}

					if (neighborIndex < numNeighbors)
					{
						for (var shiftIndex = numNeighbors - 1; shiftIndex > neighborIndex; shiftIndex--)
						{
							kNNDistances[shiftIndex] = kNNDistances[shiftIndex - 1];
						}
						kNNDistances[neighborIndex] = distance;
					}
				}
				coreDistances[point] = kNNDistances[numNeighbors - 1];
			}
			return coreDistances;
		}

        // Строим минимальное остовное дерево
        public static UndirectedGraph ConstructMst(
	        Func<int, int, double> distances,
	        int numPoints,
			double[] coreDistances,
			bool selfEdges)
		{
			var selfEdgeCapacity = 0;
			if (selfEdges)
				selfEdgeCapacity = numPoints;

			var attachedPoints = new BitSet();

			var nearestMRDNeighbors = new int[numPoints - 1 + selfEdgeCapacity];
			var nearestMRDDistances = new double[numPoints - 1 + selfEdgeCapacity];

			for (var i = 0; i < numPoints - 1; i++)
			{
				nearestMRDDistances[i] = double.MaxValue;
			}

			var currentPoint = numPoints - 1;
			var numAttachedPoints = 1;
			attachedPoints.Set(numPoints - 1);

			while (numAttachedPoints < numPoints)
			{
				var nearestMRDPoint = -1;
				var nearestMRDDistance = double.MaxValue;

				for (var neighbor = 0; neighbor < numPoints; neighbor++)
				{
					if (currentPoint == neighbor)
						continue;

					if (attachedPoints.Get(neighbor))
						continue;

                    var distance = distances(currentPoint, neighbor);
					var mutualReachabiltiyDistance = distance;

					if (coreDistances[currentPoint] > mutualReachabiltiyDistance)
						mutualReachabiltiyDistance = coreDistances[currentPoint];

					if (coreDistances[neighbor] > mutualReachabiltiyDistance)
						mutualReachabiltiyDistance = coreDistances[neighbor];

					if (mutualReachabiltiyDistance < nearestMRDDistances[neighbor])
					{
						nearestMRDDistances[neighbor] = mutualReachabiltiyDistance;
						nearestMRDNeighbors[neighbor] = currentPoint;
					}

					if (nearestMRDDistances[neighbor] <= nearestMRDDistance)
					{
						nearestMRDDistance = nearestMRDDistances[neighbor];
						nearestMRDPoint = neighbor;
					}
				}

				attachedPoints.Set(nearestMRDPoint);
				numAttachedPoints++;
				currentPoint = nearestMRDPoint;
			}

			var otherVertexIndices = new int[numPoints - 1 + selfEdgeCapacity];
			for (var i = 0; i < numPoints - 1; i++)
			{
				otherVertexIndices[i] = i;
			}

			if (selfEdges)
			{
				for (var i = numPoints - 1; i < numPoints * 2 - 1; i++)
				{
					var vertex = i - (numPoints - 1);
					nearestMRDNeighbors[i] = vertex;
					otherVertexIndices[i] = vertex;
					nearestMRDDistances[i] = coreDistances[vertex];
				}
			}

			return new UndirectedGraph(numPoints, nearestMRDNeighbors, otherVertexIndices, nearestMRDDistances);
		}


		//построение иерархити и кластерного дерева
		public static List<Cluster> ComputeHierarchyAndClusterTree(
			UndirectedGraph mst,
			int minClusterSize,
			List<int[]> hierarchy,
			double[] pointNoiseLevels,
			int[] pointLastClusters)
		{
			int hierarchyPosition = 0;

			var currentEdgeIndex = mst.GetNumEdges() - 1;
			var nextClusterLabel = 2;
			var nextLevelSignificant = true;

			var previousClusterLabels = new int[mst.GetNumVertices()];
			var currentClusterLabels = new int[mst.GetNumVertices()];

			for (var i = 0; i < currentClusterLabels.Length; i++)
			{
				currentClusterLabels[i] = 1;
				previousClusterLabels[i] = 1;
			}

			var clusters = new List<Cluster>();
			clusters.Add(null);
			clusters.Add(new Cluster(1, null, double.NaN, mst.GetNumVertices()));

			var clusterOne = new SortedSet<int>();
			clusterOne.Add(1);
			CalculateNumConstraintsSatisfied(
				clusterOne,
				clusters,
				currentClusterLabels);

			var affectedClusterLabels = new SortedSet<int>();
			var affectedVertices = new SortedSet<int>();

			while (currentEdgeIndex >= 0)
			{
				var currentEdgeWeight = mst.GetEdgeWeightAtIndex(currentEdgeIndex);
				var newClusters = new List<Cluster>();

				while (currentEdgeIndex >= 0 && mst.GetEdgeWeightAtIndex(currentEdgeIndex) == currentEdgeWeight)
				{
					var firstVertex = mst.GetFirstVertexAtIndex(currentEdgeIndex);
					var secondVertex = mst.GetSecondVertexAtIndex(currentEdgeIndex);
					mst.GetEdgeListForVertex(firstVertex).Remove(secondVertex);
					mst.GetEdgeListForVertex(secondVertex).Remove(firstVertex);

					if (currentClusterLabels[firstVertex] == 0)
					{
						currentEdgeIndex--;
						continue;
					}
					affectedVertices.Add(firstVertex);
					affectedVertices.Add(secondVertex);
					affectedClusterLabels.Add(currentClusterLabels[firstVertex]);
					currentEdgeIndex--;
				}

				if (!affectedClusterLabels.Any())
					continue;

				while (affectedClusterLabels.Any())
				{
					var examinedClusterLabel = affectedClusterLabels.Last();
					affectedClusterLabels.Remove(examinedClusterLabel);

					var examinedVertices = new SortedSet<int>();

					foreach (var vertex in affectedVertices.ToList())
					{
						if (currentClusterLabels[vertex] == examinedClusterLabel)
						{
							examinedVertices.Add(vertex);
							affectedVertices.Remove(vertex);
						}
					}

					SortedSet<int> firstChildCluster = null;
					LinkedList<int> unexploredFirstChildClusterPoints = null;
					var numChildClusters = 0;

					while (examinedVertices.Any())
					{
						var constructingSubCluster = new SortedSet<int>();
						var unexploredSubClusterPoints = new LinkedList<int>();
						var anyEdges = false;
						var incrementedChildCount = false;
						var rootVertex = examinedVertices.Last();
						constructingSubCluster.Add(rootVertex);
						unexploredSubClusterPoints.AddLast(rootVertex);
						examinedVertices.Remove(rootVertex);

						while (unexploredSubClusterPoints.Any())
						{
							var vertexToExplore = unexploredSubClusterPoints.First();
							unexploredSubClusterPoints.RemoveFirst();

							foreach (var neighbor in mst.GetEdgeListForVertex(vertexToExplore))
							{
								anyEdges = true;
								if (constructingSubCluster.Add(neighbor))
								{
									unexploredSubClusterPoints.AddLast(neighbor);
									examinedVertices.Remove(neighbor);
								}
							}

							if (!incrementedChildCount && constructingSubCluster.Count >= minClusterSize && anyEdges)
							{
								incrementedChildCount = true;
								numChildClusters++;

								if (firstChildCluster == null)
								{
									firstChildCluster = constructingSubCluster;
									unexploredFirstChildClusterPoints = unexploredSubClusterPoints;
									break;
								}
							}
						}

						if (numChildClusters >= 2 && constructingSubCluster.Count >= minClusterSize && anyEdges)
						{
							var firstChildClusterMember = firstChildCluster.Last();
							if (constructingSubCluster.Contains(firstChildClusterMember))
								numChildClusters--;
							else
							{
								var newCluster = CreateNewCluster(constructingSubCluster, currentClusterLabels,
										clusters[examinedClusterLabel], nextClusterLabel, currentEdgeWeight);
								newClusters.Add(newCluster);
								clusters.Add(newCluster);
								nextClusterLabel++;
							}
						}
						else if (constructingSubCluster.Count < minClusterSize || !anyEdges)
						{
							CreateNewCluster(constructingSubCluster, currentClusterLabels,
									clusters[examinedClusterLabel], 0, currentEdgeWeight);

							foreach (var point in constructingSubCluster)
							{
								pointNoiseLevels[point] = currentEdgeWeight;
								pointLastClusters[point] = examinedClusterLabel;
							}
						}
					}
					if (numChildClusters >= 2 && currentClusterLabels[firstChildCluster.First()] == examinedClusterLabel)
					{
						while (unexploredFirstChildClusterPoints.Any())
						{
							var vertexToExplore = unexploredFirstChildClusterPoints.First();
							unexploredFirstChildClusterPoints.RemoveFirst();
							foreach (var neighbor in mst.GetEdgeListForVertex(vertexToExplore))
							{
								if (firstChildCluster.Add(neighbor))
									unexploredFirstChildClusterPoints.AddLast(neighbor);
							}
						}
						var newCluster = CreateNewCluster(firstChildCluster, currentClusterLabels,
								clusters[examinedClusterLabel], nextClusterLabel, currentEdgeWeight);
						newClusters.Add(newCluster);
						clusters.Add(newCluster);
						nextClusterLabel++;
					}
				}

				if (nextLevelSignificant || newClusters.Any())
				{
					int[] lineContents = new int[previousClusterLabels.Length];
					for (var i = 0; i < previousClusterLabels.Length; i++)
						lineContents[i] = previousClusterLabels[i];
					hierarchy.Add(lineContents);
					hierarchyPosition++;
				}

				var newClusterLabels = new SortedSet<int>();
				foreach (var newCluster in newClusters)
				{
					newCluster.HierarchyPosition = hierarchyPosition;
					newClusterLabels.Add(newCluster.Label);
				}

				if (newClusterLabels.Any())
					CalculateNumConstraintsSatisfied(newClusterLabels, clusters, currentClusterLabels);

				for (var i = 0; i < previousClusterLabels.Length; i++)
				{
					previousClusterLabels[i] = currentClusterLabels[i];
				}

				if (!newClusters.Any())
					nextLevelSignificant = false;
				else
					nextLevelSignificant = true;
			}

			{
				int[] lineContents = new int[previousClusterLabels.Length + 1];
				for (var i = 0; i < previousClusterLabels.Length; i++)
					lineContents[i] = 0;
				hierarchy.Add(lineContents);
			}
			
			return clusters;
		}

		// Проверка стабильности
		public static bool PropagateTree(List<Cluster> clusters)
		{
			var clustersToExamine = new SortedDictionary<int, Cluster>();
			var addedToExaminationList = new BitSet();
			var infiniteStability = false;

			foreach (var cluster in clusters)
			{
				if (cluster != null && !cluster.HasChildren)
				{
					var label = cluster.Label;
					clustersToExamine.Remove(label);
					clustersToExamine.Add(label, cluster);
					addedToExaminationList.Set(label);
				}
			}

			while (clustersToExamine.Any())
			{
				var currentKeyValue = clustersToExamine.Last();
				var currentCluster = currentKeyValue.Value;
				clustersToExamine.Remove(currentKeyValue.Key);

				currentCluster.Propagate();

				if (currentCluster.Stability == double.PositiveInfinity)
					infiniteStability = true;

				if (currentCluster.Parent != null)
				{
					var parent = currentCluster.Parent;
					var label = parent.Label;

					if (!addedToExaminationList.Get(label))
					{
						clustersToExamine.Remove(label);
						clustersToExamine.Add(label, parent);
						addedToExaminationList.Set(label);
					}
				}
			}

			return infiniteStability;
		}

		//Дает плоский результат кластеризации, используя соответствие ограничениям и стабильность кластера, и возвращает массив меток. Перед вызовом этого метода необходимо вызвать функцию PropagateTree().
		public static int[] FindProminentClusters(
			List<Cluster> clusters,
			List<int[]> hierarchy,
			int numPoints)
		{
			var solution = clusters[1].PropagatedDescendants;

			var flatPartitioning = new int[numPoints];

			var significantHierarchyPositions = new SortedDictionary<int, List<int>>();

			foreach (var cluster in solution)
			{
				var hierarchyPosition = cluster.HierarchyPosition;
				if (significantHierarchyPositions.ContainsKey(hierarchyPosition))
					significantHierarchyPositions[hierarchyPosition].Add(cluster.Label);
				else
					significantHierarchyPositions[hierarchyPosition] = new List<int> { cluster.Label };
			}

			while (significantHierarchyPositions.Any())
			{
				var entry = significantHierarchyPositions.First();
				significantHierarchyPositions.Remove(entry.Key);

				var clusterList = entry.Value;
				var hierarchyPosition = entry.Key;
				var lineContents = hierarchy[hierarchyPosition];
				
				for (var i = 0; i < lineContents.Length; i++)
				{
					var label = lineContents[i];
					if (clusterList.Contains(label))
						flatPartitioning[i] = label;
				}
			}
			return flatPartitioning;
		}

		public static List<OutlierScore> CalculateOutlierScores(
			List<Cluster> clusters,
			double[] pointNoiseLevels,
			int[] pointLastClusters,
			double[] coreDistances)
		{
			var numPoints = pointNoiseLevels.Length;
			var outlierScores = new List<OutlierScore>(numPoints);

			for (var i = 0; i < numPoints; i++)
			{
				var epsilonMax = clusters[pointLastClusters[i]].PropagatedLowestChildDeathLevel;
				var epsilon = pointNoiseLevels[i];
				double score = 0;

				if (epsilon != 0)
					score = 1 - (epsilonMax / epsilon);

				outlierScores.Add(new OutlierScore(score, coreDistances[i], i));
			}

			outlierScores.Sort();

			return outlierScores;
		}

		private static Cluster CreateNewCluster(
			SortedSet<int> points,
			int[] clusterLabels,
			Cluster parentCluster,
			int clusterLabel,
			double edgeWeight)
		{
			foreach (var point in points)
			{
				clusterLabels[point] = clusterLabel;
			}

			parentCluster.DetachPoints(points.Count, edgeWeight);

			if (clusterLabel != 0)
				return new Cluster(clusterLabel, parentCluster, edgeWeight, points.Count);
			
			parentCluster.AddPointsToVirtualChildCluster(points);
			return null;
		}

		private static void CalculateNumConstraintsSatisfied(
			SortedSet<int> newClusterLabels,
			List<Cluster> clusters,
			int[] clusterLabels)
		{
				return;
		}
	}
}
