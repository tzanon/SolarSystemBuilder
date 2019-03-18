using UnityEngine;

public struct OrbitRegion
{
	public float upperRad1, upperRad2;
	public float lowerRad1, lowerRad2;

	public float Max
	{
		get
		{
			return Mathf.Max(upperRad1, upperRad2);
		}
	}

	public float Min
	{
		get
		{
			return Mathf.Min(lowerRad1, lowerRad2);
		}
	}

}
