using UnityEngine;

public class InteractHand : MonoBehaviour {

	/* components */
	private LineRenderer lr;
	private Rigidbody rb;
	private SphereCollider selectRegion;

	/* need different trigger regions for touch and vive controllers */
	public bool usingVive;
	private readonly Vector3 touchTriggerPos = new Vector3(0.0f, -0.03f, -0.05f);
	private const float touchTriggerRadius = 0.03f;
	private readonly Vector3 viveTriggerPos = new Vector3(0.0f, -0.05f, 0.03f);
	private const float viveTriggerRadius = 0.03f;


	public float laserRange = Mathf.Infinity;

	private CelestialBody _selectedObject;
	private CelestialBody _hoveredObject;
	private MenuControl _hoveredControl;

	public Vector3 WorldPosition
	{
		get { return this.transform.position; }
	}
	
	public GameObject SelectedObject
	{
		get
		{
			if (_selectedObject)
				return _selectedObject.gameObject;
			else
				return null;
		}
	}

	private void Start () {
		this.tag = "InteractHand";

		lr = GetComponent<LineRenderer>();
		lr.enabled = false;

		rb = GetComponent<Rigidbody>();
		rb.isKinematic = true;

		selectRegion = GetComponent<SphereCollider>();

		if (usingVive)
		{
			selectRegion.center = viveTriggerPos;
			selectRegion.radius = viveTriggerRadius;
		}
		else
		{
			selectRegion.center = touchTriggerPos;
			selectRegion.radius = touchTriggerRadius;
		}
	}
	
	private void Update()
	{
		UpdateSelectLaser();
	}

	#region selecting objects

	public void ActivateSelectLaser()
	{
		//lr.enabled = true;
		lr.enabled = !lr.enabled;
	}

	// for updating the laser's start and end positions
	private void UpdateSelectLaser()
	{
		// don't bother updating if laser isn't on
		if (lr.enabled)
		{
			// render laser
			Ray ray = new Ray(this.transform.position, this.transform.forward);
			Vector3 startPos = this.transform.position;
			Vector3 endPos = ray.GetPoint(laserRange);

			// highlight object being aimed at
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				endPos = hit.point;

				CelestialBody newHoveredBody = hit.collider.GetComponent<CelestialBody>();
				if (newHoveredBody)
				{
					_hoveredObject = newHoveredBody;
					_hoveredObject.Highlight();
				}
				else if (_hoveredObject && _hoveredObject != _selectedObject)
				{
					_hoveredObject.RemoveHighlight();
					_hoveredObject = null;
				}
			}
			else if (_hoveredObject && _hoveredObject != _selectedObject)
			{
				_hoveredObject.RemoveHighlight();
				_hoveredObject = null;
			}

			lr.SetPositions(new Vector3[] {startPos, endPos});

		}
	}

	public GameObject Select()
	{
		// if an object is already selected, remove highlight
		if (_selectedObject && lr.enabled)
		{
			Debug.Log("deselecting object " + _selectedObject.ToString());
			Deselect();
		}
		
		//Debug.Log("attempting to select object " + _hoveredObject);

		// select the object that the laser is pointing at
		if (_hoveredObject && lr.enabled)
		{
			_selectedObject = _hoveredObject;
			_hoveredObject = null;
			_selectedObject.Highlight();
			Debug.Log("_selectedObject object " + _selectedObject.ToString());
		}

		lr.enabled = false;

		return SelectedObject;
	}

	public void Deselect()
	{
		_selectedObject.RemoveHighlight();
		_selectedObject = null;
	}


	#endregion

	#region menu interaction

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Control"))
		{
			_hoveredControl = other.GetComponent<MenuControl>();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Control"))
		{
			_hoveredControl = null;
			this.StopUsingControl();
		}
	}

	public void UseControl()
	{
		if (_hoveredControl)
		{
			_hoveredControl.Use();

			MenuSlider slider;
			if (slider = _hoveredControl.GetComponent<MenuSlider>())
			{
				slider.interactingHand = this;
			}

		}
	}

	public void StopUsingControl()
	{
		MenuSlider slider;
		if (_hoveredControl && (slider = _hoveredControl.GetComponent<MenuSlider>()))
		{
			slider.StopUsing();
		}
	}

	#endregion

	public void SetAsSelectingHand()
	{

	}

	public void SetAsMenuHand()
	{

	}

}
