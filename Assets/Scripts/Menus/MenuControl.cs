using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class MenuControl : MonoBehaviour {

	public UnityEvent OnPress;

	public Sprite defaultSprite;
	public Sprite hoverSprite;
	public Sprite pressedSprite;

	protected Image _buttonImage;
	
	protected bool _isHovered = false;
	
	private Collider _collider;
	
	void Start () {
		this.tag = "Control";

		_buttonImage = GetComponent<Image>();
		_collider = GetComponent<BoxCollider>();
		_collider.isTrigger = true;
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("trigger entered");
		if (other.CompareTag("InteractHand"))
		{
			Debug.Log("hover");
			Hover();
		}
	}

	void OnTriggerExit(Collider other)
	{
		Debug.Log("trigger exited");
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
