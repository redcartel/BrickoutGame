using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] ForegroundController foreground;
    public float width = 0.0f;
    public float leftPaddleBound = 0.0f;
    public float rightPaddleBound = 0.0f;
    [SerializeField] public string soundTag = "PADDLEBOUNCE";
    
    
    [SerializeField] float regularSpeed = 200f;
    [SerializeField] float fastSpeed = 400f;
    public Rigidbody2D rigidBody2D;

    

    void Start()
    {
        width = GetComponent<SpriteRenderer>().sprite.rect.width;
        leftPaddleBound = width / 2;
        rightPaddleBound = foreground.game.expectedWidth - width / 2;
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "ball" || foreground.stuckToPaddle)
        {
            return;
        }
        foreground.game.PlaySound(soundTag);
    }
}
