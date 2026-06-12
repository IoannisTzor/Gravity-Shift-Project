using UnityEngine;
using System.Collections;

public class HazardBehavior : MonoBehaviour
{


    // when collided with another gameObject
    void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("Collision Detected");
		// only do stuff if collided with the Player
		if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.GameOver();
        
            // destroy player
            
            Destroy(other.gameObject);
            //add explosion
	    }
    }

}

