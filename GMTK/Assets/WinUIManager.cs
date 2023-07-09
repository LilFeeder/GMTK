using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUIManager : MonoBehaviour
{
    public GameObject winMenuUI;
    public Button quitButton;
    public Button retryButton;

    private void Start()
    {
        quitButton.onClick.AddListener(QuitGame);
        retryButton.onClick.AddListener(RetryLevel);
        WinGame();
    }

    public void WinGame()
    {
        winMenuUI.SetActive(false);
    }

    public void AfterWinGame()
    {
        winMenuUI.SetActive(true);
        Time.timeScale = 0f;
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
