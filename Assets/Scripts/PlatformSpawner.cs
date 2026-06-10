using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    private float lastX;
    public float minY = -5f;
    public float maxY = 5f;
    public float buffer = 3f;
    public float spawnGap = 5f;

    private float fovBorder;
    public GameObject platformPrefab;
    public Transform lastStartingPlatform;


    void Start()
    {
        fovBorder  = Camera.main.orthographicSize * Camera.main.aspect;
        lastX = lastStartingPlatform.transform.position.x;
    }
    // Update is called once per frame
    void Update()
    {
         if (Camera.main.transform.position.x+ fovBorder >= lastX + spawnGap)
        {
            Instantiate(platformPrefab, new Vector2(Camera.main.transform.position.x + fovBorder + buffer ,Random.Range(minY, maxY)),Quaternion.identity);
            lastX = Camera.main.transform.position.x + fovBorder+buffer;
        }
    }
}
