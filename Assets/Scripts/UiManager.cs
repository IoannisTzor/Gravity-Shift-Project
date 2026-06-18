using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI timerText;
    public static UiManager Instance;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject timerPanel;
    private bool isPaused;
    



    void Awake()
    {
        Instance = this;                  
    }
    void Start()
    {
     gameOverPanel.SetActive(false);
     pausePanel.SetActive(false); 
     timerPanel.SetActive(false); 
       
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        //add a timer
        pausePanel.SetActive(false);
        StartCoroutine(ResumeCountdown());
    }

    IEnumerator ResumeCountdown()
{
    // show countdownText
    timerPanel.SetActive(true);
    timerText.text = "Starting in: 3";
    yield return new WaitForSecondsRealtime(1f);
    timerText.text = "Starting in: 2";
    yield return new WaitForSecondsRealtime(1f);
    timerText.text = "Starting in: 1";
    yield return new WaitForSecondsRealtime(1f);
    timerPanel.SetActive(false);
    Time.timeScale = 1f;
    isPaused = false;

}

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }
    public void TogglePause()
    {
        if (isPaused)
            {
                ResumeGame();
            }
            else if (GameManager.Instance.IsGameOver() == false)
            {
                PauseGame();
            }
    }
    
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: "+ Mathf.FloorToInt(GameManager.Instance.ScoreGet());
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void showGameOver()
    {

        gameOverPanel.SetActive(true);
        Time.timeScale = 1f;
        highScoreText.text = "High Score: "+ Mathf.FloorToInt(PlayerPrefs.GetFloat("HighScore", 0f));
        finalScoreText.text = "Final Score: "+ Mathf.FloorToInt(GameManager.Instance.ScoreGet());
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
