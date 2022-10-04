using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class ScoreCounter
{
    static int score = 0;

    public static void GetOverallScore()
    {
        Debug.Log(score);
    }

    public static int GetRandomScore()
    {
        return Random.Range(350, 450);
    }

    public static void AddScore(int scoreValue)
    {
        score += scoreValue;
    }
}
