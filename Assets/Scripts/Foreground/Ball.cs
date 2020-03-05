using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float minAngle = 22.5f;

    GameController game;
    public Rigidbody2D rigidBody2D;

    private float maCos;
    private float maSin;
    private Vector3 paddleBallDelta;
    private bool waitToChangeBallVelocity = false;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameController>();
        rigidBody2D = GetComponent<Rigidbody2D>();

        maCos = Mathf.Cos(minAngle / 180.0f * Mathf.PI);
        maSin = Mathf.Sin(minAngle / 180.0f * Mathf.PI);

    }

    // Update is called once per frame
    void Update()
    {
        if (!waitToChangeBallVelocity)
        {
            FixBallVelocity();
        }
        waitToChangeBallVelocity = false;
    }

    // Don't let the ball travel at too low or too great of a slope.
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

    // This fixes a weird bug. Just wait a frame before doing a velocity fix after a collision. Seems to work.
    void OnCollisionEnter2D(Collision2D collision)
    {
        game.waitToChangeBallVelocity = true;
    }
}
