using UnityEngine;
using System.Collections;

public class currentLevelController : MonoBehaviour {

	public string currentApplicationLevelName;
	public int currentLevel = 0;
	public GameObject[] levels;

	public void nextLevel()
	{
		if (currentLevel < levels.Length - 1) 
		{
			levels[currentLevel].SetActive(false);
			currentLevel++;
			levels[currentLevel].SetActive(true);
		}

	}

	public void previousLevel()
	{
		if (currentLevel > 0)
		{
			levels[currentLevel].SetActive(false);
			currentLevel--;
			levels[currentLevel].SetActive(true);
		}
	}

    void Update()
    {
		if ((Input.GetKeyDown(KeyCode.Escape) || Input.touchCount >= 4) && Time.timeScale > .5f)
        {
            Application.LoadLevel("GameMenu");
        }
		else if ((Input.GetKeyDown(KeyCode.Escape) || Input.touchCount >= 4) && Time.timeScale < .5f)
		{
			Time.timeScale = 1.0f;
			Time.fixedDeltaTime = 10f * Time.fixedDeltaTime;
			Camera.main.GetComponent<cameraController>().switchToFollowingCamera();
			Application.LoadLevel("GameMenu");
		}

		if ((Input.GetKeyDown(KeyCode.R) || Input.touchCount >= 3) && Time.timeScale > .5f)
		{
			Application.LoadLevel(currentApplicationLevelName);
		}
		else if ((Input.GetKeyDown(KeyCode.R) || Input.touchCount >= 3) && Time.timeScale < .5f)
		{
			Time.timeScale = 1.0f;
			Time.fixedDeltaTime = 10f * Time.fixedDeltaTime;
			Camera.main.GetComponent<cameraController>().switchToFollowingCamera();
			Application.LoadLevel(currentApplicationLevelName);
		}
    }

	public void SimulateKeyPressR()
	{
		if (Time.timeScale > .5f)
		{
			Application.LoadLevel(currentApplicationLevelName);
		}
		else if (Time.timeScale < .5f)
		{
			Time.timeScale = 1.0f;
			Time.fixedDeltaTime = 10f * Time.fixedDeltaTime;
			Camera.main.GetComponent<cameraController>().switchToFollowingCamera();
			Application.LoadLevel(currentApplicationLevelName);
		}
	}
}
