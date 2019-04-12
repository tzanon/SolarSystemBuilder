using System.Collections.Generic;
using UnityEngine;

public class OrbitingBodyMenu : BaseMenu {

	public OrbitingBody orbitingBody;
	
	public static float radiusIncrement = 100.0f;

	public MenuSlider tiltXSlider;
	public MenuSlider tiltZSlider;
	public MenuSlider sizeSlider;
	public MenuSlider rotationSlider;
	public MenuSlider luminositySlider;
	public MenuSlider orbitSpeedSlider;

	public override void Init()
	{
		
		orbitingBody = sceneManager.user.selectedObject.GetComponent<OrbitingBody>();
		if (!orbitingBody)
		{
			Debug.LogError("ERROR: NO ORBITING BODY SELECTED");
		}
		
		base.Init();

		next.next = this;
		next.previous = this;
		previous.next = this;
		previous.previous = this;
		/**/
	}
	
	protected override void MakeSliderList()
	{
		_sliders = new List<MenuSlider>();
		
		if (tiltXSlider)
			_sliders.Add(tiltXSlider);
		if (tiltZSlider)
			_sliders.Add(tiltZSlider);
		if (sizeSlider)
			_sliders.Add(sizeSlider);
		if (rotationSlider)
			_sliders.Add(rotationSlider);
		if (luminositySlider)
			_sliders.Add(luminositySlider);
		if (orbitSpeedSlider)
			_sliders.Add(orbitSpeedSlider);
	}
	
	public override void NextPage()
	{
		base.NextPage();
	}

	public override void PreviousPage()
	{
		base.PreviousPage();
	}

	public void NextTexture () {
		Debug.Log ("next texture");
		orbitingBody.NextTexture();
	}

	public void PreviousTexture () {
		Debug.Log ("previous texture");
		orbitingBody.PreviousTexture();
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

	public void IncrementOrbitRad1()
	{
		orbitingBody.OrbitRadius1 += radiusIncrement;
	}

	public void DecrementOrbitRad1()
	{
		orbitingBody.OrbitRadius1 -= radiusIncrement;
	}

	public void IncrementOrbitRad2()
	{
		orbitingBody.OrbitRadius2 += radiusIncrement;
	}

	public void DecrementOrbitRad2()
	{
		orbitingBody.OrbitRadius2 -= radiusIncrement;
	}
	
	public void ChangeOrbitSpeed()
	{
		orbitingBody.SetOrbitSpeedByPercent(orbitSpeedSlider.Value);
	}
	
	public void ListSatellitesToAdd ()
	{
		menuManager.ActivateMenu(MenuManager.MenuType.AddSatellite);
	}

	public override void SetInitialSliderValues()
	{
		base.SetInitialSliderValues();

		if (tiltXSlider)
			tiltXSlider.Value = orbitingBody.GetTiltXPercent();
		if (tiltZSlider)
			tiltZSlider.Value = orbitingBody.GetTiltZPercent();
		if (sizeSlider)
			sizeSlider.Value = orbitingBody.GetSizePercent();
		if (rotationSlider)
			rotationSlider.Value = orbitingBody.GetRotationVelocityPercent();
		if (luminositySlider)
			luminositySlider.Value = orbitingBody.GetLuminosityPercent();
		if (orbitSpeedSlider)
			orbitSpeedSlider.Value = orbitingBody.GetOrbitSpeedPercent();
	}
	
}
