﻿using UnityEngine;

public class PrimeStarMenu : BaseMenu {

	public AddSatelliteMenu addSatelliteMenu;
	public CelestialBody initialStar;

	public override void Init () {
		initialStar = sceneManager.initialStar;
	}

	public void NextTexture () {
		Debug.Log ("next texture");
		
	}

	public void PreviousTexture () {
		Debug.Log ("previous texture");
		
	}
	
	public void ChangeTiltX(float sliderValue)
	{
		
	}
	
	public void ChangeTiltZ(float sliderValue)
	{
		
	}
	
	public void ChangeSize(float sliderValue)
	{
		
	}
	
	public void ChangeRotationVelocity(float sliderValue)
	{
		
	}
	
	public void ChangeLuminosity(float sliderValue)
	{
		
	}
	
	public void ListSatellitesToAdd () {
		menuManager.ActivateMenu(MenuManager.MenuType.AddSatellite);
	}

}