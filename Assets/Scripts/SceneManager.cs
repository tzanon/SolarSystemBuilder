using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

	public enum CelestialType { Star, Planet, Moon, SatCam };

	public OrbitingBody starTemplate;
	public OrbitingBody planetTemplate;
	public OrbitingBody moonTemplate;
	public OrbitPath orbitPathTemplate;

	public CelestialBody initialStar;

	public List<OrbitingBody> bodies = new List<OrbitingBody> ();

	private void Awake () {
		CelestialBody.TimeMultiplier = 0.0f;

		// TODO: spawn initial star
	}

	void Start () {
		this.AddSatellite(initialStar, CelestialType.Planet, 20.0f);
	}
	
	/* adds a satellite in orbit around an existing body */
	public void AddSatellite (CelestialBody primary, CelestialType type, float size) {
		/* 
		 * TODO:
		 * 1) get larger radius of primary's furthest orbital path
		 * 2) min valid orbit rad = furthestOrbit.region.upperLimit + minDist (for now)
		 * 3) add satellite at some radius s.t. not overlapping with former furthest
		 */
		
		float furthestRegionLimit;

		if (primary.NumSatellites <= 0)
		{
			furthestRegionLimit = primary.BodyRadius;
		}
		else
		{
			furthestRegionLimit = primary.GetFurthestOrbit().Region.Max;
		}
		
		float orbitRadius = furthestRegionLimit + CelestialBody.MinimumSeparatingDistance + size;
		
		
		//Orbit orbit = new Orbit();
		
		OrbitPath path = Instantiate(orbitPathTemplate, primary.transform.position, Quaternion.identity);
		path.Primary = primary;
		path.Initialize(orbitRadius, orbitRadius);
		
		/* TODO: spawn satellite at path's north position
			
		*/
		
	}

	public void SaveSolarSystem () {

	}

	public void LoadSolarSystem () {

	}

}
