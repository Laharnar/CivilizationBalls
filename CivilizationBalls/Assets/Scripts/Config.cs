using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Config :ScriptableObject{
    internal List<MergingLogic> balls = new List<MergingLogic>();
    public int currentColor;
    public int currentPlayer;

    [SerializeField] Transform p1BallPref, p2BallPref;

    public ScalingConfig sc;
    internal int p1Score;
    internal int p2Score;

    public Transform BallPrefab { get => currentPlayer == 0 ? p1BallPref : p2BallPref; }

    public void EndTurn()
    {
        currentPlayer = (currentPlayer + 1) % 2;
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].EndOfTurn();
        }
    }

    public void SetColor(int col)
    {
        this.currentColor = col;
    }

    internal void UpdateUI(GameObject uiP1, GameObject uiP2)
    {
        if (currentPlayer == 0)
        {
            uiP1.gameObject.SetActive(true);
            uiP2.gameObject.SetActive(false);
        }
        else
        {
            uiP1.gameObject.SetActive(false);
            uiP2.gameObject.SetActive(true);
        }
    }
}