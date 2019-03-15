using UnityEngine;
using Valve.VR;

public class User : MonoBehaviour {

	SteamVR_Input_Sources menuHand;
	SteamVR_Input_Sources selectHand;

	SteamVR_Action_Boolean toggleLaser;
	SteamVR_Action_Boolean select;
	SteamVR_Action_Boolean menuInteract;
	SteamVR_Action_Boolean teleport;

	private void Start () {
		menuHand = SteamVR_Input_Sources.LeftHand;
		selectHand = SteamVR_Input_Sources.RightHand;

		toggleLaser = SteamVR_Actions.TZ643P.ToggleLaser;
		select = SteamVR_Actions.TZ643P.Select;
		menuInteract = SteamVR_Actions.TZ643P.InteractUI;
		teleport = SteamVR_Actions.TZ643P.Teleport;
	}

	private void Update () {
		/* toggle selection laser on/off */
		if (toggleLaser.GetStateDown (selectHand)) {

		}
		/* select the hovered object */
		if (select.GetStateDown (selectHand)) {

		}
		/* teleport to the hovered object */
		if (teleport.GetStateDown (selectHand)) {

		}
		/* push a UI button, drag a slider, etc. */
		if (menuInteract.GetStateDown (selectHand)) {

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