using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour {

	//public delegate void OnClick();
	public UnityEvent OnPress;

	private readonly Color[] colours = { Color.red, Color.blue, Color.green };

	private Text text;

	void Start () {
		text = GetComponent<Text>();
	}
	
	public void Press()
	{
		OnPress.Invoke();
	}

	public void ChangeColour()
	{
		int idx = Random.Range(0, colours.Length);
		text.color = colours[idx];
	}

}
