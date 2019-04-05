using System.Collections.Generic;
using UnityEngine;

public class ViewModeMenu : BaseMenu {
	
	public MenuSlider timeSlider;
	
	protected override void MakeSliderList()
	{
		_sliders = new List<MenuSlider> { timeSlider };
	}
	
	public void UpdateTimeMultiplier()
	{
		sceneManager.SetTimeMultiplierByPercent(timeSlider.Value);
	}
	
	public void GoToEditMenu()
	{
		Debug.Log("going to edit mode");
		menuManager.ActivateMenu(MenuManager.MenuType.Edit);
		sceneManager.SetEditMode();
	}
	
	public override void SetInitialSliderValues()
	{
		base.SetInitialSliderValues();

		timeSlider.Value = CelestialBody.TimeMultiplier;
	}

}
