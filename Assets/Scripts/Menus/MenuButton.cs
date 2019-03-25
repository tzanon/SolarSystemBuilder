using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour {
	
	public UnityEvent OnPress;
	
	public Sprite defaultSprite;
	public Sprite hoverSprite;
	public Sprite pressedSprite;
	
	private Image _buttonImage;
	
	private bool _isHovered = false;
	
	
	void Start () {
		_buttonImage = GetComponent<Image>();
		
		
	}
	
	public void Default()
	{
		_buttonImage.sprite = defaultSprite;
		_isHovered = false;
	}
	
	public void Hover()
	{
		_buttonImage.sprite = hoverSprite;
		_isHovered = true;
	}
	
	public void Press()
	{
		OnPress.Invoke();
		
		if (pressedSprite)
		{
			StartCoroutine("VisualizePress");
		}
	}
	
	private IEnumerator VisualizePress()
	{
		_buttonImage.sprite = pressedSprite;
		
		yield return new WaitForSeconds(0.1f);
		
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
