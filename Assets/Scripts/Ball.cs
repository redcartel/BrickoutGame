using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Game game;
    Vector3 startDelta;
    Paddle paddle;
    public Rigidbody2D rigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<Game>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!game.gamePlaying)
        {
            transform.position = game.paddle.transform.position + game.paddleBallDelta;
            if (Input.GetMouseButtonDown(0) || game.autoPlay)
            {
                game.gamePlaying = true;
                rigidBody2D.velocity = game.launchVector;
            }
        }
    }
}
