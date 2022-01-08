using System;

namespace HdbscanSharp.Hdbscanstar
{
	//Базовый класс результата
	public class OutlierScore : IComparable<OutlierScore> {

		private readonly double _coreDistance;
		public double Score { get; set; }
		public int Id { get; set; }

		public OutlierScore(double score, double coreDistance, int id)
		{
			Score = score;
			_coreDistance = coreDistance;
			Id = id;
		}

		public int CompareTo(OutlierScore other)
		{
			if (Score > other.Score)
				return 1;
			
			if (Score < other.Score)
				return -1;
			
			if (_coreDistance > other._coreDistance)
				return 1;
			
			if (_coreDistance < other._coreDistance)
				return -1;
			
			return Id - other.Id;
		}
	}
}
