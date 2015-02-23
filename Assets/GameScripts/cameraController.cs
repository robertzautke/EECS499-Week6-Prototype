using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

	public GameObject Player;
	public GameObject stationaryObject;
	private bool zoomedOut = false;

	enum state {FOLLOWING, STATIONARY};
	state currentState = state.FOLLOWING;
	void Start() {
		Screen.orientation = ScreenOrientation.Landscape;
	}

	public static cameraController Instance
	{
		get { return instance; }
	}
	private static cameraController instance = null;

	void Awake() {

	}

	public Vector3 cameraPosition = new Vector3(0f,19.2f,-50f);
	void Update () {

		if(this.GetComponent<AudioSource>().loop == false)
		{ 
			this.GetComponent<AudioSource>().loop = true;
		}


		if (currentState == state.FOLLOWING)
		{
			if(Player != null)
			{ 
				cameraPosition.x = Player.transform.position.x;
				cameraPosition.y = 19.2f;
				this.gameObject.transform.position = cameraPosition;
			}
			else
			{ 
				Player = GameObject.Find("Box(Sprite)Player");

			}


			Vector3 zoomOutPos = new Vector3(73.2f, 19.2f, -50f);
			if(Player != null)
				zoomOutPos.x = Player.transform.position.x;

			this.gameObject.transform.position = zoomOutPos;

			cameraPosition = zoomOutPos;
			this.GetComponent<Camera>().rect = new Rect(0,0,2,1);
			this.GetComponent<Camera>().orthographicSize = 20;
			zoomedOut = true;
		}
		else if(currentState == state.STATIONARY)
		{

			cameraPosition.x = stationaryObject.transform.position.x;
			cameraPosition.y = 19.2f;
			this.gameObject.transform.position = cameraPosition;

			Vector3 zoomOutPos = new Vector3(73.2f, 19.2f, -50f);
			zoomOutPos.x = stationaryObject.transform.position.x;
			this.gameObject.transform.position = zoomOutPos;

			cameraPosition = zoomOutPos;
			this.GetComponent<Camera>().rect = new Rect(0, 0, 2, 1);
			this.GetComponent<Camera>().orthographicSize = 20;
			zoomedOut = true;
		}
	}

	public void switchToStationaryCamera(GameObject inStationaryObject)
	{ 
		currentState = state.STATIONARY;
		stationaryObject = inStationaryObject;

	}

	public void switchToFollowingCamera()
	{ 
		currentState = state.FOLLOWING;
	}
}
