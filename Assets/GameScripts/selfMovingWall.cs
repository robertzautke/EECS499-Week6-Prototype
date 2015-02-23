using UnityEngine;
using System.Collections;

public class selfMovingWall : MonoBehaviour {

	public float movementDistance = 7.2f;
	public float moveY = 0f;
	public bool startUp = true;

	Vector3 startPosition;
	bool goUp = true;

	void Start()
	{
		startPosition = transform.position;
		goUp = true;
	}

	void Update()
	{

		if(startUp)
		{
			if (goUp)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + moveY, transform.position.z);
			}
			else
			{
				transform.position = new Vector3(transform.position.x, transform.position.y - moveY, transform.position.z);
			}

			if (transform.position.y - startPosition.y >= movementDistance)
			{
				goUp = false;
			}
			else if (transform.position.y - startPosition.y <= 0f)
			{
				goUp = true;
			}		
		}
		else
		{
			if (goUp)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y - moveY, transform.position.z);
			}
			else
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + moveY, transform.position.z);
			}

			if (transform.position.y - startPosition.y <= -movementDistance)
			{
				goUp = false;
			}
			else if (transform.position.y - startPosition.y >= 0f)
			{
				goUp = true;
			}	
		}

	}
}
