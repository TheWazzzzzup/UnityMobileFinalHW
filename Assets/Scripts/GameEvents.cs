using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class GameEvents : MonoBehaviour
{
    public UnityEvent GameOverEvent;

    [SerializeField] GameObject gameOverMenu;

    private void Start()
    {
        GameOverEvent.AddListener(GameOver);
    }

    void GameOver()
    {
        gameOverMenu.SetActive(true);
    }
}
