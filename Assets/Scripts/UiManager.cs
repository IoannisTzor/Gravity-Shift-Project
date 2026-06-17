using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;
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
        highScoreText.text = "High Score: "+ Mathf.FloorToInt(PlayerPrefs.GetFloat("HighScore", 0f));
        finalScoreText.text = "Final Score: "+ Mathf.FloorToInt(GameManager.Instance.ScoreGet());
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
