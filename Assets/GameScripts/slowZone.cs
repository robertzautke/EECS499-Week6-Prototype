using UnityEngine;
using System.Collections;

public class slowZone : MonoBehaviour {

	private float trueTimeScale = 1.0f;
	private float slowTimeScale = 0.1f;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Time.timeScale = slowTimeScale;
			Time.fixedDeltaTime = .1f * Time.fixedDeltaTime;
			Camera.main.GetComponent<cameraController>().switchToStationaryCamera(this.gameObject);

			//other.GetComponent<characterController>().airGravity = 0.3f;
			//other.GetComponent<characterController>().groundGravity = 0.3f;
			//other.gameObject.rigidbody2D.velocity *= (1 / 5f);
		}		
	}

	void OnTriggerStay2D(Collider2D other)
	{ 
		if(other.gameObject.tag == "Player")
		{
			//Camera.main.GetComponent<cameraController>().switchToStationaryCamera(this.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Time.timeScale = trueTimeScale;
			Time.fixedDeltaTime = 10f * Time.fixedDeltaTime;
			Camera.main.GetComponent<cameraController>().switchToFollowingCamera();

			//other.GetComponent<characterController>().airGravity = 2f;
			//other.GetComponent<characterController>().groundGravity = 2f;
			//other.gameObject.rigidbody2D.velocity *= (5 / 1f);
		}	
	}
}
