using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
	private MeshRenderer mr;
	public List<Material> availableMaterials;
	
	private static float _timeMultiplier = 1.0f;
	
	public const float MinimumSeparatingDistance = 10.0f;
	
	[SerializeField]
	private float _rotationVelocity = 30.0f;
	
	// TODO: set some rule for satellites
	// e.g. max orbit is half that of its primary
	private float maxOrbitDistance = 100.0f;
	
	/* list of object and orbit path pairs for this body */
	protected List<Orbit> _orbits = new List<Orbit>();
	
	public static float TimeMultiplier
	{
		get { return _timeMultiplier; }
		set { _timeMultiplier = Mathf.Clamp(value, 0.0f, 1.0f); }
	}
	
	public int NumSatellites
	{
		get { return _orbits.Count; }
	}
	
	public float BodyRadius
	{
		get { return transform.localScale.x / 2; }
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
				Mathf.Clamp(value, 0.0f, 360.0f)
				);
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
	
	public Orbit GetFurthestOrbit()
	{
		if (_orbits.Count <= 0)
		{
			return null;
		}
		
		Orbit furthest = _orbits[0];
		float furthestOrbitDistance = furthest.Satellite.MaxOrbitRadius;

		foreach (Orbit orbit in _orbits)
		{
			float satMaxOrbit = orbit.Satellite.MaxOrbitRadius;

			if (satMaxOrbit > furthestOrbitDistance)
			{
				furthest = orbit;
				furthestOrbitDistance = satMaxOrbit;
			}
		}
		
		return furthest;
	}
	
	public void AddOrbitingBody(ISatellite body)
	{
		
	}
	
	public void RemoveOrbitingBody(ISatellite body)
	{
		
	}
	
}
