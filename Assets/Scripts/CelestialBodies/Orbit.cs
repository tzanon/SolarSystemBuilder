using System.Collections.Generic;
using UnityEngine;

/* new satellites (orbiting bodies) should be added through this class */
public class Orbit {
	
	// move orbit body logic here?
	
	private ISatellite _satellite;
	private OrbitPath _path;
	private OrbitRegion _region;
	
	
	public float Radius1
	{
		get { return _path.Radius1; }
		set
		{
			_path.Radius1 = value;
			CalculateOrbitRegion();
		}
	}
	
	public float Radius2
	{
		get { return _path.Radius2; }
		set
		{
			_path.Radius2 = value;
			CalculateOrbitRegion();
		}
	}
	
	public ISatellite Satellite {
		get { return _satellite; }
	}
	
	public OrbitPath Path {
		get { return _path; }
	}
	
	public OrbitRegion Region
	{
		get { return _region; }
	}
	
	public Orbit (SceneManager.CelestialType type, float radius1, float radius2, CelestialBody primary) {
		// TODO: spawn body and path
		// set body to first point of path (north position)
		// make this a monobehaviour?
		// DO NOT make the path a child of anything (scale!!!)
		
		
		
		this.CalculateOrbitRegion();
	}
	
	public void CalculateOrbitRegion()
	{
		if (!_path || _satellite == null)
			return;
		
		_region = new OrbitRegion
		{
			upperRad1 = _path.Radius1,
			upperRad2 = _path.Radius2,
			lowerRad1 = _path.Radius1,
			lowerRad2 = _path.Radius2
		};
		
		if (_satellite.GetType() == typeof(CelestialBody))
		{
			CelestialBody cb = (CelestialBody)_satellite;
			
			if (cb.NumSatellites <= 0)
			{
				_region += cb.BodyRadius;
			}
			else
			{
				_region += cb.GetFurthestOrbit().Region;
			}
		}
		
	}
	
}