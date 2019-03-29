using UnityEngine;

public class MenuSlider : MenuControl {

	public const float MinValue = 0.0f;
	public const float MaxValue = 1.0f;

	public InteractHand interactingHand;

	public override void Use()
	{
		// TODO: call event method with current slider value
	}

	public void StopUsing()
	{
		// TODO: stop updating value

		if (_isHovered)
		{
			this.Hover();
		}
		else
		{
			this.Default();
		}
	}

	private void VisualizeUse()
	{
		_buttonImage.sprite = pressedSprite;
	}

}
