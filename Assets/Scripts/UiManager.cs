using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: "+ Mathf.FloorToInt(GameManager.Instance.ScoreGet());
    }
}
