using System;
using UnityEngine;
using UnityEngine.UI;

public class EndPlatform :MonoBehaviour{
    public int expectPlayerIDToEnd = 0;
    public bool connected = false;
    public SpriteRenderer img;
    public Color greenColor;
    public Color redColor;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (connected) return;
        if (expectPlayerIDToEnd == collision.gameObject.GetComponent<MergingLogic>().playerID)
        {
            connected = true;
            if(img)
            img.color = greenColor;
        }
    }

    private void MoveDown()
    {
        transform.Translate(Vector2.down * 2f);
    }

    public void ResetLevel()
    {
        if (Time.time < 1)
        {
            return;
        }
        if (connected)
            transform.Translate(Vector2.up * 2f);
        //else MoveDown();//additional mechanic, that ensures losing player has it easier.
            if(img)
        img.color = redColor;

        connected = false;
    }
}
