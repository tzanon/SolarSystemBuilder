using System.Collections.Generic;
using UnityEngine;

public class OrbitingBodyMenu : BaseMenu {

	public OrbitingBody orbitingBody;
	
	public MenuSlider tiltXSlider;
	public MenuSlider tiltZSlider;
	public MenuSlider sizeSlider;
	public MenuSlider rotationSlider;
	public MenuSlider luminositySlider;
	public MenuSlider orbitSpeedSlider;
	public MenuSlider radius1Slider;
	public MenuSlider radius2Slider;

	public override void Init()
	{
		base.Init();
		orbitingBody = sceneManager.user.selectedObject.GetComponent<OrbitingBody>();
	}
	
	protected override void MakeSliderList()
	{
		_sliders = new List<MenuSlider> { tiltXSlider, tiltZSlider, sizeSlider, rotationSlider,
			orbitSpeedSlider, radius1Slider, radius2Slider };
		
		if (luminositySlider)
			_sliders.Add(luminositySlider);
	}
	
	public void NextTexture () {
		Debug.Log ("next texture");
	}

	public void PreviousTexture () {
		Debug.Log ("previous texture");
	}
	
	public void ChangeTiltX()
	{
		orbitingBody.SetTiltXByPercent(tiltXSlider.Value);
	}
	
	public void ChangeTiltZ()
	{
		orbitingBody.SetTiltZByPercent(tiltZSlider.Value);
	}
	
	public void ChangeSize()
	{
		orbitingBody.SetSizeByPercent(sizeSlider.Value);
	}
	
	public void ChangeRotationVelocity()
	{
		orbitingBody.SetRotationVelocityByPercent(rotationSlider.Value);
	}
	
	public void ChangeLuminosity()
	{
		orbitingBody.SetLuminosityPercent(luminositySlider.Value);
	}
	
	public void ChangeOrbitRad1()
	{
		
	}
	
	public void ChangeOrbitRad2()
	{
		
	}
	
	public void ChangeOrbitSpeed()
	{
		
	}
	
	public void ListSatellitesToAdd ()
	{
		menuManager.ActivateMenu(MenuManager.MenuType.AddSatellite);
	}

	public override void SetInitialSliderValues()
	{

	}
	
}
