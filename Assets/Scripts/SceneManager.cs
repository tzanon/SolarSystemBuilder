using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

	MenuManager menuManager;

	public bool debugMode = false;
	private bool _sceneIsEditable;

	public User user;

	public Text orbitListDisplay;
	public Text numSatellitesDisplay;
	public Text furthestOrbitDisplay;

	public enum CelestialType { Star, Planet, Moon, SatCam };
	//public enum PropertyType { Time, Size, TiltX, TiltZ, Rotation, OrbitSpeed, Radius1, Radius2 };

	public OrbitingBody starTemplate;
	public OrbitingBody planetTemplate;
	public OrbitingBody moonTemplate;
	public OrbitPath orbitPathTemplate;

	private Dictionary<CelestialType, OrbitingBody> templates;

	public CelestialBody initialStar;
	public List<OrbitingBody> bodies = new List<OrbitingBody> ();

	private float lastTimeMultiplier;

	private Vector3 systemViewPosition;

	public bool SceneIsEditable {
		get { return _sceneIsEditable; }
	}

	/*  */
	private void Awake () {
		CelestialBody.TimeMultiplier = 0.0f;
		lastTimeMultiplier = 0.0f;
	}

	/*  */
	private void Start () {
		menuManager = GetComponent<MenuManager>();
		//menuManager.InitMenus();

		templates = new Dictionary<CelestialType, OrbitingBody> {
			{ CelestialType.Star, starTemplate },
			{ CelestialType.Planet, planetTemplate },
			{ CelestialType.Moon, moonTemplate }
		};

		/* user stars in edit mode */
		menuManager.ActivateMenu(MenuManager.MenuType.Edit);
		this.SetEditMode ();

		// for testing
		//user.SelectInitStar(initialStar);

		if (debugMode)
			UpdateDisplayInfo ();
	}

	/* keyboard (debug) input handling */
	private void Update () {
		if (debugMode) {
			// toggle mode
			if (Input.GetKeyDown (KeyCode.M)) {
				if (SceneIsEditable) {
					Debug.Log ("now in view mode");
					SetViewMode ();
				} else {
					Debug.Log ("now in edit mode");
					SetEditMode ();
				}
			}

			if (Input.GetKeyDown (KeyCode.I)) {
				initialStar.PrintOrbits ();
			}

			// spawn a star
			if (Input.GetKeyDown (KeyCode.S)) {
				Debug.Log ("added star");
				this.AddSatellite (CelestialType.Star);
			}

			// spawn a planet
			if (Input.GetKeyDown (KeyCode.P)) {
				Debug.Log ("added planet");
				this.AddSatellite (CelestialType.Planet);
			}
		}
	}

	public void SetEditMode () {
		_sceneIsEditable = true;
		lastTimeMultiplier = CelestialBody.TimeMultiplier; 
		CelestialBody.TimeMultiplier = 0.0f;
	}

	public void SetViewMode () {
		_sceneIsEditable = false;
		// TODO: change this, make multiplier current slider value
		// or set slider at full position
		CelestialBody.TimeMultiplier = lastTimeMultiplier;
	}

	/* update the time multiplier */
	public void SetTimeMultiplierByPercent(float percent)
	{
		if (debugMode)
			Debug.Log("new time multiplier: " + percent);
		CelestialBody.TimeMultiplier = percent;
	}

	public void CalculateSystemViewpoint()
	{
		float furthestDistance;

		if (initialStar.NumSatellites <= 0) {
			furthestDistance = initialStar.Size / 2;
		} else {
			furthestDistance = initialStar.FurthestSatellite.Region.Max;
		}

		furthestDistance += CelestialBody.MinimumSeparatingDistance;

		systemViewPosition = new Vector3(furthestDistance, 0.0f, 0.0f);
	}

	public void TeleportToSystemViepoint()
	{
		// teleport magic

		user.transform.position = systemViewPosition;

		// fade in
	}

	/* adds a satellite in orbit around an existing body */
	public void AddSatellite (CelestialType type) {
		/* 
		 * TODO:
		 * 1) get larger radius of primary's furthest orbital path
		 * 2) min valid orbit rad = furthestOrbit.region.upperLimit + minDist (for now)
		 * 3) add satellite at some radius s.t. not overlapping with former furthest
		 */

		if (!_sceneIsEditable)
			return;

		CelestialBody primary;

		if (!user) {
			primary = initialStar;
		} else if ((primary = user.selectedObject.GetComponent<CelestialBody> ()) == null) {
			return;
		}

		float furthestRegionLimit;

		if (primary.NumSatellites <= 0) {
			furthestRegionLimit = primary.Size / 2;
		} else {
			furthestRegionLimit = primary.FurthestSatellite.Region.Max;
		}

		float orbitRadius = furthestRegionLimit + CelestialBody.MinimumSeparatingDistance + templates[type].Size / 2;
		OrbitPath path = Instantiate (orbitPathTemplate);

		Debug.Log ("orbit radius is " + orbitRadius);

		/*
		 * TODO: spawn satellite at path's north position
		 * set satellite's path
		 * add satellite to SM's list of CBs
		 */

		OrbitingBody satellite = Instantiate (templates[type]);
		satellite.InitSatellite (primary, path, orbitRadius, orbitRadius);
		bodies.Add(satellite);

		primary.AddOrbitingBody (satellite);

		if (debugMode)
			UpdateDisplayInfo ();
	}

	public void RemoveSatellite () {

	}

	public void SaveSolarSystem () {

	}

	public void LoadSolarSystem () {

	}

	public void UpdateDisplayInfo () {
		if (!orbitListDisplay || !numSatellitesDisplay || !furthestOrbitDisplay) {
			return;
		}

		orbitListDisplay.text = initialStar.SatellitesToString ();
		numSatellitesDisplay.text = "Satellites: " + initialStar.NumSatellites;

		ISatellite furthest = initialStar.FurthestSatellite;
		if (furthest != null)
			furthestOrbitDisplay.text = "Furthest Orbit: " + furthest.MaxOrbitRadius;
		else
			furthestOrbitDisplay.text = "Furthest Orbit: None";
	}

}