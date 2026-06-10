using System.Runtime.CompilerServices;
using UnityEngine;

public class PlatformCleanup : MonoBehaviour
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
             Destroy(gameObject);
        }
    }
}
