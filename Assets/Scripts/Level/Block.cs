using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] int points = 10;
    [SerializeField] int speedClass = 0;
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
        if (collision.gameObject.tag == "ball")
            if (breakable)
            {
                Debug.Log(level);
                Debug.Log(level.game);
                level.game.IncreaseScore(points);
                level.game.RemoveBlock();
                Destroy(gameObject);
                level.game.SetBallSpeedClass(speedClass);
            }

            if (!(soundTag is null) && soundTag.Length > 0)
            {
                level.game.PlaySound(soundTag);
            }
    }
}
