using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour {

	public GameObject currentLevelController;
    public int health = 6;

    public Sprite face_smile;
    public Sprite face_OK;
    public Sprite face_frown;

	public Sprite face_smile_Stars;
	public Sprite face_OK_Stars;
	public Sprite face_frown_Stars;

	public float movementVelocity = 3f;
	public bool canJump = true;
	public int airTime = 0;
	public int temp_airTime = 10;

	public float force = 5.0f;
	public float airTorque = 1f;
	public float groundTorque = 5f;

	public float airGravity = 2;
	public float groundGravity = 0.65f;

	public int temp_briefGravityTurnOn = 40;
	public int briefGravityTurnOn = 0;

	private int starTime;
	public float angularVelocity;

	public bool W = false;
	public bool A = false;
	public bool S = false;
	public bool D = false;

//////////////////////////////////////////////////////////////////////////////////////

    // Values to set:
	public float comfortZone = 1000000.0f;
	public float minSwipeDist = 2 * ((float)Screen.height / 3.0f);
	public float maxSwipeTime = 1.0f;
 
    private float startTime;
    private Vector2 startPos;
    private bool couldBeSwipeUpDown;
	private bool couldBeSwipeLeftRight;
	
	public int swipeTimer = 6;
	private int tempSwipeTimer = 6;

    public enum SwipeDirection {
        None,
        Up,
        Down,
		Left,
		Right
    }

	public SwipeDirection lastSwipe = characterController.SwipeDirection.None;
	public float lastSwipeTime;

	Vector2 mouseInit;
	Vector2 mouseFinal;
	float distance;

//////////////////////////////////////////////////////////////////////////////////////

	void Update () 
	{
/////////////// TouchScreen Handler //////////////////////////////////////////////////


        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
			
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    lastSwipe = characterController.SwipeDirection.None;
                                        lastSwipeTime = 0;
                    couldBeSwipeUpDown = true;
					couldBeSwipeLeftRight = true;
                    startPos = touch.position;
                    startTime = Time.time;
					tempSwipeTimer = swipeTimer;
                    break;
                case TouchPhase.Moved:

					float swipeValue = Mathf.Sign(touch.position.y - startPos.y);

					if (tempSwipeTimer > 0)
					{
						if (swipeValue > 0)
						{
							if ((touch.position.y - startPos.y) > minSwipeDist)
							{
								lastSwipe = characterController.SwipeDirection.Up;
								W = true;
							}

						}
						else if (swipeValue < 0)
						{
							lastSwipe = characterController.SwipeDirection.Down;
							S = true;
						}

						swipeValue = Mathf.Sign(touch.position.x - startPos.x);

						if (swipeValue > 0)
						{
							lastSwipe = characterController.SwipeDirection.Right;
							D = true;
							if(canJump)
							{
								movementVelocity = 5.0f * (Mathf.Abs(touch.position.x - startPos.x) / (float)Screen.width);
							}
							else
							{
								movementVelocity = 10.0f * (Mathf.Abs(touch.position.x - startPos.x) / (float)Screen.width);
							}
							
						}
						else if (swipeValue < 0)
						{
							lastSwipe = characterController.SwipeDirection.Left;
							A = true;
							if (canJump)
							{
								movementVelocity = 5.0f * (Mathf.Abs(touch.position.x - startPos.x) / (float)Screen.width);
							}
							else
							{
								movementVelocity = 10.0f * (Mathf.Abs(touch.position.x - startPos.x) / (float)Screen.width);
							}
						}
						tempSwipeTimer--;
					}
                    break;

                case TouchPhase.Ended:
					tempSwipeTimer = swipeTimer;
                    break;
            }
        }

