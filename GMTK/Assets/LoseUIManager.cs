using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseUIManager : MonoBehaviour
{
    public GameObject loseMenuUI;
    public Button quitButton;
    public Button retryButton;
    private bool isLose = false;

    private void Start()
    {
        quitButton.onClick.AddListener(QuitGame);
        retryButton.onClick.AddListener(RetryLevel);
        LoseGame();
    }

    public void LoseGame()
    {
        isLose = false;
        loseMenuUI.SetActive(false);
    }

    public void AfterLoseGame()
    {
        isLose = true;
        Time.timeScale = 0f;
        loseMenuUI.SetActive(true);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
