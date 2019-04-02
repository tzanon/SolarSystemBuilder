using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuSlider : MenuControl {

	public float MinValue = 0.0f;
	public float MaxValue = 1.0f;

	private float minPos;
	private float maxPos;

	public InteractHand interactingHand;
	public GameObject knob;

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

		//Vector3 clampedPosition = new Vector3(Mathf.Clamp(localNewPosition.x, minPos, maxPos), knobPos.y, knobPos.z);
		Vector3 newKnobPos = new Vector3(localNewPosition.x, knobPos.y, knobPos.z);

		if (debugMode)
			Debug.Log("setting knob to position " + newKnobPos.ToString());
		
		knob.transform.localPosition = newKnobPos;
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
		// TODO: call event method with current slider value
		this.VisualizeUse();
	}

	public void StopUsing()
	{
		interactingHand = null;

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


	private void VisualizeUse()
	{
		_buttonImage.sprite = pressedSprite;
	}

}
