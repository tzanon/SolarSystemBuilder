using UnityEngine;

public class EditModeMenu : BaseMenu {

    public override void Init()
    {
        
    }

	public void GoToViewMenu()
	{
		menuManager.ActivateMenu(MenuManager.MenuType.View);
		sceneManager.SetViewMode();
	}

}