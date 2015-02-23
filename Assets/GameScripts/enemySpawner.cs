using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemySpawner : MonoBehaviour {

	public GameObject enemyType;
	private List<Object> spawnedEnemies = new List<Object>();

	public int numberOfEnemies = 1;
	public int currentNumberOfEnemies = 0;

	void Start () {
	
	}
	void Update () {
		if(currentNumberOfEnemies < numberOfEnemies)
		{ 
			GameObject newEnemy = (GameObject)Object.Instantiate(enemyType, this.transform.position, this.transform.rotation);
			if(newEnemy.GetComponent<enemyController>() != null)
				newEnemy.GetComponent<enemyController>().spawner = this.gameObject;
			else if(newEnemy.GetComponent<enemyController_Triangle>() != null)
				newEnemy.GetComponent<enemyController_Triangle>().spawner = this.gameObject;				
			//spawnedEnemies.Add(Object.Instantiate(enemyType, this.transform.position, this.transform.rotation));
			currentNumberOfEnemies++;

		}

		//foreach(Object O in spawnedEnemies)
		//{
		//	if(O == null)
		//	{ 
		//		spawnedEnemies.Remove(O);
		//		currentNumberOfEnemies--;
		//	}
		//}
	}
}
