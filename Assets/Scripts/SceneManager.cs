using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

	public bool debugMode = false;
	private bool _sceneIsEditable;

	public Text orbitListDisplay;
	public Text numSatellitesDisplay;
	public Text furthestOrbitDisplay;

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
			{ CelestialType.Moon, moonTemplate }
		};

		this.SetEditMode();
		UpdateDisplayInfo();
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

			if (Input.GetKeyDown(KeyCode.I))
			{
				initialStar.PrintOrbits();
			}

			// spawn a star
			if (Input.GetKeyDown(KeyCode.S))
			{
				Debug.Log("added star");
				this.AddSatellite(initialStar, CelestialType.Star);
			}

			// spawn a planet
			if (Input.GetKeyDown(KeyCode.P))
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
			furthestRegionLimit = primary.FurthestSatellite.Region.Max;
		}

		//Debug.Log("size of body is " + templates[type].Size);

		float orbitRadius = furthestRegionLimit + CelestialBody.MinimumSeparatingDistance + templates[type].Size;
		OrbitPath path = Instantiate(orbitPathTemplate);

		Debug.Log("orbit radius is " + orbitRadius);
		
		/*
		 * TODO: spawn satellite at path's north position
		 * set satellite's path
		 * add satellite to SM's list of CBs
		 */
		 
		
		OrbitingBody satellite = Instantiate(templates[type]);
		satellite.InitSatellite(primary, path, orbitRadius, orbitRadius);

		primary.AddOrbitingBody(satellite);

		if (debugMode)
			UpdateDisplayInfo();
	}

	public void RemoveSatellite()
	{

	}

	public void SaveSolarSystem () {

	}

	public void LoadSolarSystem () {

	}

	public void UpdateDisplayInfo()
	{
		orbitListDisplay.text = initialStar.SatellitesToString();
		numSatellitesDisplay.text = "Satellites: " + initialStar.NumSatellites;

		ISatellite furthest = initialStar.FurthestSatellite;
		if (furthest != null)
			furthestOrbitDisplay.text = "Furthest Orbit: " + furthest.MaxOrbitRadius;
		else
			furthestOrbitDisplay.text = "Furthest Orbit: None";

	}

}
