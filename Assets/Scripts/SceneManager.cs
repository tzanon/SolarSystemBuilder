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

	void Update () {

	}

	/* adds a satellite in orbit around an existing body */
	public void AddSatellite (CelestialBody primary, CelestialType type, float size) {
		/* 
		 * TODO:
		 * 1) get larger radius of primary's furthest orbital path
		 * 2) min valid orbit rad = furthestOrbit.region.upperLimit + minDist (for now)
		 * 3) add satellite at some radius s.t. not overlapping with former furthest
		 */

		ISatellite furthestFromPrimary = primary.FurthestSatellite();

		float furthestRegionLimit;

		if (furthestFromPrimary != null)
		{
			furthestRegionLimit = furthestFromPrimary.OrbitalRegion.Max;
		}
		else
		{
			furthestRegionLimit = primary.BodyRadius;
		}

		float orbitRadius = furthestRegionLimit + CelestialBody.MinimumSeparatingDistance + size;

		OrbitPath path = Instantiate(orbitPathTemplate, primary.transform.position, Quaternion.identity);
		path.Primary = primary;
		path.Initialize(orbitRadius, orbitRadius);

	}

	public void SaveSolarSystem () {

	}

	public void LoadSolarSystem () {

	}

}
