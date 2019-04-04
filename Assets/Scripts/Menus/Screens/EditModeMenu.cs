using UnityEngine;

public class EditModeMenu : BaseMenu {

    

	public void SaveSolarSystem()
	{
		Debug.Log("Saving system as...");
		// TODO: save solar system with some convention (current date?)
	}

	public void ListSystemsToLoad()
	{
		// TODO: make menu with list of mazes on file
		
	}

	public void GoToViewMenu()
	{
		Debug.Log("going to view mode");
		menuManager.ActivateMenu(MenuManager.MenuType.View);
		sceneManager.SetViewMode();
	}

	public override void SetInitialSliderValues()
	{

	}

}