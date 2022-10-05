using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform playerSpawnPos;

    [SerializeField] GameEvents gameEvents;

    [SerializeField] GameObject currentPlayer;
    [SerializeField] List<GameObject> listOfPlayers;
    [SerializeField] List<GameObject> thisRoundCharactes;

    private AngryBirdsScript angrybirdscripts;

    private int lifeIndex = 0;
    private int roundAttempts = 3;

    private bool currentPlayerLaunched;
    private bool gameOver;

    private void Start()
    {
        PickPlayers(roundAttempts);
        currentPlayerLaunched = false;
        gameOver = false;

        if (thisRoundCharactes.Count > 0)
        {
            UpdateCurrentPlayer(lifeIndex);
        }

        currentPlayer.transform.position = playerSpawnPos.position;
    }

    public bool CurrentPlayerLaunched()
    {
        return currentPlayerLaunched;
    }

    public IEnumerator Lunched()
    {
        currentPlayerLaunched = true;
        yield return new WaitForSeconds(4);
        currentPlayerLaunched = false;
        UpdateCurrentPlayer(currentPlayer);
    }

    void PickPlayers(int gameLives)
    {
        for (int i = 0; i < gameLives; i++)
        {
            thisRoundCharactes.Add(RandomPlayerPick());
        }
    }

    GameObject RandomPlayerPick()
    {
        int x = Random.Range(0, listOfPlayers.Count);
        return listOfPlayers[x];
    }

    void UpdateCurrentPlayer(int playerIndex)
    {
        this.currentPlayer = Instantiate(thisRoundCharactes[playerIndex], playerSpawnPos.transform);
        angrybirdscripts = currentPlayer.GetComponent<AngryBirdsScript>();
        angrybirdscripts.InitGameManager(this);
    }

    void UpdateCurrentPlayer(GameObject currentPlayer)
    {
        currentPlayer.SetActive(false);
        lifeIndex++;
        if (!angrybirdscripts.PlayerLifeCycle && lifeIndex < roundAttempts)
        {
            Debug.Log(lifeIndex);
            UpdateCurrentPlayer(lifeIndex);
            this.currentPlayer.transform.position = playerSpawnPos.position;
            this.currentPlayer.SetActive(true);
        }
        else { gameOver = true; gameEvents.GameOverEvent.Invoke(); }
    }

    [ContextMenu("GetScore")]
    void GetScore()
    {
        ScoreCounter.GetOverallScore();
    }

}
