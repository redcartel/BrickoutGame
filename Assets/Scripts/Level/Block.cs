using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] int points = 10;
    [SerializeField] int speedClass = 0;
    [SerializeField] bool breakable = true;
    [SerializeField] public int soundNum = 0;

    GameController game;

    void Start()
    {
        game = FindObjectOfType<GameController>();
        if (breakable)
        {
            game.AddBlock();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (breakable)
        {
            game.IncreaseScore(points);
            game.RemoveBlock();
            Destroy(gameObject);
            game.SetSpeedClass(speedClass);
        }
        game.playSound(soundNum);
    }
}
