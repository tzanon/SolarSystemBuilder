using System.Collections.Generic;
using UnityEngine;

public class PrimeStarMenu : BaseMenu {
	
	public CelestialBody initialStar;
	
	public MenuSlider tiltXSlider;
	public MenuSlider tiltZSlider;
	public MenuSlider sizeSlider;
	public MenuSlider rotationSlider;
	public MenuSlider luminositySlider;
	
	public override void Init () {
		
		initialStar = sceneManager.initialStar;
		base.Init();
	}

	protected override void MakeSliderList()
	{
		_sliders = new List<MenuSlider> { tiltXSlider, tiltZSlider, sizeSlider, rotationSlider, luminositySlider };
	}

	public override void NextPage()
	{
		//next.next = this;
		//next.previous = this;
		
		base.NextPage();
	}

	public override void PreviousPage()
	{
		//previous.next = this;
		//previous.previous = this;

		base.PreviousPage();
	}

	public void NextTexture () {
		Debug.Log ("next texture");
		initialStar.NextTexture();
	}

	public void PreviousTexture () {
		Debug.Log ("previous texture");
		initialStar.PreviousTexture();
	}
	
	public void ChangeTiltX()
	{
		initialStar.SetTiltXByPercent(tiltXSlider.Value);
	}
	
	public void ChangeTiltZ()
	{
		initialStar.SetTiltZByPercent(tiltZSlider.Value);
	}
	
	public void ChangeSize()
	{
		initialStar.SetSizeByPercent(sizeSlider.Value);
	}
	
	public void ChangeRotationVelocity()
	{
		initialStar.SetRotationVelocityByPercent(rotationSlider.Value);
	}
	
	public void ChangeLuminosity()
	{
		initialStar.SetLuminosityPercent(luminositySlider.Value);
	}
	
	public void ListSatellitesToAdd () {
		menuManager.ActivateMenu(MenuManager.MenuType.AddSatellite);
	}
	
	public override void SetInitialSliderValues()
	{
		base.SetInitialSliderValues();

		tiltXSlider.Value = initialStar.GetTiltXPercent();
		tiltZSlider.Value = initialStar.GetTiltZPercent();
		sizeSlider.Value = initialStar.GetSizePercent();
		rotationSlider.Value = initialStar.GetRotationVelocityPercent();
		luminositySlider.Value = initialStar.GetLuminosityPercent();
	}
}