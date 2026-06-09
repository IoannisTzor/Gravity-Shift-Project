using UnityEngine;
using System.Collections;

public class TargetBehavior : MonoBehaviour
{
    public int scoreAmount = 0;


    // when collided with another gameObject
    void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("Collision Detected");
		// only do stuff if collided with the Player
		
            // destroy self
            Destroy(gameObject);
	}

}
