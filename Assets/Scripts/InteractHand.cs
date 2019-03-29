using UnityEngine;

public class InteractHand : MonoBehaviour {

	private LineRenderer lr;
	private Rigidbody rb;
	private SphereCollider selectRegion;

	private MenuControl hoveredControl;

	private void Start () {
		this.tag = "InteractHand";
		
		selectRegion = GetComponent<SphereCollider>();
		rb = GetComponent<Rigidbody>();

		rb.isKinematic = true;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Control"))
		{
			hoveredControl = other.GetComponent<MenuControl>();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Control"))
		{
			hoveredControl = null;
			this.StopUsingControl();
		}
	}
	
	public void UseControl()
	{
		if (hoveredControl)
			hoveredControl.Use();
	}

	public void StopUsingControl()
	{
		MenuSlider slider;
		if (hoveredControl && (slider = hoveredControl.GetComponent<MenuSlider>()))
		{
			slider.StopUsing();
		}
	}

	public void SetAsSelectingHand()
	{

	}

	public void SetAsMenuHand()
	{

	}

}
