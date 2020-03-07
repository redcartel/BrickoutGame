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

    public float maxPaddleSpeed = 100f;

    private float fixedPaddleY;
    Rigidbody2D paddleBody;

    Rigidbody2D ballBody;
    float ballMaxSpeed;
    void Start()
    {
        Vector3 bPosition = ball.transform.position;
        Vector3 pPosition = paddle.transform.position;
        ballPaddleDelta = new Vector3(0, bPosition.y - pPosition.y, 0);
        fixedPaddleY = (float)(int)pPosition.y;

        ballBody = ball.GetComponent<Rigidbody2D>();
        paddleBody = paddle.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        float mouseX = game.mouseX;
        float xSpeed = 100f;

        if (mouseX < paddle.width / 2) {
            mouseX = paddle.width / 2;
            xSpeed = 199f;
        }

        if (mouseX > game.expectedWidth - game.borderWidth - paddle.width / 2) {
            mouseX = game.expectedWidth - game.borderWidth - paddle.width / 2;
            xSpeed = 199f;
        }

        if (stuckToPaddle) {
            mouseX = game.expectedWidth / 2;
            xSpeed = 199f;
        }

        float paddleX = paddle.transform.position.x;
        float sign = Mathf.Sign(mouseX - paddleX);
        
        if (Mathf.Abs(mouseX - paddleX) < 4) {
            paddle.transform.position = new Vector2(mouseX, fixedPaddleY);
            paddleBody.velocity = new Vector3(0f,0f,0f);
        }
        else {
            paddleBody.velocity = new Vector3(sign * xSpeed, 0f, 0f);
        }
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

    public void ResetPaddle() {
        paddle.transform.position = new Vector3(game.expectedWidth / 2, fixedPaddleY, 0f);
    }

    public void HandleClick(float mouseX, float mouseY, Vector2 launchVector, float launchSpeed)
    {
        if (stuckToPaddle)
        {
            LaunchBall(mouseX, mouseY);
        }
    }

    public void LaunchBall(float eX, float eY)
    {
        Vector3 launchVector = (new Vector3(eX, eY, 0) - ball.transform.position).normalized;
        float launchSpeed = game.levelData.launchSpeed;
        ball.transform.position = paddle.transform.position + ballPaddleDelta;
        stuckToPaddle = false;
        ball.SetMinMaxSpeeds(launchSpeed);
        ball.GetComponentInChildren<Rigidbody2D>().velocity = launchVector * launchSpeed;
    }

    public void ResetBall()
    {
        ball.transform.position = paddle.transform.position + ballPaddleDelta;
        stuckToPaddle = true;
        ball.GetComponentInChildren<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
    }
}
