using UnityEngine;

public class ViewModeMenu : BaseMenu {

	public override void Init()
    {
        
    }

	public void GoToEditMenu()
	{
		menuManager.ActivateMenu(MenuManager.MenuType.Edit);
		sceneManager.SetEditMode();
	}

}
