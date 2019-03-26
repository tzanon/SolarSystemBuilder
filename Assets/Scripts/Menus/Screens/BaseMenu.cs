using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMenu : MonoBehaviour {

	public static SceneManager sceneManager;
	public static MenuManager menuManager;

	private void Start () {
		this.Init ();
	}

	public abstract void Init ();

	public virtual void Hide () {
		this.gameObject.SetActive (false);
	}

	/*
	public static void MakeMenuVisible (BaseMenu menu) {
		menu.gameObject.SetActive (true);
		menu.Init ();
	}
	*/

}