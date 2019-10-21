using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    public Config config;
    public EndPlatform p1, p2;
    public GameObject uip1, uip2;
    public Image uiOverlay;
    public Text text;
    public Text scoreText;
    public Color fadeoutColor;
    bool done = false;

    public AllColors colors;

    private void Start()
    {
        config.p1Score = 0;
        config.p2Score = 0;
        Reload();
    }

    private void Update()
    {
        if (done)
        {
            return;
        }

        scoreText.text = config.p1Score + ":" + config.p2Score;

        if (p1.connected)
        {
            config.p2Score++;
            Win("Player 2 WON!");
        }
        else if (p2.connected)
        {
            config.p1Score++;
            Win("Player 1 WON!");
        }
    }

    private void Win(string v)
    {
        done = true;
        StartCoroutine(FadeOut(v));
    }

    IEnumerator FadeOut(string v)
    {
        text.text = "";
        Color c = uiOverlay.color;
        Color targetCol = fadeoutColor;
        int playerWon = config.currentPlayer;

        float t = 0;
        yield return new WaitForSeconds(1);
        while (t < 1)
        {
            uiOverlay.color = Color.Lerp(c, targetCol, t);
            t += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < v.Length; i++)
        {
            text.text += v[i];
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1.75f);

        Reload();

        SetUpForNextGame(playerWon);
    }

    void Reload()
    {
        // Resets entire configuration and clear level states.
        done = false;
        config.currentColor = 1;
        config.currentPlayer = 0;
        // -- Player score stays the same.

        // Clear ball objects.
        for (int i = 0; i < config.balls.Count; i++)
        {
            GameObject.Destroy(config.balls[i].gameObject);
        }
        Debug.Log("Destroyed "+config.balls.Count);
        config.balls.Clear();

        // gui
        uiOverlay.color = Color.clear;
        text.text = "";

        

    }

    private void SetUpForNextGame(int playerWon)
    {
        // platforms
        p1.ResetLevelWhenSomeoneWins();
        p2.ResetLevelWhenSomeoneWins();

        config.currentColor = UnityEngine.Random.Range(1, 3);

        config.currentPlayer = (playerWon + 1) % 2;

        // randomly pick first player and color
        uip1.gameObject.SetActive(config.currentPlayer == 0);
        uip2.gameObject.SetActive(config.currentPlayer == 1);
    }

    private void OnDestroy()
    {
        Reload();
    }
}
