using UnityEngine;

public class OrbitRegion
{
	public Vector3 minimum, maximum;
	
	public OrbitRegion(Vector3 min, Vector3 max)
	{
		minimum = min;
		maximum = max;
	}
	
	public bool ContainsPoint(Vector3 point)
	{
		// test for colinearity
		Vector3 axis = maximum - minimum;
		Vector3 pointAxis = point - minimum;
		
		// if not colinear, false
		if (Vector3.Dot(axis, pointAxis) == 0)
			return false;
		
		//
		
		return false;
	}
}
