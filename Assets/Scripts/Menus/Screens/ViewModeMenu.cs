using UnityEngine;

public class ViewModeMenu : BaseMenu {

	public override void Init() {}

	public void UpdateTimeMultiplier(float t)
	{
		
	}

	public void GoToEditMenu()
	{
		Debug.Log("going to edit mode");
		menuManager.ActivateMenu(MenuManager.MenuType.Edit);
		sceneManager.SetEditMode();
	}

}
