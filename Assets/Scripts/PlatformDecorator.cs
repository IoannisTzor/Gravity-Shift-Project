using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class PlatformDecorator : MonoBehaviour
{
    public float PlatWidth = 2f;
    public float PlatHeight = 0.55f;
    private float coinX=0f;

    public GameObject HazardPrefab;
    public GameObject CoinPrefab;
    public int slotCount = 6;
    public float slotWidth =0;
    public float leftEdge = 0f;
    public float maxOffset = 40;
    public float rampFactor = 0.3;

    private float ySign = 0;
    private float yFSign = 0;
    private Quaternion rotation;
    private Quaternion fRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slotWidth = (PlatWidth * 2) / slotCount;
        List<float> slots = new List<float>();
        leftEdge = transform.position.x - PlatWidth;
        float score = GameManager.Instance.ScoreGet();
        float offset = Mathf.Min(maxOffset, score * rampFactor);   // capped so it can't run away
        for (int i = 0; i < slotCount; i++)
        {
            slots.Add(leftEdge + (i + 0.5f) * slotWidth);
        }
        int roll1 = Random.Range(0, 100);
        int roll2 = Random.Range(0, 100);
        int roll3 = Random.Range(0, 100);
        int roll4 = Random.Range(0, 100);
        int spikeNum = 0;

        
        if (transform.position.y <= 0)
            {
                ySign = 1f;
                yFSign = -1f;
                rotation = Quaternion.identity;
                fRotation = Quaternion.Euler(0, 0, 180);
            }
        else
            {
                ySign = -1f;
                yFSign = 1f;
                rotation = Quaternion.Euler(0, 0, 180);
                fRotation = Quaternion.identity;
            }
        if (roll1 >= 60 - offset)
            {
                SpawnSpike(slots,ySign,rotation);
                spikeNum++;
            }
        if (roll3 >= 75 - offset)
            {
                SpawnSpike(slots,ySign,rotation);
                spikeNum++;
            }
        if (roll2 >= 55 - offset)
            {
                coinX = TakeRandomSlot(slots);
                Instantiate(CoinPrefab, new Vector2(coinX ,transform.position.y + PlatHeight * ySign),rotation);  
            }
        if (roll4 >= 80)
        {
            for (int i = 0; i < spikeNum; i++)
            {
                SpawnSpike(slots,yFSign,fRotation);
            }
        }
    }
    float TakeRandomSlot(List<float> slots)
    {
        int choice = Random.Range(0, slots.Count);
        float xPos = slots[choice];
        slots.RemoveAt(choice);
        return xPos;
    }    
    void SpawnSpike(List<float> slots, float ySign, Quaternion rotation)
    {
        float x = TakeRandomSlot(slots);
        Instantiate(HazardPrefab, new Vector2(x ,transform.position.y + (PlatHeight - 0.10f) * ySign ),rotation);
    }
}
