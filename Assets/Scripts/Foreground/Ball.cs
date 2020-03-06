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

    void Start()
    {
       
        rigidBody2D = GetComponent<Rigidbody2D>();

        // the power of 8th grade math & the unit circle cannot be stopped
        maCos = Mathf.Cos(minAngle / 180.0f * Mathf.PI);
        maSin = Mathf.Sin(minAngle / 180.0f * Mathf.PI);

    }

    // FixedUpdate called once per physics frame;
    void FixedUpdate()
    {
        // the turn after waitToChangeBallVelocity, we check no matter what
        /*if (changeBallVelocityNextCycle)
        {
            changeBallVelocityNextCycle = false;
            FixBallVelocity();
        }
        // if it's the waitToChangeBallVelocity update step, set 
        // changeBallVelocityNextCycle to true
        else*/ if (waitToChangeBallVelocity)
        {
            waitToChangeBallVelocity = false;
            /*changeBallVelocityNextCycle = true;*/
        }
        // otherwise check if the velocity has changed since last time.
        else if ((lastVelocity - rigidBody2D.velocity).magnitude > .01f)
        {
            FixBallVelocity();
            lastVelocity = rigidBody2D.velocity;
        }
        else
        {
            lastVelocity = rigidBody2D.velocity;
        }
    }

    // Don't let the ball travel at too low or too great of a slope.
    private void FixBallVelocity()
    {
        // If the ball is going at too steep or shallow of an angle (minAngle), fix that.
        float speed = rigidBody2D.velocity.magnitude;
        Vector2 direction = rigidBody2D.velocity.normalized;

        // some trig
        if (Mathf.Abs(direction.y) + .01f < maSin)
        {
            Debug.Log("Ball Velocity Fixed");
            direction = new Vector2(Mathf.Sign(direction.x) * maCos, Mathf.Sign(direction.y) * maSin);
            rigidBody2D.velocity = speed * direction.normalized;
        }
        else if (Mathf.Abs(direction.x) +.01f < maSin)
        {
            Debug.Log("Ball Velocity Fixed");
            direction = new Vector2(Mathf.Sign(direction.x) * maSin, Mathf.Sign(direction.y) * maSin);
            rigidBody2D.velocity = speed * direction.normalized;
        }
    }
}
