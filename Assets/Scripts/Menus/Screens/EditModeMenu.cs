using UnityEngine;

public class EditModeMenu : BaseMenu {

    public override void Init() {}

	public void GoToViewMenu()
	{
		Debug.Log("going to view mode");
		menuManager.ActivateMenu(MenuManager.MenuType.View);
		sceneManager.SetViewMode();
	}

}