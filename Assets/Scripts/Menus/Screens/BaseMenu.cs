﻿using UnityEngine;

public abstract class BaseMenu : MonoBehaviour {

	public static SceneManager sceneManager;
	public static MenuManager menuManager;

	public BaseMenu next;
	public BaseMenu previous;
	
	private void Start () {
		this.Init ();
	}

	public abstract void Init ();

	public virtual void Hide () {
		this.gameObject.SetActive (false);
	}

	public void NextPage()
	{
		menuManager.ActivateMenu(next);
	}
	
	public void PreviousPage()
	{
		menuManager.ActivateMenu(previous);
	}
	
}
