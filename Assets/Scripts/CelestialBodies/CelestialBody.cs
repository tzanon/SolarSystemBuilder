using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
	private MeshRenderer mr;
	public List<Material> availableMaterials;
	
	
	private static float _timeMultiplier = 1.0f;
	
    [SerializeField]
	private float _rotationVelocity = 30.0f;

	// TODO: set some rule for satellites
	// e.g. max orbit is half that of its primary
	private float maxOrbitDistance = 100.0f;
	
	/* set of satellites orbiting this body */
	protected List<ISatellite> _satellites = new List<ISatellite>();
	
	public static float TimeMultiplier
	{
		get { return _timeMultiplier; }
		set { _timeMultiplier = Mathf.Clamp(value, 0.0f, 1.0f); }
	}
	
	public float RotationVelocity
	{
		get { return _rotationVelocity; }
		set { _rotationVelocity = Mathf.Clamp(value, -180.0f, 180.0f); }
	}
	
	public float AxialTiltX
	{
		get { return transform.rotation.eulerAngles.x; }
		set
		{
			transform.rotation = Quaternion.Euler(
				Mathf.Clamp(value, 0.0f, 360.0f),
				transform.rotation.eulerAngles.y,
				transform.rotation.eulerAngles.z);
		}
	}
	
	public float AxialTiltZ
	{
		get { return transform.rotation.eulerAngles.z; }
		set
		{
			transform.rotation = Quaternion.Euler(
				transform.rotation.eulerAngles.x,
				transform.rotation.eulerAngles.y,
				Mathf.Clamp(value, 0.0f, 360.0f));
		}
	}
	
	protected virtual void Start()
	{
		mr = GetComponent<MeshRenderer>();
		
	}
	
	protected virtual void FixedUpdate()
	{
		/* rotate on axis at given speed and direction */
		transform.Rotate(0.0f, TimeMultiplier * RotationVelocity * Time.deltaTime, 0.0f);
		
	}
	
	public void SetTextureByIndex(int idx)
	{
		if (availableMaterials.Count <= 0)
		{
			return;
		}
		
		Material[] mats = new Material[1];
		mats[0] = availableMaterials[Mathf.Clamp(idx, 0, availableMaterials.Count - 1)];
		mr.materials = mats;
	}

    public float GetFurthestOrbit()
    {
		float furthestOrbitDistance = 0.0f;

		if (_satellites.Count <= 0)
		{
			return furthestOrbitDistance;
		}

		ISatellite furthestSat = _satellites[0];
		furthestOrbitDistance = furthestSat.MaxOrbitRadius;

		foreach (ISatellite satellite in _satellites)
		{
			float satMaxOrbit = satellite.MaxOrbitRadius;

			if (satMaxOrbit > furthestOrbitDistance)
			{
				furthestSat = satellite;
				furthestOrbitDistance = satMaxOrbit;
			}
		}

		if (furthestSat.GetType() == typeof(CelestialBody))
		{
			Debug.Log("finding furthest orbit of furthest body's satellites");
			furthestOrbitDistance += ((CelestialBody)furthestSat).GetFurthestOrbit();
		}

		return furthestOrbitDistance;
    }

	public void AddOrbitingBody(ISatellite body)
	{
		
	}
	
	public void RemoveOrbitingBody(ISatellite body)
	{
		if (_satellites.Contains(body))
			_satellites.Remove(body);
	}
	
}
