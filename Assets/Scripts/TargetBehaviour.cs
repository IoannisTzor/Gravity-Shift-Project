using UnityEngine;

public class TargetBehavior : MonoBehaviour
{

    // when collided with another gameObject
    void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("Collision Detected");
		if (other.gameObject.tag == "Player")
        {
            //add score
            GameManager.Instance.addCoin();
            
            // destroy self
            Destroy(gameObject);

        }    
	}

}
