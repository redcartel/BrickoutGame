using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float minAngle = 22.5f;

    Game game;
    public Rigidbody2D rigidBody2D;

    private float maCos;
    private float maSin;
    private Vector3 paddleBallDelta;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<Game>();
        rigidBody2D = GetComponent<Rigidbody2D>();

        minAngle = minAngle / 180 * Mathf.PI;
        maCos = Mathf.Cos(minAngle);
        maSin = Mathf.Sin(minAngle);

        paddleBallDelta = transform.position - game.paddle.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (!game.gamePlaying)
        {
            LaunchBall();
        }
        else if (game.waitToChangeBallVelocity == true)
        {
            // wait a frame after a collision to fix the ball velocity
            game.waitToChangeBallVelocity = false;
        }
        else 
        {
            FixBallVelocity();
        }
    }

    private void LaunchBall()
    {
        transform.position = game.paddle.transform.position + paddleBallDelta;
        if (Input.GetMouseButtonDown(0) || game.autoPlay)
        {
            game.gamePlaying = true;
            rigidBody2D.velocity = game.launchVector;
            game.playSound(game.paddle.soundNum);
        }
    }

    private void FixBallVelocity()
    {
        // If the ball is going at too steep or shallow of an angle (minAngle), fix that.
        float speed = rigidBody2D.velocity.magnitude;
        Vector2 direction = rigidBody2D.velocity.normalized;

        // some trig
        if (Mathf.Abs(direction.y) < maSin)
        {
            direction = new Vector2(Mathf.Sign(direction.x) * maCos, Mathf.Sign(direction.y) * maSin);
        }
        else if (Mathf.Abs(direction.x) < maSin)
        {
            direction = new Vector2(Mathf.Sign(direction.x) * maSin, Mathf.Sign(direction.y) * maSin);
        }

        rigidBody2D.velocity = speed * direction.normalized;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        game.waitToChangeBallVelocity = true;
    }
}
