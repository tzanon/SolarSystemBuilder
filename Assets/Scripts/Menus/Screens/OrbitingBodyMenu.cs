
using UnityEngine;

public class OrbitingBodyMenu : BaseMenu {

	public OrbitingBody orbitingBody;
	
	public override void Init()
	{
		
		orbitingBody = sceneManager.user.selectedObject.GetComponent<OrbitingBody>();
	}
	
	public void NextTexture () {
		Debug.Log ("next texture");
	}

	public void PreviousTexture () {
		Debug.Log ("previous texture");
	}
	
	public void ChangeTiltX(float sliderValue)
	{
		
	}
	
	public void ChangeTiltZ(float sliderValue)
	{
		
	}
	
	public void ChangeSize(float sliderValue)
	{
		
	}
	
	public void ChangeRotationVelocity(float sliderValue)
	{
		
	}
	
	public void ChangeLuminosity(float sliderValue)
	{
		
	}
	
	public void ChangeOrbitRad1(float sliderValue)
	{
		
	}
	
	public void ChangeOrbitRad2(float sliderValue)
	{
		
	}
	
	public void ChangeOrbitSpeed(float sliderValue)
	{
		
	}
	
	public void ListSatellitesToAdd ()
	{
		menuManager.ActivateMenu(MenuManager.MenuType.AddSatellite);
	}
}
