using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameController game;
    [SerializeField] public Ball ball;
    [SerializeField] public Paddle paddle;

    [SerializeField] public float launchSpeed = 55f;

    public bool frozen = false;
    // TODO: do pause the real way
    public Vector3 savedBallVelocity = new Vector3(0f,0f,0f);
    public bool stuckToPaddle = true;
    private Vector3 ballPaddleDelta;

    [SerializeField] public float paddleSpeed = 300f;

    private float fixedPaddleY;

    float ballMaxSpeed;
    void Start()
    {
        Vector3 bPosition = ball.transform.position;
        Vector3 pPosition = paddle.transform.position;
        ballPaddleDelta = new Vector3(0, bPosition.y - pPosition.y, 0);
        fixedPaddleY = pPosition.y;
    }

    void FixedUpdate() {
        if (game.demoMode && stuckToPaddle) {
            float yVec = Random.Range(fixedPaddleY, game.expectedHeight);
            float xVec = Random.Range(-1 * game.expectedWidth / 2, game.expectedWidth / 2);
            LaunchBall(xVec, yVec);
            return;
        }

        // virtual mouse position, cannot go left or right of paddle bounds
        float mouseX = game.mouseX; 
        if (stuckToPaddle) mouseX = game.expectedWidth / 2;
        if (game.demoMode) mouseX = ball.transform.position.x;

        float xSpeed = paddleSpeed;

        if (mouseX < paddle.leftPaddleBound) {
            mouseX = paddle.leftPaddleBound;
            xSpeed = paddleSpeed * 2;
        }

        if (mouseX > paddle.rightPaddleBound) {
            mouseX = paddle.rightPaddleBound;
            xSpeed = paddleSpeed * 2;
        }

        float paddleX = paddle.transform.position.x;
        float sign = Mathf.Sign(mouseX - paddleX);
        
        if (Mathf.Abs(mouseX - paddleX) < paddle.width / 8) {
            paddle.transform.position = new Vector2(mouseX, fixedPaddleY);
            paddle.rigidBody2D.velocity = new Vector3(0f,0f,0f);
        }
        else {
            paddle.rigidBody2D.velocity = new Vector3(sign * xSpeed, 0f, 0f);
        }
    }

    private float leftPaddleBound { get { return game.borderWidth + paddle.width * .5f; } }
    private float rightPaddleBound { get { return game.expectedWidth - game.borderWidth - paddle.width * .5f; } }

    // TODO: replace this
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

    public void HandleClick(float mouseX, float mouseY)
    {
        if (stuckToPaddle)
        {
            LaunchBall(mouseX, mouseY);
        }
    }

    public void LaunchBall(float eX, float eY)
    {
        Vector3 launchVector = (new Vector3(eX, eY, 0) - ball.transform.position).normalized;
        ball.transform.position = paddle.transform.position + ballPaddleDelta;
        stuckToPaddle = false;
        ball.minSpeed = launchSpeed;
        ball.maxSpeed = launchSpeed * ball.maxSpeedMultiplier;
        ball.rigidBody2D.velocity = launchVector * launchSpeed;
    }

    public void ResetBall()
    {
        ball.transform.position = paddle.transform.position + ballPaddleDelta;
        stuckToPaddle = true;
        ball.GetComponentInChildren<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
    }
}
