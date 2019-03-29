using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MenuControl {
	
	private const float PressVisualTime = 0.2f;

	public override void Use()
	{
		OnPress.Invoke();
		
		if (pressedSprite)
		{
			StartCoroutine("VisualizeUse");
		}
	}
	
	private IEnumerator VisualizeUse()
	{
		_buttonImage.sprite = pressedSprite;
		
		yield return new WaitForSeconds(PressVisualTime);
		
		if (_isHovered)
		{
			this.Hover();
		}
		else
		{
			this.Default();
		}
		
	}
	
}
