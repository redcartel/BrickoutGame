using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDController : MonoBehaviour
{
    [SerializeField] private GameController game;

    [SerializeField] private string disabledTag = "defaultDisabled";
    [SerializeField] private GameUI gameUI;
    [SerializeField] private StartUI startUI;
    [SerializeField] private WinUI winUI;
    [SerializeField] private GameOverUI gameOverUI;

    public bool startShown = false;
    public bool gameOverShown = false;
    public bool winShown = false;

    void Start()
    {
    }


    public void SetAlpha(CanvasGroup cg, float alpha)
    {
        if (!(cg is null)) {
            cg.alpha = alpha;
            if (alpha < 0.00001f) {
                cg.blocksRaycasts = false;
            }
            else if (alpha > 0.99999) {
                cg.blocksRaycasts = true;
            }
        }
        else {
            Debug.LogWarning(string.Format("Null CanvasGroup for Canvas"));
        }
    }

    public void RefreshGameUI()
    {
        gameUI.SetScore(game.score);
        gameUI.SetLives(game.lives);
        gameUI.SetLevel(game.level);
    }

    public void HandleClick(float eX, float eY)
    {
        if (startShown)
        {
            game.StartGame();
        }
        else if (gameOverShown)
        {
            game.ResetGame();
        }
        else if (winShown)
        {
            game.ResetGame();
        }
    }

    public void ShowGameOver()
    {
        SetAlpha(gameOverUI.GetComponent<CanvasGroup>(), 1.0f);
        gameOverShown = true;
    }

    public void ShowStartMessage()
    {
        SetAlpha(startUI.GetComponent<CanvasGroup>(), 1.0f);
        startShown = true;
    }

    public void ShowWinMessage()
    {
        SetAlpha(winUI.GetComponent<CanvasGroup>(), 1.0f);
        winShown = true;
    }
}
