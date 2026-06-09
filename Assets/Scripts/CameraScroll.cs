using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float scrollSpeed = 3f;   // (1)

    void Update()
    {
        if (GameManager.Instance.IsGameOver()) return;   // (2)

        transform.position += Vector3.right * scrollSpeed * Time.deltaTime;   // (3)
    }
}
