using UnityEngine;
using System.Collections;

public class MovableWall : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;

	public bool turnOnColliders = true;
	public bool isDragging = false;
	public bool mouseUp = false;

	void OnMouseDown()
	{
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		foreach(Transform t in transform)
		{ 
			t.collider2D.enabled = false;
		}
	}

	void OnMouseDrag()
	{
		isDragging = true;
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}

	void OnMouseUp()
	{
		isDragging = false;
		mouseUp = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.tag == "Player") { 
			turnOnColliders = false;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")		
			turnOnColliders = true;
	}

	void Update()
	{
		//Debug.Log("turnOnColliders: " + turnOnColliders);
		//Debug.Log("isDragging: " + isDragging);
		//Debug.Log("mouseUp: " + mouseUp);

		if (turnOnColliders && !isDragging && mouseUp)
		{
			//Debug.Log("ReEnable");
			foreach (Transform t in transform)
			{
				t.collider2D.enabled = true;
			}
			mouseUp = false;
		}
	}
}
