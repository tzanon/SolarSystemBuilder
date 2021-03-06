﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuSlider : MenuControl {
	private float minPos = -0.1f;
	private float maxPos = 0.1f;

	public InteractHand interactingHand;
	public GameObject knob;

	public GameObject objectToAffect;
	
	/* the value, in percentage, of the slider */
	public float Value
	{
		get
		{
			float absolutePosition = knob.transform.localPosition.x + Mathf.Abs(minPos);
			float percent = absolutePosition / _collider.bounds.size.x;
			return Mathf.Clamp(percent, 0.0f, 1.0f);
		}
		set
		{
			float percent = Mathf.Clamp(value, 0.0f, 1.0f);
			float absolutePosition = percent / _collider.bounds.size.x;
			float trackPosition = Mathf.Clamp(absolutePosition - Mathf.Abs(minPos), minPos, maxPos);
			
			Vector3 knobPos = knob.transform.localPosition;
			knob.transform.localPosition = new Vector3(trackPosition, knobPos.y, knobPos.z);
		}
	}
	
	protected override void Awake()
	{
		_buttonImage = knob.GetComponent<Image>();
	}

	protected override void OnTriggerExit(Collider other)
	{
		base.OnTriggerExit(other);

		if (other.CompareTag("InteractHand"))
		{
			this.StopUsing();
		}
	}

	public override void InitControl()
	{
		base.InitControl();

		this.tag = "Slider";
		minPos = -_collider.bounds.size.x / 2;
		maxPos = _collider.bounds.size.x / 2;
	}

	/* set the knob in accordance with the given position */
	public void UpdateKnobPosition(Vector3 worldNewPosition)
	{
		Vector3 localNewPosition = this.transform.InverseTransformPoint(worldNewPosition);
		Vector3 knobPos = knob.transform.localPosition;
		Vector3 newKnobPos = new Vector3(localNewPosition.x, knobPos.y, knobPos.z);

		if (debugMode)
			Debug.Log("setting knob to position " + newKnobPos.ToString());
		
		knob.transform.localPosition = newKnobPos;

		// TODO: call some "non-final" representation (e.g. visualization) of the final value
	}
	
	public override void Use()
	{
		_buttonImage.sprite = pressedSprite;
	}

	public void StopUsing()
	{		
		if (_isHovered)
		{
			this.Hover();
		}
		else
		{
			this.Default();
		}
		
		if (debugMode)
		{
			Debug.Log("Slider value: " + this.Value);
		}

		OnPress.Invoke();
	}

}
