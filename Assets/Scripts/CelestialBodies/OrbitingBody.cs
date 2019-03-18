using System.Collections.Generic;
using UnityEngine;

/* a celestial body that can orbit other bodies */
public class OrbitingBody : CelestialBody, ISatellite
{
	[Range(1.0f, 500.0f)]
	private float _orbitSpeed;
	
	private OrbitPath _path;
	private int _pointIndex = 0;
	private Vector3 _nextPathPoint;
	
	/* body that this one orbits */
	/*
	private CelestialBody _primary;
	
	public CelestialBody Primary
	{
		get { return _primary; }
		set { _primary = value; }
	}
	*/

	public float OrbitSpeed
	{
		get { return _orbitSpeed; }
		set { _orbitSpeed = Mathf.Clamp(value, 1.0f, 500.0f); }
	}

	/* larger orbit radius */
	public float MaxOrbitRadius
	{
		get
		{
			return Mathf.Max(_path.Radius1, _path.Radius2);
		}
	}

	/* smaller orbit radius */
	public float MinOrbitRadius
	{
		get { return Mathf.Min(_path.Radius1, _path.Radius2); }
	}

	public OrbitPath OrbitalPath
	{
		get { return _path; }
		set { _path = value; }
	}
	
	public OrbitRegion OrbitalRegion
	{
		get
		{
			return CalculateOrbitRegion();
		}
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
	
	private OrbitRegion CalculateOrbitRegion()
	{
		if (!_path)
			return new OrbitRegion();

		return new OrbitRegion
		{
			upperRad1 = _path.Radius1 + this.BodyRadius,
			upperRad2 = _path.Radius2 + this.BodyRadius,
			lowerRad1 = _path.Radius1 - this.BodyRadius,
			lowerRad2 = _path.Radius2 - this.BodyRadius
		}; ;
	}
	
	public bool RegionsOverlap(OrbitRegion other)
	{

		return false;
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
}
