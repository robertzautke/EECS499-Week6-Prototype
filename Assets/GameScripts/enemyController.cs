using UnityEngine;
using System.Collections;

public class enemyController : MonoBehaviour {

    public int health = 3;

	public GameObject player;
	public GameObject spawner;

    public Sprite face_smile;
    public Sprite face_OK;
    public Sprite face_frown;

	Vector3 position;

	public float movementVelocity = 3f;
	public bool canJump = true;
	public int airTime = 0;
	public int temp_airTime = 10;

	public int force = 5;
	public float airTorque = 1f;
	public float groundTorque = 5f;

	public int airGravity = 2;
	public float groundGravity = 0.65f;

	public int temp_briefGravityTurnOn = 40;
	public int briefGravityTurnOn = 0;

	public int stillTime = 0;
	private Vector3 lastposition = new Vector3(0, 0, 0);
	public bool W = false;
	public bool A = false;
	public bool S = false;
	public bool D = false;

	public float attackDistance = 8.0f;

	void Start()
	{
		player = GameObject.Find("Box(Sprite)Player");
	}

	void Update()
	{
		/////////////// Check Movement Conditions ////////////////////////////////////////////

		position = this.gameObject.transform.position;

		float distance = Vector3.Distance(player.transform.position, this.transform.position);

		if(distance < attackDistance)
		{ 
			Vector3 heading = player.transform.position - this.transform.position;
			Vector3 direction = heading / distance;

			if (position == lastposition)
			{
				stillTime++;
				if (stillTime > 100)
				{
					stillTime = 0;
					W = true;
					if (direction.x < 0)
					{
						A = true;
					}
					if (direction.x > 0)
					{
						D = true;
					}
				}
			}

			if(direction.y < 0)
			{ 
				W = true;
			}
			if (direction.x < 0)
			{
				A = true;
			}
			if (direction.y > 0)
			{
				S = true;
			}
			if (direction.x > 0)
			{
				D = true;
			}
		}

		lastposition = position;

		/////////////// Movement Physics/Controls ////////////////////////////////////////////

		if (canJump)
		{
			this.GetComponent<Rigidbody2D>().gravityScale = groundGravity;
		}

		if (W)
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
				W = false;
			}

			if (A)
			{
				rigidbody2D.AddTorque(groundTorque);
				rigidbody2D.AddForce(new Vector2(-movementVelocity, 0), ForceMode2D.Force);
				A = false;
			}
			if (D)
			{
				rigidbody2D.AddTorque(-groundTorque);
				rigidbody2D.AddForce(new Vector2(+movementVelocity, 0), ForceMode2D.Force);
				D = false;
			}
		}
		else if (A)
		{
			rigidbody2D.AddForce(new Vector2(-movementVelocity, 0), ForceMode2D.Force);
			A = false;
		}
		else if (S)
		{
			rigidbody2D.AddForce(new Vector2(0, -movementVelocity), ForceMode2D.Force);
			S = false;
		}
		else if (D)
		{
			rigidbody2D.AddForce(new Vector2(+movementVelocity, 0), ForceMode2D.Force);
			D = false;
		}
		else
		{

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

		if (airTime > 0)
		{
			canJump = false;
			airTime--;
		}
		else if (airTime <= 0 && !canJump)
		{

			this.GetComponent<Rigidbody2D>().gravityScale = airGravity;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Floor")
		{
			canJump = true;
			this.GetComponent<Rigidbody2D>().gravityScale = groundGravity;
		}


        else if (other.gameObject.tag == "Player")
        {
            health--;

            if (health <= 2)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = face_OK;
            }

            if (health <= 1)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = face_frown;
            }

            if (health <= 0)
            {
                //health = 3;
				if(spawner != null)
				{ 
					spawner.GetComponent<enemySpawner>().currentNumberOfEnemies--;
				}
                Destroy(this.gameObject);
            }
        }

		else if (other.gameObject.tag == "instaKill")
		{
			spawner.GetComponent<enemySpawner>().currentNumberOfEnemies--;
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{ 
		if(other.gameObject.tag == "PlayerAttack")
		{
			if (spawner != null)
			{
				spawner.GetComponent<enemySpawner>().currentNumberOfEnemies--;
			}
			Destroy(this.gameObject);
		}
	}
}
