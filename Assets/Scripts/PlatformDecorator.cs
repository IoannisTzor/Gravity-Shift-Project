using UnityEngine;
using System.Collections.Generic;

public class PlatformDecorator : MonoBehaviour
{
    public float PlatWidth = 2f;
    public float PlatHeight = 0.55f;
    private float spikeX =0f;
    private float coinX=0f;
    public float minGap = 1f;
    private float retries = 0;

    public GameObject HazardPrefab;
    public GameObject CoinPrefab;
    public int slotCount = 4;
    public float slotWidth =0;
    public float leftEdge = 0f;

    private float ySign = 0;
    private Quaternion rotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slotWidth = (PlatWidth * 2) / slotCount;
        List<float> slots = new List<float>();
        leftEdge = transform.position.x - PlatWidth;
        for (int i = 0; i < slotCount; i++)
        {
            slots.Add(leftEdge + (i + 0.5f) * slotWidth);
        }
        int roll1 = Random.Range(0, 100);
        int roll2 = Random.Range(0, 100);
        spikeX = Random.Range(transform.position.x - PlatWidth, transform.position.x + PlatWidth);
        coinX = Random.Range(transform.position.x - PlatWidth, transform.position.x + PlatWidth);
        
        if (transform.position.y <= 0)
            {
                ySign = 1f;
                rotation = Quaternion.identity;
            }
        else
            {
                ySign = -1f;
                rotation = Quaternion.Euler(0, 0, 180);
            }
        if (roll1 >= 70)
            {
                Instantiate(HazardPrefab, new Vector2(spikeX ,(transform.position.y + (PlatHeight - 0.10f) * ySign) ),rotation);
                while (Mathf.Abs(coinX - spikeX)< minGap && retries < 100)
                    {
                    coinX = Random.Range(transform.position.x - PlatWidth, transform.position.x + PlatWidth);
                    retries +=1;
                    }
            }
        if (roll2 >= 55)
            {
                Instantiate(CoinPrefab, new Vector2(coinX ,transform.position.y + PlatHeight * ySign),rotation);  
            }
    }
}
