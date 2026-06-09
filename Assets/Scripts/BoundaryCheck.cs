using System.Numerics;
using System.Security;
using UnityEngine;

public class BoundaryCheck : MonoBehaviour
{
    public GameObject Player;
    public float CamSize = 5f;


    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x < transform.position.x - (2*CamSize+5))
        {
            GameManager.Instance.GameOver();
            return;
        }
        if (Player.transform.position.y < transform.position.y - (CamSize+5) || Player.transform.position.y > transform.position.y + (CamSize+5))
        {
            GameManager.Instance.GameOver();
            return;
        }
    }
}
