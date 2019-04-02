using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class MenuControl : MonoBehaviour {

	public static bool debugMode = true;

	public UnityEvent OnPress;

	public Sprite defaultSprite;
	public Sprite hoverSprite;
	public Sprite pressedSprite;

	protected Image _buttonImage;
	
	protected bool _isHovered = false;
	
	protected Collider _collider;
	
	protected virtual void Awake()
	{
		_buttonImage = GetComponent<Image>();
	}

	protected virtual void Start () {
		this.tag = "Control";
		
		_collider = GetComponent<BoxCollider>();
		_collider.isTrigger = true;
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("InteractHand"))
		{
			if (debugMode)
				Debug.Log("hover");
			Hover();
		}
	}

	protected virtual void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("InteractHand"))
		{
			if (debugMode)
				Debug.Log("default");
			Default();
		}
	}

	protected void Default()
	{
		_buttonImage.sprite = defaultSprite;
		_isHovered = false;
	}
	
	protected void Hover()
	{
		_buttonImage.sprite = hoverSprite;
		_isHovered = true;
	}

	public abstract void Use();

}
