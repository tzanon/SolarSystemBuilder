﻿using UnityEngine;
using Valve.VR;

public class User : MonoBehaviour {

	public bool debugMode = false;

	/* scene and UI managers */
	public SceneManager sceneManager;
	public MenuManager menuManager;

	/* interaction things */
	public GameObject selectedObject;
	public InteractHand interactingHand;

	/* only if there's enough time */
	public GameObject leftHand, RightHand;
	private SteamVR_Input_Sources menuHand;
	private SteamVR_Input_Sources selectHand;

	/* SteamVR actions */
	public SteamVR_ActionSet actionSetEnable;
	public SteamVR_Action_Boolean toggleLaser;
	public SteamVR_Action_Boolean select;
	public SteamVR_Action_Boolean menuInteract;
	public SteamVR_Action_Boolean toggleMenu;
	public SteamVR_Action_Boolean teleport;

	// for debugging
	SteamVR_Action_Boolean toggle2;
	SteamVR_Action_Boolean select2;

	public Canvas menu;

	private void Start () {
		actionSetEnable.Activate ();
		this.SelectWithRightHand();
		toggle2 = SteamVR_Actions._default.GrabPinch;
		select2 = SteamVR_Actions._default.GrabGrip;
	}

	/* VR input handling */
	private void Update () {
		/* toggle interaction menu */
		if (toggleMenu.GetStateDown (menuHand)) {
			Debug.Log ("toggling menu");
			menu.gameObject.SetActive(!menu.gameObject.activeInHierarchy);
		}

		/* toggle selection laser on/off */
		if (toggleLaser.GetStateDown (selectHand)) {
			Debug.Log ("toggling laser");
			interactingHand.ActivateSelectLaser();
		}
		/* select the hovered object */
		if (select.GetStateDown (selectHand)) {
			Debug.Log ("selecting");
			this.SelectObject();
		}
		/* teleport to the hovered object */
		if (teleport.GetStateDown (selectHand)) {
			Debug.Log ("teleporting");
			sceneManager.TeleportToSystemViepoint();
		}
		/* push a UI button, drag a slider, etc. */
		if (menuInteract.GetStateDown (selectHand)) {
			Debug.Log ("interacting");
			interactingHand.UseControl();
			//interactingHand.ControlCast();
		}
		if (menuInteract.GetStateUp (selectHand)) {
			// stop using control (slider)
			interactingHand.StopUsingControl();
		}
		if (menuInteract.GetState(selectHand))
		{
			// shoot raycast to manipulate slider
			interactingHand.ControlCast();
		}

		// VR control debugging
		if (debugMode)
		{
			if (toggle2.GetStateDown(SteamVR_Input_Sources.Any))
			{
				Debug.Log("default: toggling laser");
			}
			if (select2.GetStateDown(SteamVR_Input_Sources.Any))
			{
				Debug.Log("defaulting: selecting");
			}
		}
	}

	public void SelectWithLeftHand () {
		menuHand = SteamVR_Input_Sources.RightHand;
		selectHand = SteamVR_Input_Sources.LeftHand;
	}
	public void SelectWithRightHand () {
		menuHand = SteamVR_Input_Sources.LeftHand;
		selectHand = SteamVR_Input_Sources.RightHand;
	}

	/* turn laser on/off */
	public void ToggleSelectLaser()
	{
		interactingHand.ActivateSelectLaser();
	}

	/* select a star/planet/moon */
	private void SelectObject()
	{
		selectedObject = interactingHand.Select();
		if (selectedObject)
		{
			if (selectedObject.CompareTag("InitialStar"))
			{
				menuManager.ActivateMenu(MenuManager.MenuType.PrimeStar);
			}
			else if (selectedObject.CompareTag("RegularStar"))
			{
				menuManager.ActivateMenu(MenuManager.MenuType.RegularStar);
			}
			else if (selectedObject.CompareTag("Planet"))
			{
				menuManager.ActivateMenu(MenuManager.MenuType.Planet);
			}
			else if (selectedObject.CompareTag("Moon"))
			{
				menuManager.ActivateMenu(MenuManager.MenuType.Moon);
			}
		}
		else
		{
			menuManager.ActivateMenu(MenuManager.MenuType.Edit);
			sceneManager.SetEditMode ();
		}
	}

}
