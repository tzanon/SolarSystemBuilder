using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	public CelestialBody initialStar;
	
	public List<OrbitingBody> bodies = new List<OrbitingBody>();

    private void Awake()
    {
        CelestialBody.TimeMultiplier = 0.0f;

        // TODO: spawn initial star

    }

	void Start()
	{

	}
	
	void Update()
	{
		
	}

    /* adds a satellite in orbit around an existing body */
    public void AddSatellite(ISatellite satellite, CelestialBody primary)
    {
        /* 
		 * TODO:
         * 1) get larger radius of primary's furthest orbital path
         * 2) add satellite at some radius s.t. not overlapping with former furthest
         */
    }

	public void SaveSolarSystem()
	{
		
	}
	
	public void LoadSolarSystem()
	{
		
	}
	
}
