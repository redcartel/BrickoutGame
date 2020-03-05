using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float width = 32f;

    /* TODO: derive these automatically */
    /*
    [SerializeField] float paddleWidth = 32f;
    [SerializeField] AudioSource collisionSound;
    [SerializeField] public int soundNum = 4;

    private Game game;

    void Start()
    {
        game = FindObjectOfType<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), 
            paddleWidth / 2.0f + game.borderWidth, 
            game.stageWidth - paddleWidth / 2.0f - game.borderWidth);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (game.frozen)
        {
            return transform.position.x;
        }
        else if (!game.levelData)
        {
            return Input.mousePosition.x / Screen.width * game.stageWidth; // changeme
        }
        else
        {
            return game.ball.transform.position.x;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            game.waitToChangeBallVelocity = true;
            game.playSound(soundNum);
        }
    }
    */
}
