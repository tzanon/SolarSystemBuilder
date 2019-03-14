using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class User : MonoBehaviour {

	SteamVR_Action_Boolean toggleLaser;
	SteamVR_Action_Boolean select;
	SteamVR_Action_Boolean menuInteract;
	SteamVR_Action_Boolean teleport;

	
	void Start () {
		toggleLaser = SteamVR_Actions.TZ643P.ToggleLaser;
		select = SteamVR_Actions.TZ643P.Select;
		menuInteract = SteamVR_Actions.TZ643P.InteractUI;
		teleport = SteamVR_Actions.TZ643P.Teleport;
	}

	void Update () {

		if (toggleLaser.GetStateDown (SteamVR_Input_Sources.LeftHand)) {

		}

		if (toggleLaser.GetStateDown (SteamVR_Input_Sources.RightHand)) {

		}

	}
}