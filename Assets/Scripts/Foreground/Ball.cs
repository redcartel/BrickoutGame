using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] ForegroundController foreground;
    [SerializeField] float minAngle = 18f;
    public Rigidbody2D rigidBody2D;

    private float maCos;
    private float maSin;
    private bool waitToChangeBallVelocity = false;
    //private bool changeBallVelocityNextCycle = false;
    private Vector2 lastVelocity = new Vector3(0f, 0f, 0);
    private float maxSpeed;
    private float minSpeed;
    public float maxSpeedMultiplier = 1.5f;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        maCos = Mathf.Cos(minAngle / 180.0f * Mathf.PI);
        maSin = Mathf.Sin(minAngle / 180.0f * Mathf.PI);
        minSpeed = foreground.game.levelData.launchSpeed;
        maxSpeed = foreground.game.levelData.launchSpeed * 1.5f;
    }

    public void SetMinMaxSpeeds(float minSpeed)
    {
        if (this.minSpeed < minSpeed) this.minSpeed = minSpeed;
        this.maxSpeed = minSpeed * maxSpeedMultiplier;
        FixBallVelocity();
    }

    // Don't let the ball travel at too low or too great of a slope.
    public void FixBallVelocity()
    {
        // If the ball is going at too steep or shallow of an angle (minAngle), fix that.
        float speed = rigidBody2D.velocity.magnitude;
        if (speed > minSpeed) speed = speed - (speed - minSpeed) * 0.9f;
        if (speed < minSpeed) speed = minSpeed;
        if (speed > maxSpeed) speed = maxSpeed;

        Vector3 direction = rigidBody2D.velocity.normalized;
        float signX = Mathf.Sign(direction.x);
        float signY = Mathf.Sign(direction.y);

        if (Mathf.Abs(direction.y) + .01f < maSin) direction = new Vector3(signX * maCos, signY * maSin, 0);
        if (Mathf.Abs(direction.x) + .01f < maSin) direction = new Vector3(signX * maSin, signY * maCos, 0);
        rigidBody2D.velocity = speed * direction;
    }

    void OnCollisionExit2D(Collision2D collision) {
        FixBallVelocity();
    }
}
