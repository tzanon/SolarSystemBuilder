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
	
	public static OrbitRegion operator+(OrbitRegion a, float f)
	{
		OrbitRegion region = new OrbitRegion
		{
			upperRad1 = a.upperRad1 + f,
			upperRad2 = a.upperRad2 + f,
			lowerRad1 = a.lowerRad1 - f,
			lowerRad2 = a.lowerRad2 - f
		};
		
		return region;
	}
	
	public static OrbitRegion operator+(OrbitRegion a, OrbitRegion b)
	{
		OrbitRegion region = new OrbitRegion
		{
			upperRad1 = a.upperRad1 + b.upperRad1,
			upperRad2 = a.upperRad2 + b.upperRad2,
			lowerRad1 = a.lowerRad1 - b.lowerRad1,
			lowerRad2 = a.lowerRad2 - b.lowerRad2
		};
		
		return region;
	}
	
}
