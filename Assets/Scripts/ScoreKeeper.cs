using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreKeeper : MonoBehaviour {
    [SerializeField] TextMeshProUGUI Score;
    [SerializeField] float WinScore;
    SceneLoader Scene;

    private void Start()
    {
         Scene = FindObjectOfType<SceneLoader>() ;
    }

    float score;
    public void KeepScore(float scores)
    {
        score += scores;
        Score.text = "Score: " + score;
        if(score>= WinScore)
        {
            Scene.loadWinScreen();
            Reset();
        }
        
    }

    public void Reset()
    {
        score = 0;
    }
}
