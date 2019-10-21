using System;
using UnityEngine;
using UnityEngine.UI;

public class EndPlatform :MonoBehaviour{
    public int expectPlayerIDToEnd = 0;
    public bool connected = false;
    public SpriteRenderer img;
    public Color greenColor;
    public Color redColor;
    public float minHeight, maxHeight;
    public Vector2 goOutDir=Vector2.zero;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (connected) return;
        if (expectPlayerIDToEnd == collision.gameObject.GetComponent<MergingLogic>().playerID)
        {
            connected = true;
            if(img)
                img.color = greenColor;
            transform.Translate(goOutDir);
        }
    }

    private void MoveDown()
    {
        transform.Translate(Vector2.down * 2f);
    }

    public void ResetLevelWhenSomeoneWins()
    {
        if (Time.time < 1)
        {
            return;
        }
        // move player goal who won, up, to lower difficulty for him.
        if (connected)
        {
            transform.Translate(Vector2.up * 2f);
            transform.Translate(-goOutDir);
        }
        else MoveDown();//additional mechanic, that ensures losing player has it easier.
        Vector2 tmp = transform.position;
        tmp.y =Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        transform.position = tmp;

        img.color = redColor;

        connected = false;
    }
}
