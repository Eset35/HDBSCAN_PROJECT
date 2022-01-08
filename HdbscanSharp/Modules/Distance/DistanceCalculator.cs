namespace HdbscanSharp.Distance
{
	public interface IDistanceCalculator<T>
	{
		double ComputeDistance(int indexOne, int indexTwo, T attributesOne, T attributesTwo);
	}
}
