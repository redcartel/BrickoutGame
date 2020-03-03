﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] int soundNum = 6;

    Game game;
    // Start is called before the first frame update
    private Image blackBar;
    private Text gameOverText;
    private bool playedGameOverSound = false;

    private void Start()
    {
        game = FindObjectOfType<Game>();
        blackBar = GameObject.Find("BlackBar").GetComponent<Image>();
        gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
        blackBar.enabled = false;
        gameOverText.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (game.global.lives <= 0 && game.normalLevel)
        {
            game.frozen = true;
            blackBar.enabled = true;
            gameOverText.enabled = true;
            if (Input.GetMouseButtonDown(0))
            {
                game.ResetGame();
            }

            if (!playedGameOverSound)
            {
                game.playSound(soundNum);
            }
            playedGameOverSound = true;
        }
    }
}
