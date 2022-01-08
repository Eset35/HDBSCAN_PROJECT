using System;
using System.Collections.Generic;

namespace HdbscanSharp.Hdbscanstar
{
	public class Cluster
	{
		private readonly double _birthLevel;
		private double _deathLevel;
		private int _numPoints;
		private double _propagatedStability;
		private int _numConstraintsSatisfied;
		private int _propagatedNumConstraintsSatisfied;
		private SortedSet<int> _virtualChildCluster;

		public List<Cluster> PropagatedDescendants { get; }
		public double PropagatedLowestChildDeathLevel { get; internal set; }
		public Cluster Parent { get; }
		public double Stability { get; internal set; }
		public bool HasChildren { get; internal set; }
		public int Label { get; }
		public int HierarchyPosition { get; set; } 

		public Cluster(int label, Cluster parent, double birthLevel, int numPoints)
		{
			_birthLevel = birthLevel;
			_deathLevel = 0;
			_numPoints = numPoints;
			_propagatedStability = 0;
			_numConstraintsSatisfied = 0;
			_propagatedNumConstraintsSatisfied = 0;
			_virtualChildCluster = new SortedSet<int>();

			Label = label;
			HierarchyPosition = 0;
			Stability = 0;
			PropagatedLowestChildDeathLevel = double.MaxValue;
			Parent = parent;
			if (Parent != null)
				Parent.HasChildren = true;
			HasChildren = false;
			PropagatedDescendants = new List<Cluster>(1);
		}

		public void DetachPoints(int numPoints, double level)
		{
			_numPoints -= numPoints;
			Stability += (numPoints * (1 / level - 1 / _birthLevel));

			if (_numPoints == 0)
				_deathLevel = level;
			else if (_numPoints < 0)
				throw new InvalidOperationException("У кластера не может быть меньше 0 инстансов");
		}

		public void Propagate()
		{
			if (Parent != null)
			{
				if (PropagatedLowestChildDeathLevel == double.MaxValue)
					PropagatedLowestChildDeathLevel = _deathLevel;
				if (PropagatedLowestChildDeathLevel < Parent.PropagatedLowestChildDeathLevel)
					Parent.PropagatedLowestChildDeathLevel = PropagatedLowestChildDeathLevel;
				
				if (!HasChildren)
				{
					Parent._propagatedNumConstraintsSatisfied += _numConstraintsSatisfied;
					Parent._propagatedStability += Stability;
					Parent.PropagatedDescendants.Add(this);
				}
				else if (_numConstraintsSatisfied > _propagatedNumConstraintsSatisfied)
				{
					Parent._propagatedNumConstraintsSatisfied += _numConstraintsSatisfied;
					Parent._propagatedStability += Stability;
					Parent.PropagatedDescendants.Add(this);
				}
				else if (_numConstraintsSatisfied < _propagatedNumConstraintsSatisfied)
				{
					Parent._propagatedNumConstraintsSatisfied += _propagatedNumConstraintsSatisfied;
					Parent._propagatedStability += _propagatedStability;
					Parent.PropagatedDescendants.AddRange(PropagatedDescendants);
				}
				else if (_numConstraintsSatisfied == _propagatedNumConstraintsSatisfied)
				{
					if (Stability >= _propagatedStability)
					{
						Parent._propagatedNumConstraintsSatisfied += _numConstraintsSatisfied;
						Parent._propagatedStability += Stability;
						Parent.PropagatedDescendants.Add(this);
					}
					else
					{
						Parent._propagatedNumConstraintsSatisfied += _propagatedNumConstraintsSatisfied;
						Parent._propagatedStability += _propagatedStability;
						Parent.PropagatedDescendants.AddRange(PropagatedDescendants);
					}
				}
			}
		}

		public void AddPointsToVirtualChildCluster(SortedSet<int> points)
		{
			foreach (var point in points)
			{
				_virtualChildCluster.Add(point);
			}
		}

		public bool VirtualChildClusterConstraintsPoint(int point)
		{
			return _virtualChildCluster.Contains(point);
		}

		public void AddVirtualChildConstraintsSatisfied(int numConstraints)
		{
			_propagatedNumConstraintsSatisfied += numConstraints;
		}

		public void AddConstraintsSatisfied(int numConstraints)
		{
			_numConstraintsSatisfied += numConstraints;
		}

		public void ReleaseVirtualChildCluster()
		{
			_virtualChildCluster = null;
		}
	}
}
