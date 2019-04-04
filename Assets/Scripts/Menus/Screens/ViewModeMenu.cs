using UnityEngine;

public class ViewModeMenu : BaseMenu {
	
	public MenuSlider timeSlider;

	//public override void Init() {}

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

	}

}
