using UnityEngine;

public abstract class BaseMenu : MonoBehaviour {

	public static SceneManager sceneManager;
	public static MenuManager menuManager;

	private void Start () {
		this.Init ();
	}

	public abstract void Init ();

	public virtual void Hide () {
		this.gameObject.SetActive (false);
	}

}