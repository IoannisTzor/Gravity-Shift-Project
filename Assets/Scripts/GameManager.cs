using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;   

    private bool isGameOver = false;     
    private float survivalTime = 0f;     

    private float coinValue = 1f; 
    private int Combo = 0;

    private float coinScore = 0f;
    public GameObject player;
    public GameObject explosion;

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
    public void addCoin()
    {
        coinScore +=  coinValue * (1 + Combo * 0.5f);
        Combo ++;
        Debug.Log("Combo = "+ Combo);
    }

    public void GameOver()               
    {
        if (isGameOver) return;
        float finalScore = ScoreGet();
        if (finalScore > PlayerPrefs.GetFloat("HighScore", 0f))
        {
            PlayerPrefs.SetFloat("HighScore", finalScore);
            PlayerPrefs.Save();
        }
        isGameOver = true;
        UiManager.Instance.showGameOver();
        Instantiate(explosion, player.transform.position, Quaternion.identity);
        Destroy(player);
        Debug.Log("Game Over! Survived: " + survivalTime + " seconds and collected "+ coinScore + " coins" );
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }
    public float ScoreGet()
    {
        return survivalTime + coinScore * 5;
    }
    public float CoinScore()
    {
        return coinScore;
    }
    public float ComboScore()
    {
        return (1 + Combo * 0.5f);
    }
    public int RawCombo()
    {
        return Combo;
    }
    public void ResetCombo()
    {
        Combo = 0;
        Debug.Log("Combo reset to" + Combo );
    }
    



}