using System.Collections.Generic;

namespace HdbscanSharp.Hdbscanstar
{
	public class UndirectedGraph
	{
		private readonly int _numVertices;
		private readonly int[] _verticesA;
		private readonly int[] _verticesB;
		private readonly double[] _edgeWeights;
		private readonly List<int>[] _edges;

		public UndirectedGraph(int numVertices, int[] verticesA, int[] verticesB, double[] edgeWeights)
		{
			_numVertices = numVertices;
			_verticesA = verticesA;
			_verticesB = verticesB;
			_edgeWeights = edgeWeights;
			_edges = new List<int>[numVertices];

			for (var i = 0; i < _edges.Length; i++)
			{
				_edges[i] = new List<int>(1 + edgeWeights.Length / numVertices);
			}

			for (var i = 0; i < edgeWeights.Length; i++)
			{
				var vertexOne = _verticesA[i];
				var vertexTwo = _verticesB[i];

				_edges[vertexOne].Add(vertexTwo);

				if (vertexOne != vertexTwo)
					_edges[vertexTwo].Add(vertexOne);
			}
		}

		public void QuicksortByEdgeWeight()
		{
			if (_edgeWeights.Length <= 1)
				return;

			var startIndexStack = new int[_edgeWeights.Length / 2];
			var endIndexStack = new int[_edgeWeights.Length / 2];

			startIndexStack[0] = 0;
			endIndexStack[0] = _edgeWeights.Length - 1;

			var stackTop = 0;

			while (stackTop >= 0)
			{
				var startIndex = startIndexStack[stackTop];
				var endIndex = endIndexStack[stackTop];
				stackTop--;

				var pivotIndex = SelectPivotIndex(startIndex, endIndex);
				pivotIndex = Partition(startIndex, endIndex, pivotIndex);

				if (pivotIndex > startIndex + 1)
				{
					startIndexStack[stackTop + 1] = startIndex;
					endIndexStack[stackTop + 1] = pivotIndex - 1;
					stackTop++;
				}

				if (pivotIndex < endIndex - 1)
				{
					startIndexStack[stackTop + 1] = pivotIndex + 1;
					endIndexStack[stackTop + 1] = endIndex;
					stackTop++;
				}
			}
		}

		private int SelectPivotIndex(int startIndex, int endIndex)
		{
			if (startIndex - endIndex <= 1)
				return startIndex;

			var first = _edgeWeights[startIndex];
			var middle = _edgeWeights[startIndex + (endIndex - startIndex) / 2];
			var last = _edgeWeights[endIndex];

			if (first <= middle)
			{
				if (middle <= last)
					return startIndex + (endIndex - startIndex) / 2;
				
				if (last >= first)
					return endIndex;
				
				return startIndex;
			}

			if (first <= last)
				return startIndex;
			
			if (last >= middle)
				return endIndex;
			
			return startIndex + (endIndex - startIndex) / 2;
		}

		private int Partition(int startIndex, int endIndex, int pivotIndex)
		{
			var pivotValue = _edgeWeights[pivotIndex];
			SwapEdges(pivotIndex, endIndex);
			var lowIndex = startIndex;
			for (var i = startIndex; i < endIndex; i++)
			{
				if (_edgeWeights[i] < pivotValue)
				{
					SwapEdges(i, lowIndex);
					lowIndex++;
				}
			}
			SwapEdges(lowIndex, endIndex);
			return lowIndex;
		}

		private void SwapEdges(int indexOne, int indexTwo)
		{
			if (indexOne == indexTwo)
				return;

			var tempVertexA = _verticesA[indexOne];
			var tempVertexB = _verticesB[indexOne];
			var tempEdgeDistance = _edgeWeights[indexOne];
			_verticesA[indexOne] = _verticesA[indexTwo];
			_verticesB[indexOne] = _verticesB[indexTwo];
			_edgeWeights[indexOne] = _edgeWeights[indexTwo];
			_verticesA[indexTwo] = tempVertexA;
			_verticesB[indexTwo] = tempVertexB;
			_edgeWeights[indexTwo] = tempEdgeDistance;
		}

		public int GetNumVertices()
		{
			return _numVertices;
		}

		public int GetNumEdges()
		{
			return _edgeWeights.Length;
		}

		public int GetFirstVertexAtIndex(int index)
		{
			return _verticesA[index];
		}

		public int GetSecondVertexAtIndex(int index)
		{
			return _verticesB[index];
		}

		public double GetEdgeWeightAtIndex(int index)
		{
			return _edgeWeights[index];
		}

		public List<int> GetEdgeListForVertex(int vertex)
		{
			return _edges[vertex];
		}
	}
}
