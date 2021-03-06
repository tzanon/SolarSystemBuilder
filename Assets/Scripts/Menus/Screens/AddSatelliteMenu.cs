﻿using System.Collections.Generic;
using UnityEngine;

public class AddSatelliteMenu : BaseMenu {

	private BaseMenu previousMenu;

	public GameObject primeStarOptions;
	public GameObject regularStarOptions;
	public GameObject planetOptions;

	public override void Init () {
		base.Init();
		this.ShowOptions ();
	}

	protected override void MakeSliderList()
	{
		_sliders = new List<MenuSlider>();
	}

	private void HideOptions () {
		primeStarOptions.SetActive (false);
		regularStarOptions.SetActive (false);
		planetOptions.SetActive (false);
	}

	private void ShowOptions () {
		CelestialBody sel = sceneManager.user.selectedObject.GetComponent<CelestialBody> ();

		HideOptions ();

		if (!sel)
			return;

		if (sel.CompareTag ("InitialStar")) {
			primeStarOptions.SetActive (true);
			previousMenu = menuManager.primeStarMenu;
		} else if (sel.CompareTag ("RegularStar")) {
			regularStarOptions.SetActive (true);
			previousMenu = menuManager.regularStarMenu;
		} else if (sel.CompareTag ("Planet")) {
			planetOptions.SetActive (true);
			previousMenu = menuManager.planetMenu;
		}
	}
	
	public void AddStar () {
		sceneManager.AddSatellite (SceneManager.CelestialType.Star);
	}

	public void AddPlanet () {
		sceneManager.AddSatellite (SceneManager.CelestialType.Planet);
	}

	public void AddMoon () {
		sceneManager.AddSatellite (SceneManager.CelestialType.Moon);
	}

	public void Back () {
		if (!previousMenu)
			return;
		
		menuManager.ActivateMenu(previousMenu);
	}
	

}