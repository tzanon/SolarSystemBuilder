using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public enum MenuType { Edit, View, PrimeStar, RegularStar, Planet, Moon, AddSatellite, Orbit }

	private SceneManager sm;

	private BaseMenu currentMenu;

	public EditModeMenu editModeMenu;
	public ViewModeMenu viewModeMenu;
	public PrimeStarMenu primeStarMenu;
	public AddSatelliteMenu addSatelliteMenu;
	public OrbitingBodyMenu regularStarMenu;
	public OrbitingBodyMenu planetMenu;
	public OrbitingBodyMenu moonMenu;
	public OrbitingBodyMenu orbitMenu;

	private Dictionary<MenuType, BaseMenu> menus;

	private void Start () {
		sm = GetComponent<SceneManager>();
		BaseMenu.sceneManager = this.sm;
		BaseMenu.menuManager = this;

		menus = new Dictionary<MenuType, BaseMenu> () {
			{ MenuType.Edit, editModeMenu },
			{ MenuType.View, viewModeMenu },
			{ MenuType.PrimeStar, primeStarMenu },
			{ MenuType.RegularStar, regularStarMenu },
			{ MenuType.Planet, planetMenu },
			{ MenuType.Moon, moonMenu },
			{ MenuType.AddSatellite, addSatelliteMenu },
			{ MenuType.Orbit, orbitMenu }
		};
	}

	public void InitMenus()
	{
		foreach (BaseMenu menu in menus.Values)
		{
			//menu.gameObject.SetActive(true);
			menu.gameObject.SetActive(false);
		}
	}

	public void ActivateMenu (MenuType type) {
		this.ActivateMenu(menus[type]);
	}

	public void ActivateMenu (BaseMenu menu) {
		if (currentMenu)
			currentMenu.Hide ();

		currentMenu = menu;
		currentMenu.gameObject.SetActive (true);
		currentMenu.Init ();
	}

}