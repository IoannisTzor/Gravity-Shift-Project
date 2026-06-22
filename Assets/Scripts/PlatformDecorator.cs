using UnityEngine;
using System.Collections.Generic;

public class PlatformDecorator : MonoBehaviour
{
    public float PlatWidth = 2f;
    public float PlatHeight = 0.55f;
    private float spikeX =0f;
    private float spike2X =0f;
    private float coinX=0f;

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
        int roll3 = Random.Range(0, 100);

        
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
        if (roll1 >= 60)
            {
                spikeX = TakeRandomSlot(slots);
                Instantiate(HazardPrefab, new Vector2(spikeX ,(transform.position.y + (PlatHeight - 0.10f) * ySign) ),rotation);
            }
        if (roll3 >= 75)
            {
                spike2X = TakeRandomSlot(slots);
                Instantiate(HazardPrefab, new Vector2(spike2X ,(transform.position.y + (PlatHeight - 0.10f) * ySign) ),rotation);
            }
        if (roll2 >= 55)
            {
                coinX = TakeRandomSlot(slots);
                Instantiate(CoinPrefab, new Vector2(coinX ,transform.position.y + PlatHeight * ySign),rotation);  
            }
    }
    float TakeRandomSlot(List<float> slots)
    {
        int choice = Random.Range(0, slots.Count);
        float xPos = slots[choice];
        slots.RemoveAt(choice);
        return xPos;
    }    
}
