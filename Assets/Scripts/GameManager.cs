using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;   

    private bool isGameOver = false;     
    private float survivalTime = 0f;     

    private float coinValue = 1f; 

    private float coinScore = 0f;

    void Awake()
    {
        Instance = this;                  
    }

    void Update()
    {
        if (!isGameOver)                 
        {
            survivalTime += Time.deltaTime;
        }
    }

    public void GameOver()               
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("Game Over! Survived: " + survivalTime + " seconds and collected "+ coinScore + " coins" );
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }
    
    public float CoinScore()
    {
        return coinScore;
    }
    public void addCoin()
    {
        coinScore +=  coinValue;
    }

}