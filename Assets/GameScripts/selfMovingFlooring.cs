using UnityEngine;
using System.Collections;

public class selfMovingFlooring : MonoBehaviour {

	public float movementDistance = 7.2f;
	public float moveX = 0f;
	public bool startRight = true;

	Vector3 startPosition;
	bool goRight = true;

	void Start () {
		startPosition = transform.position;
		goRight = true;
	}

	void Update () {

		if(startRight)
		{
			if (goRight)
			{
				transform.position = new Vector3(transform.position.x + moveX, transform.position.y, transform.position.z);
			}
			else
			{
				transform.position = new Vector3(transform.position.x - moveX, transform.position.y, transform.position.z);
			}

			if (transform.position.x - startPosition.x >= movementDistance)
			{
				goRight = false;
			}
			else if (transform.position.x - startPosition.x <= 0f)
			{
				goRight = true;
			}		
		}
		else
		{
			if (goRight)
			{
				transform.position = new Vector3(transform.position.x - moveX, transform.position.y, transform.position.z);
			}
			else
			{
				transform.position = new Vector3(transform.position.x + moveX, transform.position.y, transform.position.z);
			}

			if (transform.position.x - startPosition.x <= -movementDistance)
			{
				goRight = false;
			}
			else if (transform.position.x - startPosition.x >= 0f)
			{
				goRight = true;
			}			
		}

	}
}
