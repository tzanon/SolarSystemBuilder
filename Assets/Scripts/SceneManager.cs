using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

	public bool debugMode = false;
	private bool _sceneIsEditable;

	public enum CelestialType { Star, Planet, Moon, SatCam };

	public OrbitingBody starTemplate;
	public OrbitingBody planetTemplate;
	public OrbitingBody moonTemplate;
	public OrbitPath orbitPathTemplate;

	private Dictionary<CelestialType, OrbitingBody> templates;

	public CelestialBody initialStar;

	public List<OrbitingBody> bodies = new List<OrbitingBody> ();

	public bool SceneIsEditable
	{
		get { return _sceneIsEditable; }
	}

	private void Awake () {
		CelestialBody.TimeMultiplier = 0.0f;
	}

	private void Start () {
		templates = new Dictionary<CelestialType, OrbitingBody>
		{
			{ CelestialType.Star, starTemplate },
			{ CelestialType.Planet, planetTemplate },
			{ CelestialType.Moon, moonTemplate },
		};

		this.SetEditMode();
	}

	private void Update()
	{
		if (debugMode)
		{
			// toggle mode
			if (Input.GetKeyDown(KeyCode.M))
			{
				if (SceneIsEditable)
				{
					Debug.Log("now in view mode");
					SetViewMode();
				}
				else
				{
					Debug.Log("now in edit mode");
					SetEditMode();
				}
			}

			// spawn a star
			if (Input.GetKeyDown(KeyCode.Q))
			{
				Debug.Log("added star");
				this.AddSatellite(initialStar, CelestialType.Star);
			}

			// spawn a planet
			if (Input.GetKeyDown(KeyCode.W))
			{
				Debug.Log("added planet");
				this.AddSatellite(initialStar, CelestialType.Planet);
			}
		}
	}

	public void SetEditMode()
	{
		_sceneIsEditable = true;
		CelestialBody.TimeMultiplier = 0.0f;

		// TODO: set user's menu accordingly

	}

	public void SetViewMode()
	{
		_sceneIsEditable = false;
		CelestialBody.TimeMultiplier = 1.0f;

		// TODO: set user's menu accordingly

	}

	/* adds a satellite in orbit around an existing body */
	public void AddSatellite (CelestialBody primary, CelestialType type) {
		/* 
		 * TODO:
		 * 1) get larger radius of primary's furthest orbital path
		 * 2) min valid orbit rad = furthestOrbit.region.upperLimit + minDist (for now)
		 * 3) add satellite at some radius s.t. not overlapping with former furthest
		 */


		if (!_sceneIsEditable)
			return;

		float furthestRegionLimit;

		if (primary.NumSatellites <= 0)
		{
			furthestRegionLimit = primary.Size;
		}
		else
		{
			furthestRegionLimit = primary.GetFurthestSatellite().Region.Max;
		}

		//Debug.Log("size of body is " + templates[type].Size);

		float orbitRadius = furthestRegionLimit + CelestialBody.MinimumSeparatingDistance + 2*templates[type].Size;
		OrbitPath path = Instantiate(orbitPathTemplate); //, primary.transform.position, Quaternion.identity);

		Debug.Log("orbit radius is " + orbitRadius);

		//path.Primary = primary;
		//path.Initialize(orbitRadius, orbitRadius);

		/*
		 * TODO: spawn satellite at path's north position
		 * set satellite's path
		 * add satellite to SM's list of CBs
		 */


		OrbitingBody satellite = Instantiate(templates[type]); //, path.GetWorldPointByIndex(0), Quaternion.identity);
		satellite.InitSatellite(primary, path, orbitRadius, orbitRadius);

		primary.AddOrbitingBody(satellite);
	}

	public void SaveSolarSystem () {

	}

	public void LoadSolarSystem () {

	}

}
