using UnityEngine;

public class BoundaryCheck : MonoBehaviour
{
    public GameObject player;
    public float buffer = 3f;          // grace distance beyond the screen edge

    private float camHalfWidth;
    private float camHalfHeight;

    void Start()
    {
        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth  = camHalfHeight * Camera.main.aspect;
    }

    void Update()
    {
        Vector2 playerPos = player.transform.position;

        if (playerPos.x < transform.position.x - (camHalfWidth + buffer))
        {
            GameManager.Instance.GameOver();
            return;
        }

        if (playerPos.y < transform.position.y - (camHalfHeight + buffer) ||
            playerPos.y > transform.position.y + (camHalfHeight + buffer))
        {
            GameManager.Instance.GameOver();
        }
    }
}
