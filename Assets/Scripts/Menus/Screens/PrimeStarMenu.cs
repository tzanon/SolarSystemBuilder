using UnityEngine;

public class PrimeStarMenu : BaseMenu {

	public CelestialBody initialStar;

	public MenuSlider tiltXSlider;
	public MenuSlider tiltZSlider;
	public MenuSlider sizeSlider;
	public MenuSlider rotationSlider;
	public MenuSlider luminositySlider;

	public override void Init () {
		base.Init();
		initialStar = sceneManager.initialStar;
	}

	public void NextTexture () {
		Debug.Log ("next texture");
		
	}

	public void PreviousTexture () {
		Debug.Log ("previous texture");
		
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

	}

}