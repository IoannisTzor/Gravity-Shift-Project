using System.Runtime.CompilerServices;
using UnityEngine;

public class PlatformCleanup : MonoBehaviour
{
     public float CamSize = 5f;

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x < Camera.main.transform.position.x - (2*CamSize+5))
        {
             Destroy(gameObject);
        }
    }
}
