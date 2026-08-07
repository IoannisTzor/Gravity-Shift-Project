using UnityEngine;

public class TargetBehavior : MonoBehaviour
{

    public float CamSize;

    // Update is called once per frame
    void Start()
    {
    CamSize = Camera.main.orthographicSize * Camera.main.aspect;
    }
    void Update()
    {

        if (transform.position.x < Camera.main.transform.position.x - CamSize-3)
        {
            GameManager.Instance.ResetCombo();
            Destroy(gameObject);
        }
    } 
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
