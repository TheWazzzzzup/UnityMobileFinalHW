using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class ScoreCounter
{
    static int score = 0;

    public static int RoundsWon = 0;
    public static int rngLowScore = 350;
    public static int rngHighScore = 451;

    public static void ResetCounter()
    {
        score = 0;
    }

    public static int GetOverallScore()
    {
        return score;
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
