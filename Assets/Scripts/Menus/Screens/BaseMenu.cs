using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMenu : MonoBehaviour {

	public static SceneManager sceneManager;
	public static MenuManager menuManager;

	public BaseMenu next;
	public BaseMenu previous;
	
	protected List<MenuSlider> _sliders;
	
	private void Start () {
		this.MakeSliderList();
		this.Init ();
	}

	protected abstract void MakeSliderList();

	public virtual void Init ()
	{
		SetInitialSliderValues();
	}
	
	public virtual void Hide () {
		this.gameObject.SetActive (false);
	}

	public void NextPage()
	{
		if (next)
			menuManager.ActivateMenu(next);
	}
	
	public void PreviousPage()
	{
		if (previous)
			menuManager.ActivateMenu(previous);
	}

	// TODO: set sliders so they reflect the current settings of the selected object
	public abstract void SetInitialSliderValues();
	
}
