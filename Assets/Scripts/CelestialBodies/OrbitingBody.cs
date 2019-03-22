using UnityEngine;

/* a celestial body that can orbit other bodies */
public class OrbitingBody : CelestialBody, ISatellite
{
	public bool debugMode = false;
	public GameObject orbitMarker;
	private GameObject[] markers;

	[Range(1.0f, 500.0f)]
	private float _orbitSpeed;
	
	private OrbitPath _path;
	private int _pointIndex = 0;
	private Vector3 _nextPathPoint;

	private OrbitRegion _region;

	/* body that this one orbits */
	public CelestialBody Primary
	{
		get { return _path.Primary; }
	}
	
	public float OrbitSpeed
	{
		get { return _orbitSpeed; }
		set { _orbitSpeed = Mathf.Clamp(value, 1.0f, 500.0f); }
	}

	public float OrbitRadius1
	{
		get { return _path.Radius1; }
		set
		{
			_path.Radius1 = value;
			CalculateOrbitRegion();
		}
	}

	public float OrbitRadius2
	{
		get { return _path.Radius2; }
		set
		{
			_path.Radius2 = value;
			CalculateOrbitRegion();
		}
	}

	/* larger orbit radius */
	public float MaxOrbitRadius
	{
		get { return Mathf.Max(_path.Radius1, _path.Radius2); }
	}

	/* smaller orbit radius */
	public float MinOrbitRadius
	{
		get { return Mathf.Min(_path.Radius1, _path.Radius2); }
	}

	public override float Size
	{
		get { return transform.localScale.x / 2; }
		set
		{
			base.Size = value;
			CalculateOrbitRegion();
		}
	}

	public OrbitPath Path
	{
		get { return _path; }
	}

	public OrbitRegion Region
	{
		get { return _region; }
	}

	protected override void Start()
	{
		base.Start();
	}
	
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		
		// TODO: traverse path at rate * time multiplier

	}

	/* should be called immediately after instantiation */
	public void InitSatellite(CelestialBody primary, OrbitPath path, float rad1, float rad2)
	{
		this._path = path;

		_path.Initialize(primary, rad1, rad2);
		this.transform.position = _path.GetWorldPointByIndex(0);
		this.transform.rotation = Quaternion.identity;

		markers = new GameObject[4];
		CalculateOrbitRegion();
	}

	

	/* calculates the region this OB occupies while orbiting */
	public void CalculateOrbitRegion()
	{
		if (!_path)
			return;

		_region = new OrbitRegion
		{
			upperRad1 = _path.Radius1,
			upperRad2 = _path.Radius2,
			lowerRad1 = _path.Radius1,
			lowerRad2 = _path.Radius2
		};

		if (this.NumSatellites <= 0)
		{
			_region += this.Size;
		}
		else
		{
			_region += this.GetFurthestSatellite().Region;
		}

		if (debugMode)
		{
			RenderRegion();
		}
	}

	private Vector3 GetNextPathPoint()
	{
		if (_pointIndex >= _path.Length - 1)
		{
			_pointIndex = 0;
		}
		else
		{
			_pointIndex++;
		}
		
		return _path.GetLocalPointByIndex(_pointIndex);
	}
	
	public void TraversePath()
	{
		if (transform.position == transform.TransformPoint(_nextPathPoint))
		{
			_nextPathPoint = GetNextPathPoint();
		}
		
		transform.position = Vector3.MoveTowards(
			transform.position,
			transform.TransformPoint(_nextPathPoint),
			TimeMultiplier * _orbitSpeed * Time.time);
	}

	public override void AddOrbitingBody(ISatellite body)
	{
		base.AddOrbitingBody(body);

		CalculateOrbitRegion();
	}

	public override void RemoveOrbitingBody(ISatellite body)
	{
		base.RemoveOrbitingBody(body);

		CalculateOrbitRegion();
	}

	#region debugging functions

	private void RenderRegion()
	{
		foreach (GameObject marker in markers)
			Destroy(marker);

		Vector3 currentPos = this.transform.position;
		Vector3 primaryPos = Primary.transform.position;

		markers[0] = Instantiate(orbitMarker, primaryPos + Vector3.forward * _region.upperRad1, Quaternion.identity);
		markers[1] = Instantiate(orbitMarker, primaryPos + Vector3.forward * _region.lowerRad1, Quaternion.identity);
		markers[2] = Instantiate(orbitMarker, primaryPos + Vector3.right * _region.upperRad2, Quaternion.identity);
		markers[3] = Instantiate(orbitMarker, primaryPos + Vector3.right * _region.lowerRad2, Quaternion.identity);
	}

	#endregion
}
