using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularStarMenu : BaseMenu {
	
	public OrbitingBody star;
	
	public override void Init() {}
	
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
	
	public void ChangeOrbitRad1(float sliderValue)
	{
		
	}
	
	public void ChangeOrbitRad2(float sliderValue)
	{
		
	}
	
	public void ChangeOrbitSpeed(float sliderValue)
	{
		
	}
	
	public void ListSatellitesToAdd () {
		menuManager.ActivateMenu(MenuManager.MenuType.AddSatellite);
	}
	
}
