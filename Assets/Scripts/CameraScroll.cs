using System.Diagnostics.Contracts;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float scrollSpeed = 3f;  
    public float baseSpeed = 3f; 
    public float acceleration = 0.05f;

    void Update()
    {
        if (GameManager.Instance.IsGameOver()) return;  
        
        scrollSpeed = baseSpeed + GameManager.Instance.ScoreGet() * acceleration;

        transform.position += Vector3.right * scrollSpeed * Time.deltaTime;   
    }
}
