using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance { get {return _instance; } }

    private int[] _scores;
    public int[] Scores { get {
            _scores = new int[3];
            for (int i = 0; i < 3; i++)
            {
                _scores[i] = PlayerPrefs.GetInt("score" + i, -1);
            }
            return _scores;
        }
    }

    public int CurrentScore { get; set; }

    private void Awake()
    {
        if (_instance != null)
            Destroy(this);
        _instance = this;
    }

    private void Start()
    {
        CurrentScore = 0;
    }

    public void AddScore(int points)
    {
        CurrentScore += points;
    }

    public void SaveScore()
    {
        for(int i = 0; i < 3; i++)
        {
            if(CurrentScore > Scores[i])
            {
                Scores[i] = CurrentScore;
                PlayerPrefs.SetInt("score" + i, CurrentScore);
                break;
            }
        }
    }
}
