using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public SceneManager sm;

	private BaseMenu currentMenu;

	void Start () {
		BaseMenu.sceneManager = this.sm;
		BaseMenu.menuManager = this;
	}

	public void ActivateMenu (BaseMenu menuToActivate) {
		currentMenu.Hide ();

		currentMenu = menuToActivate;
		currentMenu.Show ();
	}

}