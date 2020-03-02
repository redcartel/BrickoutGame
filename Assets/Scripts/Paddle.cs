using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    /* TODO: derive these automatically */
    [SerializeField] float pixelsPerUnit = 32;
    [SerializeField] float halfPaddleWidth = .75f / 2;
    [SerializeField] float stageWidth = 4f;
    [SerializeField] float borderWidth = .25f;

    Game game;

    void Start()
    {
        game = FindObjectOfType<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), 0f + borderWidth + halfPaddleWidth, 4f - borderWidth - halfPaddleWidth);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (game.frozen)
        {
            return transform.position.x;
        }
        else if (!game.autoPlay)
        {
            return Input.mousePosition.x / Screen.width * stageWidth; // changeme
        }
        else
        {
            return game.ball.transform.position.x;
        }
    }
}
