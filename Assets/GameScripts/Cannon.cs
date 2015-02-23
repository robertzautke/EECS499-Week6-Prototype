using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	bool active = false;
	bool canRotate = true;
	public float cannonForce = 10f;

	Vector3 initialMouseClickPositon;
	Vector3 finalMouseClickPosition;
	Transform player;

	void Update()
	{
		if(Input.GetKey(KeyCode.Space))
		{ 
			return;
		}

		if(!active)
		{ 
			return;
		}

		if(Input.GetMouseButtonDown(0))
		{
			canRotate = false;
			initialMouseClickPositon = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
		}

		if(Input.GetMouseButton(0) && canRotate == false)
		{
			player = transform.FindChild("Box(Sprite)Player");
			if(player.localPosition.x > -1f)
				player.localPosition = new Vector3(player.localPosition.x - .01f, player.localPosition.y, player.localPosition.z);
		}

		if(Input.GetMouseButtonUp(0))
		{
			finalMouseClickPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);

			Vector3 cannonBlast = transform.right.normalized * cannonForce * Mathf.Abs(player.localPosition.x);
			//Debug.Log(cannonBlast);
			//Debug.Log(Mathf.Abs(player.localPosition.x));

			player.GetComponent<Rigidbody2D>().isKinematic = false;
			player.parent = null;

			player.rigidbody2D.AddForce(new Vector2(cannonBlast.x, cannonBlast.y), ForceMode2D.Impulse);

			canRotate = true;
			

		}

		if(canRotate)
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = 52f; //The distance between the camera and object
			Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
			mousePos.x = mousePos.x - objectPos.x;
			mousePos.y = mousePos.y - objectPos.y;
			float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{ 
		if(other.gameObject.tag == "Player")
		{
			active = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{ 
		if(other.gameObject.tag == "Player" && canRotate)
		{ 
			active = false;
		}
	}
}
