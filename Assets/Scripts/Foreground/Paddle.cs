using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float width = 32f;

    [SerializeField] Vector2 dimensions = new Vector2(32, 8);
    [SerializeField] public string soundTag = "PADDLEBOUNCE";
    [SerializeField] ForegroundController foreground;

    void Start()
    {
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball" && !foreground.stuckToPaddle)
        {
            foreground.game.PlaySound(soundTag);
        }
    }
}