/////////////// Movement Physics/Controls ////////////////////////////////////////////

		if(canJump)
		{
			this.GetComponent<Rigidbody2D>().gravityScale = groundGravity;
		}

		if (Input.GetKey(KeyCode.W) || W)
		{
			if (canJump) 
			{
				airTime = temp_airTime;
				if (airTime <= 0)
				{
					canJump = false;
					briefGravityTurnOn = temp_briefGravityTurnOn;
					airTime = 0;
				}
				else 
				{
					rigidbody2D.AddForce(new Vector2(0, force), ForceMode2D.Impulse);


					if (Input.GetKey(KeyCode.A))
					{
						rigidbody2D.AddTorque(airTorque);		
					}

					if (Input.GetKey(KeyCode.D))
					{
						rigidbody2D.AddTorque(-airTorque);
					}
					airTime--;
				}

			}
			W = false;
			if (Input.GetKey(KeyCode.A) || A)
			{
				rigidbody2D.AddTorque(groundTorque);
				rigidbody2D.AddForce(new Vector2(-movementVelocity, 0), ForceMode2D.Impulse);
				A = false;
			}
			if (Input.GetKey(KeyCode.D) || D)
			{
				rigidbody2D.AddTorque(-groundTorque);
				rigidbody2D.AddForce(new Vector2(+movementVelocity, 0), ForceMode2D.Impulse);
				D = false;
			}
		}
		else if (Input.GetKey(KeyCode.A) || A)
		{
			rigidbody2D.AddTorque(groundTorque);
			rigidbody2D.AddForce(new Vector2(-movementVelocity, 0), ForceMode2D.Impulse);
			A = false;
		}
		else if (Input.GetKey(KeyCode.S) || S)
		{
			rigidbody2D.AddForce(new Vector2(0, -movementVelocity), ForceMode2D.Impulse);
			S = false;
		}
		else if (Input.GetKey(KeyCode.D) || D)
		{

			rigidbody2D.AddTorque(-groundTorque);
			rigidbody2D.AddForce(new Vector2(+movementVelocity, 0), ForceMode2D.Impulse);
			D = false;
		}

		if (briefGravityTurnOn > 0)
		{
			briefGravityTurnOn--;
			this.GetComponent<Rigidbody2D>().gravityScale = airGravity;
		}
		else
		{
			this.GetComponent<Rigidbody2D>().gravityScale = groundGravity;		
		}

		if(airTime > 0)
		{
			canJump = false;
			airTime--;
		}
		else if(airTime <= 0 && !canJump)
		{

			this.GetComponent<Rigidbody2D>().gravityScale = airGravity;
		}

/////////////////////// Character Face Handler and Attack Handling ///////////////////////////////

		angularVelocity = this.rigidbody2D.angularVelocity;
		if (Mathf.Abs(this.rigidbody2D.angularVelocity) > 2000)
		{
			starTime = 100;
		}
		if(starTime > 0)
		{ 
			starTime--;
		}
		if(this.GetComponent<SpriteRenderer>().sprite == face_smile && starTime > 0)
		{ 
			this.GetComponent<SpriteRenderer>().sprite = face_smile_Stars;
			int children = this.transform.childCount;
			for(int i = 0; i < children; i++)
			{ 
				this.transform.GetChild(i).gameObject.SetActive(true);
			}
		}
		else if (this.GetComponent<SpriteRenderer>().sprite == face_smile_Stars && starTime <= 0)
		{
			this.GetComponent<SpriteRenderer>().sprite = face_smile;
			int children = this.transform.childCount;
			for (int i = 0; i < children; i++)
			{
				this.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
		else if (this.GetComponent<SpriteRenderer>().sprite == face_OK && starTime > 0)
		{
			this.GetComponent<SpriteRenderer>().sprite = face_OK_Stars;
			int children = this.transform.childCount;
			for (int i = 0; i < children; i++)
			{
				this.transform.GetChild(i).gameObject.SetActive(true);
			}
		}
		else if (this.GetComponent<SpriteRenderer>().sprite == face_OK_Stars && starTime <= 0)
		{
			this.GetComponent<SpriteRenderer>().sprite = face_OK;
			int children = this.transform.childCount;
			for (int i = 0; i < children; i++)
			{
				this.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
		else if (this.GetComponent<SpriteRenderer>().sprite == face_frown && starTime > 0)
		{
			this.GetComponent<SpriteRenderer>().sprite = face_frown_Stars;
			int children = this.transform.childCount;
			for (int i = 0; i < children; i++)
			{
				this.transform.GetChild(i).gameObject.SetActive(true);
			}
		}
		else if (this.GetComponent<SpriteRenderer>().sprite == face_frown_Stars && starTime <= 0)
		{
			this.GetComponent<SpriteRenderer>().sprite = face_frown;
			int children = this.transform.childCount;
			for (int i = 0; i < children; i++)
			{
				this.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
//////////////////// Level Controls /////////////////////////////////////////

		if(Input.GetKeyDown(KeyCode.UpArrow))
		{ 
			currentLevelController.GetComponent<currentLevelController>().nextLevel();
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			currentLevelController.GetComponent<currentLevelController>().previousLevel();		
		}
/////////////////////////////////////////////////////////////////////////////
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Floor")
		{
			canJump = true;
			this.GetComponent<Rigidbody2D>().gravityScale = groundGravity;
		}

        else if (other.gameObject.tag == "Enemy")
        {
			if(other.gameObject.GetComponent<enemyController_Triangle >() != null)
			{
				health -= 3;
			}
			canJump = true;
			this.GetComponent<Rigidbody2D>().gravityScale = groundGravity;

            health--;

            if (health <= 4)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = face_OK;
            }

            if (health <= 2)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = face_frown;
            }

            if (health <= 0)
            {
                health = 6;
                //Application.LoadLevel(currentLevelController.GetComponent<currentLevelController>().currentApplicationLevelName);
				GameObject.Find("CurrentLevelController").GetComponent<currentLevelController>().SimulateKeyPressR();
            }
        }
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Cannon")
		{
			this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
			this.gameObject.transform.parent = other.gameObject.transform;

			this.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
			this.gameObject.transform.localRotation = new Quaternion(0f,0f,0f,0f);
		}
	}
}
