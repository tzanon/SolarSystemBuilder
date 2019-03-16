using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class User : MonoBehaviour {

	SteamVR_Input_Sources menuHand;
	SteamVR_Input_Sources selectHand;

	SteamVR_Action_Boolean toggleLaser;
	SteamVR_Action_Boolean select;
	SteamVR_Action_Boolean menuInteract;
	SteamVR_Action_Boolean toggleMenu;
	SteamVR_Action_Boolean teleport;

	public Canvas menu;

	private void Start () {
		menuHand = SteamVR_Input_Sources.LeftHand;
		selectHand = SteamVR_Input_Sources.RightHand;

		toggleLaser = SteamVR_Actions.TZ643P.ToggleLaser;
		select = SteamVR_Actions.TZ643P.Select;
		menuInteract = SteamVR_Actions.TZ643P.InteractUI;
		toggleMenu = SteamVR_Actions.TZ643P.ToggleMenu;
		teleport = SteamVR_Actions.TZ643P.Teleport;

		Debug.Log("starting");
	}

	private void Update () {
		if (toggleMenu.GetStateDown(menuHand)) {
			Debug.Log("toggling menu");
			menu.gameObject.SetActive(!menu.gameObject.activeInHierarchy);
		}

		/* toggle selection laser on/off */
		if (toggleLaser.GetStateDown (selectHand)) {
			Debug.Log("toggling laser");
		}
		/* select the hovered object */
		if (select.GetStateDown (selectHand)) {
			Debug.Log("selecting");
		}
		/* teleport to the hovered object */
		if (teleport.GetStateDown (selectHand)) {
			Debug.Log("teleporting");
		}
		/* push a UI button, drag a slider, etc. */
		if (menuInteract.GetStateDown (selectHand)) {
			Debug.Log("interacting");
		}
	}

	public void SelectWithLeftHand()
	{
		menuHand = SteamVR_Input_Sources.RightHand;
		selectHand = SteamVR_Input_Sources.LeftHand;
	}

	public void SelectWithRightHand()
	{
		menuHand = SteamVR_Input_Sources.LeftHand;
		selectHand = SteamVR_Input_Sources.RightHand;
	}

}