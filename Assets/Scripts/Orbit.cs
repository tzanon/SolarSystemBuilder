using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* new satellites (orbiting bodies) should be added through this class */
public class Orbit {

	private ISatellite _satellite;
	private OrbitPath _path;

	public ISatellite Satellite {
		get { return _satellite; }
	}

	public OrbitPath Path {
		get { return _path; }
	}

	public Orbit (SceneManager.CelestialType type, float radius1, float radius2, CelestialBody primary) {

		// TODO: spawn body and path
		// set body to first point of path (north position)
		// make this a monobehaviour?
		// DO NOT make this a child of anything (scale!!!)

	}

	
}