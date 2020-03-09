using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: set speed and direction with getters & setters?

public class Ball : MonoBehaviour
{
    //[SerializeField] ForegroundController foreground;
    [SerializeField] float minAngle = 18f;
    public Rigidbody2D rigidBody2D;

    public float magnitude;
    private float maCos;
    private float maSin;
    public float maxSpeed;
    public float minSpeed;
    public float maxSpeedMultiplier = 1.5f;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        maCos = Mathf.Cos(minAngle / 180.0f * Mathf.PI);
        maSin = Mathf.Sin(minAngle / 180.0f * Mathf.PI);
    }

    // Does nothing important, just for debugging speed correction. Coment out
    void FixedUpdate() {
        magnitude = rigidBody2D.velocity.magnitude;
    }

    public void SetMinMaxSpeeds(float newMinSpeed)
    {
        if (minSpeed < newMinSpeed) {
            minSpeed = newMinSpeed;
            maxSpeed = minSpeed * maxSpeedMultiplier;
        }
    }

    public void enforceMinMaxSpeed() {

        maxSpeed = minSpeed * maxSpeedMultiplier;
        // Make sure ball's speed is > minSpeed and < minSpeed * 1.5 (by default)
        Vector2 velocity = rigidBody2D.velocity;
        Vector2 direction = velocity.normalized;
        float speed = velocity.magnitude;

        // after a ball bounces, if it is going slower than the minspeed, catch it up
        if (1 < velocity.magnitude && velocity.magnitude < minSpeed) {
            speed = minSpeed;
        }
        // if it is going faster than the max speed, slow it a lot
        else if (velocity.magnitude > (minSpeed * maxSpeedMultiplier)) {
            speed = speed - ((speed - maxSpeed) * .5f) - 5.0f;
        }
        // if it is going faster than the minspeed but less than maxspeed, slow it down a littl
        else if (velocity.magnitude > minSpeed) {
            speed = speed - ((speed - minSpeed) * .1f);
        }

        float signX = Mathf.Sign(direction.x);
        float signY = Mathf.Sign(direction.y);

        // Trig!
        // If absolute value of y is less than sin of the minimum angle, change ball direction to minimum low slope
        if (Mathf.Abs(direction.y) + .01f < maSin) direction = new Vector3(signX * maCos, signY * maSin, 0);
        // If absolute value of x is less than the sin of the minimum angle, change the ball to the maximum high slope
        if (Mathf.Abs(direction.x) + .01f < maSin) direction = new Vector3(signX * maSin, signY * maCos, 0);
        
        rigidBody2D.velocity = speed * direction;
    }

    /** The ball can't go too fast or too slow or at too straight of an angle
    * we fix that after the ball collides with something. */
    void OnCollisionExit2D(Collision2D collision) {
        enforceMinMaxSpeed();
    }
}
