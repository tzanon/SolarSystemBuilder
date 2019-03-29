using UnityEngine;

public class MenuSlider : MenuControl {

	public float MinValue = 0.0f;
	public float MaxValue = 1.0f;

	public float minPos;
	public float maxPos;

	public InteractHand interactingHand;

	public float Value
	{
		get
		{
			float trackPosition = this.transform.position.x + Mathf.Abs(minPos);
			float range = Mathf.Abs(minPos) + Mathf.Abs(maxPos);
			float value = trackPosition / range;
			
			return value;
		}
	}

	private void Update()
	{
		if (interactingHand)
		{
			// TODO: 
			
			Vector3 newPos;

			Vector3 handPos = interactingHand.WorldPosition;
			Vector3 localHandPos = this.transform.InverseTransformPoint(handPos);
			Vector3 knobPos = this.transform.localPosition;
			
			newPos = new Vector3(localHandPos.x, knobPos.y, knobPos.z);

			//Vector3 handPos = interactingHand.WorldPosition;
			//Vector3 knobPos = this.transform.position;
			//newPos = new Vector3(handPos.x, knobPos.y, knobPos.z);

			this.transform.localPosition = newPos;
		}
	}

	protected override void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);
	}

	protected override void OnTriggerExit(Collider other)
	{
		base.OnTriggerExit(other);
	}
	
	public override void Use()
	{
		// TODO: call event method with current slider value

		this.VisualizeUse();
	}

	public void StopUsing()
	{
		// TODO: stop updating value

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
