using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundController : MonoBehaviour
{
    // Start is called before the first frame updat
    [SerializeField] public Ball ball;
    [SerializeField] public Paddle paddle;

    GameController game;

    public bool stuckToPaddle = true;
    private Vector3 ballPaddleDelta;

    private Block[] blocks;
    private float leftBound;
    private float rightBound;

    void Start()
    {
        game = (game is null) ? FindObjectOfType<GameController>() : game;
        ball = (ball is null) ? FindObjectOfType<Ball>() : ball;
        paddle = (paddle is null) ? FindObjectOfType<Paddle>() : paddle;
        ballPaddleDelta = ball.transform.position - paddle.transform.position;
        leftBound = game.borderWidth + paddle.width / 2;
        rightBound = game.expectedWidth - game.borderWidth - paddle.width / 2;
    }

    public void HandleMouseMove(float mouseX, float mouseY)
    {
        Vector3 position = paddle.transform.position;
        float newX = Mathf.Clamp(mouseX, leftBound, rightBound);
        paddle.transform.position = new Vector3(newX, position.y, position.z);

        if (stuckToPaddle)
        {
            Vector3 ballPosition = paddle.transform.position + ballPaddleDelta;
            ball.transform.position = ballPosition;
        }
    }

    public void HandleClick(float mouseX, float mouseY)
    {
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
