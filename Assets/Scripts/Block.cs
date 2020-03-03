using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] int points = 10;
    [SerializeField] int speedClass = 0;
    [SerializeField] bool breakable = true;
    [SerializeField] public int soundNum = 0;

    Game game;

    void Start()
    {
        game = FindObjectOfType<Game>();
        if (breakable)
        {
            game.AddBlock();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetBallSpeed()
    {
        // If the ball is going slower than this brick's speed class when it collides, speed it up
        Rigidbody2D ballRigidBody = game.ball.rigidBody2D;
        float targetSpeed = game.speeds[speedClass];
        if (ballRigidBody.velocity.magnitude < targetSpeed)
        {
            ballRigidBody.velocity = ballRigidBody.velocity.normalized * targetSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (breakable)
        {
            game.IncreaseScore(points);
            game.RemoveBlock();
            Destroy(gameObject);
            SetBallSpeed();
        }
        game.playSound(soundNum);
    }
}
