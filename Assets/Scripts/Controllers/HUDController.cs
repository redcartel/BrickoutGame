using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDController : MonoBehaviour
{
    [SerializeField] public GameController game;
    /*
    [SerializeField] private string disabledTag = "defaultDisabled";
    [SerializeField] private GameUI gameUI;
    [SerializeField] private StartUI startUI;
    [SerializeField] private WinUI winUI;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private DevelopUI developUI;
    */
    void Start()
    {
        SetVisible<GameUI>(true);
        SetVisible<StartUI>(false);
        SetVisible<GameOverUI>(false);
        SetVisible<WinUI>(false);
        SetVisible<DevelopUI>(false);

        if (game.levelData.showStartUI) SetVisible<StartUI>(true);
    }

    public void SetVisible<T>(bool visible = true) where T : MonoBehaviour {
        float alpha = visible ? 1.0f : 0.0f;
        T canvas = GetComponentInChildren<T>();
        CanvasGroup cgrp = canvas.GetComponent<CanvasGroup>();
        cgrp.alpha = alpha;
        cgrp.blocksRaycasts = visible;
    }

    public bool IsVisible<T>() where T : MonoBehaviour {
        CanvasGroup cgrp = GetComponentInChildren<T>().GetComponent<CanvasGroup>();
        return (cgrp.alpha > .0001);
    }

    public void HandleClick(float eX, float eY)
    {
        Debug.Log("HUD click");
        if (IsVisible<StartUI>())
        {
            Debug.Log("Start click");
            game.StartGame();
        }
        else if (IsVisible<GameOverUI>())
        {
            game.ResetGame();
        }
        else if (IsVisible<WinUI>())
        {
            game.ResetGame();
        }
    }

    /*
    void HandleDevUIClick(float eX, float eY) {

    }
    */
}
