using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class MenuControl : MonoBehaviour {

	public static bool debugMode = false;

	public UnityEvent OnPress;

	public Sprite defaultSprite;
	public Sprite hoverSprite;
	public Sprite pressedSprite;

	protected Image _buttonImage;
	
	protected bool _isHovered = false;
	
	private Collider _collider;
	
	private void Start () {
		this.tag = "Control";

		_buttonImage = GetComponent<Image>();
		_collider = GetComponent<BoxCollider>();
		_collider.isTrigger = true;
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("InteractHand"))
		{
			Debug.Log("hover");
			Hover();
		}
	}

	protected virtual void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("InteractHand"))
		{
			Debug.Log("default");
			Default();
		}
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

	public abstract void Use();

}
