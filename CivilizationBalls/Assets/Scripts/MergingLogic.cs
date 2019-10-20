using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergingLogic : MonoBehaviour, ICreate, IEndOfTurnListener {

    public int playerID;
    SingleBall ball;
    public int ballColor;

    public bool isMaxed { get => ballColorsRef.maxColor == ballColor; }
    
    public AllColors ballColorsRef;

    [SerializeField] Config config;

    bool lockedCollision = true;

    private void Start()
    {
        ball = GetComponent<SingleBall>();
        Register();
    }

    public void OnCreated(int currentColor, int currentPlayer)
    {
        lockedCollision = false;
        this.ballColor = currentColor;
        this.playerID = currentPlayer;
        transform.GetComponent<SpriteRenderer>().color = ballColorsRef.GetColor(ballColor);
    }

    private void OnDestroy()
    {
        UnRegister();
    }

    public void Register()
    {
        config.balls.Add(this);
    }

    public void UnRegister()
    {
        config.balls.Remove(this);
    }

    public void EndOfTurn()
    {
        Grow(config.sc.scalingMultOnEndTurn, config.sc.scalingMode);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (lockedCollision) return;

        MergingLogic otherBall = collision.gameObject.GetComponent<MergingLogic>();
        if (!otherBall) return;
        if (playerID == otherBall.playerID)
        {
            AttemptToMerge(otherBall);
        }
        else
        {
            AttemptToDestroy(otherBall);
        }
    }

    private void AttemptToMerge(MergingLogic otherBall)
    {
        BallColor settingsThis = ballColorsRef.GetBallColorSettings(ballColor);
        BallColor settingsOther = ballColorsRef.GetBallColorSettings(otherBall.ballColor);
        if (settingsThis.CanMerge(otherBall.ballColor))
        {
            ballColor = settingsThis.GetMergedColorId(otherBall.ballColor);
            transform.GetComponent<SpriteRenderer>().color = ballColorsRef.GetColor(ballColor);
            Vector2 maxScale = otherBall.transform.localScale;
            if (transform.localScale.y > maxScale.y)
            {
                maxScale.y = transform.localScale.y;
                maxScale.x = transform.localScale.x;
            }
            transform.localScale = maxScale;

            Grow(config.sc.scalingMultOnMerge, config.sc.scalingMode);

            Destroy(otherBall.gameObject);
        }
    }

    void Grow(float scaleMult, int mode)
    {
        if (mode == 0)
        {
            transform.localScale = new Vector3(
                transform.localScale.x * scaleMult,
                transform.localScale.y * scaleMult,
                transform.localScale.z);
        }
        else if (mode == 1)
        {
            transform.localScale = new Vector3(
                transform.localScale.x + scaleMult,
                transform.localScale.y + scaleMult,
                transform.localScale.z);
        }
    }

    private void AttemptToDestroy(MergingLogic otherBall)
    {
        BallColor settingsThis = ballColorsRef.GetBallColorSettings(ballColor);
        BallColor settingsOther = ballColorsRef.GetBallColorSettings(otherBall.ballColor);
        if (settingsThis.CanDestroy(otherBall.ballColor))
        {
            Destroy(otherBall.gameObject);
            Grow(config.sc.scalingMultOnDestroy, config.sc.scalingMode);
        }
    }

}
