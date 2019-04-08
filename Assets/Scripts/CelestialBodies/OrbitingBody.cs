using UnityEngine;

/* a celestial body that can orbit other bodies */
public class OrbitingBody : CelestialBody, ISatellite
{
	public bool debugMode = false;
	public bool orbitDebugMode = false;
	public GameObject orbitMarker;
	private GameObject[] markers;
	public float msgRate = 1.0f;
	private float _nextMsg;

	//public float initOrbitSpeed = 10.0f;

	private Vector3 offset;

	public const float MaxOrbitSpeed = 100.0f;
	public const float MinOrbitSpeed = 1.0f;
	
	public float _orbitSpeed = 15.0f;
	
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
		set { _orbitSpeed = value; }
	}

	public float OrbitRadius1
	{
		get { return _path.Radius1; }
		set
		{
			_path.Radius1 = value;
			this.transform.position = _path.GetWorldPointByIndex(0);
			CalculateOrbitRegion();
		}
	}

	public float OrbitRadius2
	{
		get { return _path.Radius2; }
		set
		{
			_path.Radius2 = value;
			this.transform.position = _path.GetWorldPointByIndex(0);
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
		protected set
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
		
		//OrbitSpeed = initOrbitSpeed;

		_nextMsg = 0.0f;
	}
	
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		
		// TODO: traverse path at rate * time multiplier
		if (_path)
		{
			//this.transform.position = Primary.transform.position + offset;
			this.TraversePath();
		}
	}

	/* should be called immediately after instantiation */
	public void InitSatellite(CelestialBody primary, OrbitPath path, float rad1, float rad2)
	{
		this._path = path;
		
		_path.Initialize(primary, rad1, rad2);
		this.transform.position = _path.GetWorldPointByIndex(0);
		this.transform.rotation = Quaternion.identity;

		offset = transform.position - Primary.transform.position;
		
		this.GetNextPathPoint();
		
		if (debugMode)
			Debug.Log("first point is " + _nextPathPoint + " at index " + _pointIndex);
		
		markers = new GameObject[4];
		CalculateOrbitRegion();
	}
	
	public void UpdateRadii(float rad1, float rad2)
	{
		/*
		* TODO: set _path radii
		* update this position
		* update orbits and positions of satellites
		* or maybe prevent radius modification when it has satellites?
		*/
		
	}

#region slider setters

	public void SetRadius1ByPercent(float percent)
	{
		OrbitRadius1 = CalculatePropertyValue(percent, OrbitPath.minRadius, OrbitPath.maxRadius);
	}

	public void SetRadius2ByPercent(float percent)
	{
		OrbitRadius2 = CalculatePropertyValue(percent, OrbitPath.minRadius, OrbitPath.maxRadius);
	}

	public void SetOrbitSpeedByPercent(float percent)
	{
		OrbitSpeed = CalculatePropertyValue(percent, MinOrbitSpeed, MaxOrbitSpeed);
	}

#endregion

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
			_region += this.naturalMaxSize;
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
	
	private void GetNextPathPoint()
	{
		if (_pointIndex >= _path.Length - 1)
		{
			_pointIndex = 0;
		}
		else
		{
			_pointIndex++;
		}
		
		//_nextPathPoint = _path.GetWorldPointByIndex(_pointIndex);
		_nextPathPoint = _path.GetLocalPointByIndex(_pointIndex);
	}
	
	public void TraversePath()
	{
		Vector3 nextWorldPosition = Primary.transform.position + _nextPathPoint;

		if (transform.position == nextWorldPosition)
		{
			this.GetNextPathPoint();

			if (orbitDebugMode)
			{
				Instantiate(orbitMarker, this.transform.position, Quaternion.identity);
			}

			if (debugMode)
				Debug.Log("moving to path point " + nextWorldPosition.ToString());
		}

		transform.position = Vector3.MoveTowards(
			transform.position,
			nextWorldPosition,
			TimeMultiplier * _orbitSpeed * Time.deltaTime);
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
