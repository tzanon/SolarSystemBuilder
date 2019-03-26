using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMenu : MonoBehaviour {

	public static SceneManager sceneManager;
	public static MenuManager menuManager;

	public virtual void Show () {
		this.gameObject.SetActive (true);
	}

	public virtual void Hide () {
		this.gameObject.SetActive (false);
	}

}