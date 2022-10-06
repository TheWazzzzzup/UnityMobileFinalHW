using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class ScoreCounter
{
    static int score = 0;

    static int rngLowScore = 350;
    static int rngHighScore = 451;

    public static void GetOverallScore()
    {
        Debug.Log(score);
    }

    public static int GetRandomScore()
    {
        return Random.Range(rngLowScore, rngHighScore);
    }

    public static void AddScore(int scoreValue)
    {
        score += scoreValue;
    }
}
