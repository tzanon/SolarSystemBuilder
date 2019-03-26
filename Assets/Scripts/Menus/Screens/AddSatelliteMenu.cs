using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSatelliteMenu : BaseMenu {

	public BaseMenu previousMenu;
	
	public GameObject primeStarOptions;
	public GameObject regularStarOptions;
	public GameObject planetOptions;
	
	public BaseMenu primeStarMenu;
	public BaseMenu regularStarMenu;
	public BaseMenu planetMenu;
	
	private void Start () {
		HideOptions ();
	}

	public override void Show () {
		base.Show ();
		this.DisplayOptions ();
	}

	public void HideOptions () {
		primeStarOptions.SetActive (false);
		regularStarOptions.SetActive (false);
		planetOptions.SetActive (false);
	}

	public void DisplayOptions () {
		GameObject sel = sceneManager.user.selectedCB;
		
		HideOptions ();
		
		if (!sel)
			return;
		
		if (sel.CompareTag ("InitialStar")) {
			primeStarOptions.SetActive (true);
			previousMenu = primeStarMenu;
		} else if (sel.CompareTag ("RegularStar")) {
			regularStarOptions.SetActive (true);
			previousMenu = regularStarMenu;
		} else if (sel.CompareTag ("Planet")) {
			planetOptions.SetActive (true);
			previousMenu = planetMenu;
		}
	}
	
}