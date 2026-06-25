using UnityEngine;

public class ForcedFlipZoneSpawner : MonoBehaviour
{
private float lastX;
    public float buffer = 3f;
    public float spawnGap = 50f;

    public float baseChance = 90f;
    public float maxOffset = 40f;
    public float rampFactor = 0.3f;
    private float fovBorder;
    public GameObject FlipZonePrefab;
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
            float score = GameManager.Instance.ScoreGet();
            float offset = Mathf.Min(maxOffset, score * rampFactor);
            int roll = Random.Range(0, 100);
            if (roll >= baseChance - offset)
            { 
                Instantiate(FlipZonePrefab, new Vector2(Camera.main.transform.position.x + fovBorder + buffer ,Camera.main.transform.position.y),Quaternion.identity);
            }
            lastX = Camera.main.transform.position.x + fovBorder+buffer;
        }
    }
}
