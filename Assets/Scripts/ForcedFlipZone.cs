using UnityEngine;

public class ForcedFlipZone : MonoBehaviour
{


void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Collision Detected");
		// only do stuff if collided with the Player
		if (other.gameObject.tag == "Player")
        {
            other.GetComponent<gravityShift>().FlipGravity();
	    }
    }
}
