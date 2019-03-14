﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPath : MonoBehaviour
{
	private enum Direction { North, East, South, West };
    private Dictionary<Direction, Vector3> _compassPoints = new Dictionary<Direction, Vector3>();
	
	private LineRenderer lr;
	
	public int pointsToCalculate;
	
	public float initRad1, initRad2;
	private float _rad1, _rad2;
	
	// number of points dependent on radius?
	private Vector3[] _cartesianPoints;
	private Vector3[] _worldPoints;
	
    /* whether the path is being visualized */
	public bool PathVisible
	{
		get { return lr.enabled; }
	}
	
    /* number of ellipse points sampled from the function */
	public int PointsToCalculate
	{
		get { return pointsToCalculate; }
		set
		{
			pointsToCalculate = value;
			CalculatePathPoints();
		}
	}
	
	/* vertical radius */
	public float Radius1
	{
		get { return _rad1; }
		set
		{
			_rad1 = Mathf.Clamp(value, 1.0f, 100.0f);
			CalculatePathPoints();
		}
	}
	
	/* horizontal radius */
	public float Radius2
	{
		get { return _rad2; }
		set
		{
			_rad2 = Mathf.Clamp(value, 1.0f, 100.0f);
			CalculatePathPoints();
		}
	}

	public int Length
	{
		get { return _cartesianPoints.Length; }
	}

	private void Start()
	{
		lr = GetComponent<LineRenderer>();
		lr.loop = true;
		
		this.HidePath();
		
		_rad1 = initRad1;
		_rad2 = initRad2;
		
		CalculatePathPoints();
		
		this.ShowPath();
	}
	
	private void Update()
	{
		UpdatePathPositions();
	}
	
	private void UpdatePathPositions()
	{
		if (_cartesianPoints == null || _cartesianPoints.Length <= 0)
			return;
		
		_worldPoints = new Vector3[_cartesianPoints.Length];
		
		for (int i = 0; i < _worldPoints.Length; i++)
		{
			_worldPoints[i] = transform.TransformPoint(_cartesianPoints[i]);
		}
		
		lr.positionCount = _worldPoints.Length;
		lr.SetPositions(_worldPoints);
	}
	
	private void PrintPoints(Vector3[] points)
	{
		string pointStr = "points: ";
		
		for (int i = 0; i < points.Length; i++)
		{
			pointStr += points[i];
			if (i < points.Length - 1)
			{
				pointStr += ", ";
			}
		}
		
		Debug.Log(pointStr);
	}
	
	private void CalculatePathPoints()
	{
		/*
		* center of orbit is transform.position
		* put 4 points at "compass" positions according to rad1 and rad2
		* calculate a set number of vertices in a curve between each pair of
		* the original points
		*/
		
		int numPoints = 4 * (pointsToCalculate + 1);
		_cartesianPoints = new Vector3[numPoints];
		
		_compassPoints[Direction.North] = _rad1 * Vector3.forward;
		_compassPoints[Direction.South] = _rad1 * Vector3.back;
		_compassPoints[Direction.East] = _rad2 * Vector3.right;
		_compassPoints[Direction.West] = _rad2 * Vector3.left;
		
		float pointDistance = _rad2 / (pointsToCalculate + 1);
		
		/* a graph's quadrants are ordered counter-clockwise */
		/* however the path's points must be ordered clockwise for the line renderer to cooperate */
		/* calculate points in first quadrant */
		Vector3[] firstQuadPoints = new Vector3[pointsToCalculate + 1];
		firstQuadPoints[0] = _compassPoints[Direction.North];
		for (int i = 1; i < firstQuadPoints.Length; i++)
		{
			firstQuadPoints[i] = CalulateCartesianPoint(i * pointDistance);
		}
		Array.Copy(firstQuadPoints, 0, _cartesianPoints, 0, firstQuadPoints.Length);
		
		/* get points in 4th quadrant by reflecting 1st points about x-axis */
		Vector3[] fourthQuadPoints = ReflectPoints(firstQuadPoints, Direction.East);
		Array.Copy(fourthQuadPoints, 0, _cartesianPoints, 1 * (pointsToCalculate + 1), fourthQuadPoints.Length);
		
		/* get points in 3rd quadrant by reflecting 1st points about x- and z-axis */
		Vector3[] thirdQuadPoints = ReflectPoints(firstQuadPoints, Direction.South);
		Array.Copy(thirdQuadPoints, 0, _cartesianPoints, 2 * (pointsToCalculate + 1), thirdQuadPoints.Length);
		
		/* get points in 2nd quadrant by reflecting 1st points about z-axis */
		Vector3[] secondQuadPoints = ReflectPoints(firstQuadPoints, Direction.West);
		Array.Copy(secondQuadPoints, 0, _cartesianPoints, 3 * (pointsToCalculate + 1), secondQuadPoints.Length);
	}
	
	private Vector3[] ReflectPoints(Vector3[] points, Direction direction)
	{
		Vector3[] reflectedPoints = new Vector3[points.Length];
		reflectedPoints[0] = _compassPoints[direction];
		
		int xSign, zSign, pointsIdx;
		
		// quadrant 4
		if (direction == Direction.East)
		{
			xSign = 1;
			zSign = -1;
		}
		// quadrant 3
		else if (direction == Direction.South)
		{
			xSign = -1;
			zSign = -1;
		}
		// quadrant 2
		else if (direction == Direction.West)
		{
			xSign = -1;
			zSign = 1;
		}
		// quadrant 1 (what the input should already be)
		else
		{
			xSign = 1;
			zSign = 1;
		}
		
		for (int i = 1; i < points.Length; i++)
		{
			if (xSign * zSign < 0)
				pointsIdx = points.Length - i;
			else
				pointsIdx = i;
			
			reflectedPoints[i] = new Vector3(xSign * points[pointsIdx].x, 0, zSign * points[pointsIdx].z);
		}
		
		return reflectedPoints;
	}
	
	public Vector3 CalulateCartesianPoint(float x)
	{
		float z = _rad1 * Mathf.Sqrt(1 - Mathf.Pow(x / _rad2, 2));
		return new Vector3(x, 0, z);
	}
	
	public Vector3 GetWorldPointByIndex(int idx)
	{
		return _worldPoints[Mathf.Clamp(idx, 0, _cartesianPoints.Length - 1)];
	}
	
	public Vector3 GetLocalPointByIndex(int idx)
	{
		return transform.InverseTransformPoint(_worldPoints[Mathf.Clamp(idx, 0, _cartesianPoints.Length - 1)]);
	}
	
	public void ShowPath()
	{
		lr.enabled = true;
	}
	
	public void HidePath()
	{
		lr.enabled = false;
	}
}
