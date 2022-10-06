using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform playerSpawnPos;

    [SerializeField] GameEvents gameEvents;

    [SerializeField] List<GameObject> listOfPlayers;
    [SerializeField] List<GameObject> thisRoundCharactes;

    private GameObject currentPlayer;

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
        else { RoundCheck(); }
    }

    void RoundCheck()
    {
        // if statement that checks if the player destroied all obstacles by min score: current level (obstcale = 5, min score = 200) minimum req is 1000 points to clear to next level
        // then invoke either gameover or congrats and move to next level!
        gameEvents.GameOverEvent.Invoke();
    }

    [ContextMenu("GetScore")]
    void GetScore()
    {
        ScoreCounter.GetOverallScore();
    }

}
