using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
	private MeshRenderer mr;
	public List<Material> availableMaterials;
	public Material highlightMaterial;

	public const float minTime = 0.0f;
	public const float maxTime = 1.0f;
	private static float _timeMultiplier = 1.0f;
	
	public const float MinimumSeparatingDistance = 15.0f;

	/* these should be different for every object */
	private float _maxSize = 100.0f; // this should be less than the smaller radius
	public float _minSize = 1.0f;

	[SerializeField]
	private float _rotationVelocity = 30.0f;
	
	// TODO: set some rule for satellites
	// e.g. max orbit is half that of its primary
	private float maxOrbitDistance = 100.0f;
	
	/* list of object and orbit path pairs for this body */
	protected List<ISatellite> _satellites = new List<ISatellite>();

	private ISatellite _furthestSatellite;
	
	public static float TimeMultiplier
	{
		get { return _timeMultiplier; }
		set { _timeMultiplier = Mathf.Clamp(value, minTime, maxTime); }
	}
	
	public int NumSatellites
	{
		get { return _satellites.Count; }
	}

	public ISatellite FurthestSatellite
	{
		get { return _furthestSatellite; }
	}

	public virtual float Size
	{
		get { return transform.localScale.x / 2; }
		set
		{
			float size = Mathf.Clamp(value, 0.1f, _maxSize);
			transform.localScale = new Vector3(value, value, value);
		}
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

	public float SetPropertyByPercent(float percent, float min, float max)
	{
		float propertyRange = max - min + 1;
		float value = min + percent * propertyRange;
		return value;
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
	
	private void CalculateFurthestSatellite()
	{
		if (_satellites.Count < 1)
		{
			_furthestSatellite = null;
			return;
		}

		if (_satellites.Count == 1)
		{
			_furthestSatellite = _satellites[0];
			return;
		}

		ISatellite furthest = _satellites[0];
		float furthestOrbitDistance = furthest.MaxOrbitRadius;

		foreach (ISatellite sat in _satellites)
		{
			float satMaxOrbit = sat.MaxOrbitRadius;

			if (satMaxOrbit > furthestOrbitDistance)
			{
				furthest = sat;
				furthestOrbitDistance = satMaxOrbit;
			}
		}

		_furthestSatellite = furthest;
	}

	public ISatellite GetFurthestSatellite()
	{
		if (_satellites.Count <= 0)
		{
			return null;
		}
		
		ISatellite furthest = _satellites[0];
		float furthestOrbitDistance = furthest.MaxOrbitRadius;

		foreach (ISatellite sat in _satellites)
		{
			float satMaxOrbit = sat.MaxOrbitRadius;

			if (satMaxOrbit > furthestOrbitDistance)
			{
				furthest = sat;
				furthestOrbitDistance = satMaxOrbit;
			}
		}
		
		return furthest;
	}
	
	public void Remove()
	{
		// TODO: destroy this CB and everything orbiting it
		// orbit path, satellites, etc.

		// not if initial star
		if (this.CompareTag("InitialStar"))
		{
			return;
		}

	}

	public virtual void AddOrbitingBody(ISatellite body)
	{
		_satellites.Add(body);
		this.CalculateFurthestSatellite();
	}
	
	public virtual void RemoveOrbitingBody(ISatellite body)
	{
		_satellites.Remove(body);
		this.CalculateFurthestSatellite();
	}

	/* for selecting */
	public void Highlight()
	{
		Material[] mats = new Material[2];
		mats[0] = mr.materials[0];
		mats[1] = highlightMaterial;
		mr.materials = mats;
	}

	public void RemoveHighlight()
	{
		Material[] mats = new Material[1];
		mats[0] = mr.materials[0];
		mr.materials = mats;
	}

	#region debug functions

	public string SatellitesToString()
	{
		string satStr = "";

		foreach (ISatellite sat in _satellites)
		{
			satStr += (sat.OrbitRadius1 + ", ");
		}

		return satStr;
	}

	public void PrintOrbits()
	{
		Debug.Log(SatellitesToString());
	}

	#endregion

}
