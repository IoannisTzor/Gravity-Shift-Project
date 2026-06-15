using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public static UiManager Instance;
    public GameObject gameOverPanel;
    
    void Awake()
    {
        Instance = this;                  
    }
    void Start()
    {
     gameOverPanel.SetActive(false);   
    }
    
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: "+ Mathf.FloorToInt(GameManager.Instance.ScoreGet());
    }
    public void showGameOver()
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Final Score: "+ Mathf.FloorToInt(GameManager.Instance.ScoreGet());
    }
}
