using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEvents : MonoBehaviour
{
    public List<GameObject> obstclesInLevel;

    public UnityEvent GameOverEvent;
    public UnityEvent RoundWonEvent;

    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject roundWonMenu;

    private int roundsToWin;
    private static int roundsWon = 0;

    private void Start()
    {
        roundsToWin = SceneManager.sceneCountInBuildSettings - 1;
        GameOverEvent.AddListener(GameOver);
        RoundWonEvent.AddListener(RoundWon);
    }

    public void RestartAndStartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadNextLevel()
    {
        switch (ScoreCounter.RoundsWon)
        {
            case 0:
                Debug.Log("LoadNextLevel Called on Main Menu?");
                break;
            case 1:
                SceneManager.LoadScene(2);
                break;
            case 2:
                SceneManager.LoadScene(3);
                break;
            case 3:
                SceneManager.LoadScene(1);
                break;

        }
    }

   public void QuitGame()
    {
        Application.Quit();
    }

    void RoundWon()
    {
        ScoreCounter.RoundsWon++;
        NextLevel();
    }

    void GameOver()
    {
        gameOverMenu.SetActive(true);
    }

    void NextLevel()
    {
        roundWonMenu.SetActive(true);
    }
}
