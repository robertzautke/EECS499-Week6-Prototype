using UnityEngine;
using System.Collections;

public class UI_Functions : MonoBehaviour {

	public void UI_StartGame() { 
		Application.LoadLevel("Level1");
	}

	public void UI_ExitGame() { 
		Application.Quit();
	}
}
