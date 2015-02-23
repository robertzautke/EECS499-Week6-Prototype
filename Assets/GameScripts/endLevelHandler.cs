using UnityEngine;
using System.Collections;

public class endLevelHandler : MonoBehaviour {

	public string LoadValue;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Application.LoadLevel(LoadValue);
        }
    }
}
