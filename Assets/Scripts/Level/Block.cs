using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] int points = 10;
    [SerializeField] float bounceSpeed = 50f;
    [SerializeField] bool breakable = false;
    [SerializeField] public string soundTag = "HARDBOUNCE";
    [SerializeField] public float width;
    [SerializeField] public float height;

    [SerializeField] public LevelController level;

    void Start()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball;
        if (ball = collision.gameObject.GetComponent<Ball>()) {
            ball.SetMinMaxSpeeds(bounceSpeed);
            ball.enforceMinMaxSpeed();
            if (breakable) {
                level.game.IncreaseScore(points);
                level.game.RemoveBlock();
                Destroy(gameObject);
            }
        }
        level.game.PlaySound(soundTag);
    }
}
