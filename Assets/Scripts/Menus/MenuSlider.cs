using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuSlider : MenuControl {

	public float MinValue = 0.0f;
	public float MaxValue = 1.0f;

	private float minPos;
	private float maxPos;

	public InteractHand interactingHand;
	public GameObject knob;

	public GameObject objectToAffect;

	public float Value
	{
		get
		{
			float trackPosition = knob.transform.localPosition.x + Mathf.Abs(minPos);
			float value = trackPosition / _collider.bounds.size.x;
			return value;
		}
	}

	protected override void Awake()
	{
		_buttonImage = knob.GetComponent<Image>();
	}

	protected override void Start()
	{
		base.Start();

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

		// TODO: update linked value
		OnPress.Invoke();
		
	}

	protected override void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);
	}

	protected override void OnTriggerExit(Collider other)
	{
		base.OnTriggerExit(other);

		if (interactingHand && other.gameObject == interactingHand.gameObject)
		{
			interactingHand = null;
		}
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
	}

}
