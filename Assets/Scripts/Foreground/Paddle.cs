using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float width = 32f;

    [SerializeField] Vector2 dimensions = new Vector2(32, 8);
    [SerializeField] public string soundTag = "PADDLEBOUNCE";
    [SerializeField] ForegroundController foreground;

    public Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "ball" || foreground.stuckToPaddle)
        {
            return;
        }
        foreground.game.PlaySound(soundTag);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball" && !foreground.stuckToPaddle) {
        }
    }
}
