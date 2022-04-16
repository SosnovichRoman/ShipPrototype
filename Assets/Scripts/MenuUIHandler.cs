using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public static MenuUIHandler Instance;

    [SerializeField]
    private GameObject startScreen;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private Text gemScoreText;

    [SerializeField]
    private AudioSource clickSound;

    private int gemScore;

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code
        Instance = this;
        Instance.gemScore = 0;
    }

    public void StartGame()
    {
        GameManager.Instance.isGameActive = true;
        startScreen.SetActive(false);
        clickSound.Play();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameplayScene");
        gameOverScreen.SetActive(false);
        clickSound.Play();
    }

    public void ShowRestartScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void UpdateGemScore()
    {
        Instance.gemScore++;
        gemScoreText.text = "Gems: " + Instance.gemScore;
    }

}
