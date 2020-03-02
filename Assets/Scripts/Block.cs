using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] int points = 10;

    Game game;

    void Start()
    {
        game = FindObjectOfType<Game>();
        game.AddBlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        game.IncreaseScore(points);
        game.RemoveBlock();
        Destroy(gameObject);
    }
}
