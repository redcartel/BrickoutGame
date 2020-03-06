using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundController : MonoBehaviour
{
    // Start is called before the first frame updat
    [SerializeField] public Ball ball;
    [SerializeField] public Paddle paddle;

    public GameController game;

    public bool frozen = false;
    public Vector3 savedBallVelocity = new Vector3(0f,0f,0f);
    public bool stuckToPaddle = true;
    private Vector3 ballPaddleDelta;
    private Block[] blocks;

    void Start()
    {
        ball = (ball is null) ? GetComponentInChildren<Ball>() : ball;
        paddle = (paddle is null) ? GetComponentInChildren<Paddle>() : paddle;
        ballPaddleDelta = ball.transform.position - paddle.transform.position;
    }

    private float leftPaddleBound { get { return game.borderWidth + paddle.width * .5f; } }
    private float rightPaddleBound { get { return game.expectedWidth - game.borderWidth - paddle.width * .5f; } }

    public void Freeze() {
        frozen = true;
        savedBallVelocity = ball.rigidBody2D.velocity;
        ball.rigidBody2D.velocity = new Vector3(0f,0f,0f);
    }
    
    public void Unfreeze() {
        frozen = false;
        ball.rigidBody2D.velocity = savedBallVelocity;
    }

    public void HandleMouseMove(float mouseX, float mouseY)
    {
        if (!frozen) {
            Vector3 position = paddle.transform.position;
            float newX = Mathf.Clamp(mouseX, leftPaddleBound, rightPaddleBound);
            paddle.transform.position = new Vector3(newX, position.y, position.z);

            if (stuckToPaddle)
            {
                Vector3 ballPosition = paddle.transform.position + ballPaddleDelta;
                ball.transform.position = ballPosition;
            }
        }
    }

    public void HandleClick(float mouseX, float mouseY, Vector2 launchVector, float launchSpeed)
    {
        if (stuckToPaddle)
        {
            LaunchBall(launchVector, launchSpeed);
        }
    }

    public void LaunchBall(Vector2 launchVector, float launchSpeed)
    {
        ball.transform.position = paddle.transform.position + ballPaddleDelta;
        stuckToPaddle = false;
        ball.GetComponentInChildren<Rigidbody2D>().velocity = launchVector * launchSpeed;
    }

    public void ResetBall()
    {
        ball.transform.position = paddle.transform.position + ballPaddleDelta;
        stuckToPaddle = true;
        ball.GetComponentInChildren<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
    }

    public void MinBallSpeed(float newMinBallSpeed)
    {
        Rigidbody2D rgb2d = ball.GetComponentInChildren<Rigidbody2D>();
        if (rgb2d.velocity.magnitude < newMinBallSpeed) {
            rgb2d.velocity = rgb2d.velocity.normalized * newMinBallSpeed;
        }
    }
}
