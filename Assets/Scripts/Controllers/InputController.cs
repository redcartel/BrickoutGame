using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public GameController game;

    [SerializeField] public float effectiveMouseX;
    [SerializeField] public float effectiveMouseY;
    private float lastEMouseX = 0;
    private float lastEMouseY = 0;

    public float effectiveScreenWidth;
    private float xOffset;
    private float invScreenWidth;
    private float invScreenHeight;
    private float rememberedScreenWidth = 0.0f;
    private float rememberedScreenHeight = 0.0f;

    void Start()
    {
        Debug.Log("InputController Start");
        CheckScreenDimensions();
    }

    private void CheckScreenDimensions()
    {
        float sWidth = (float)Screen.width;
        float sHeight = (float)Screen.height;
        if (Mathf.Abs(sWidth - rememberedScreenWidth) > 1.0 || 
            Mathf.Abs(sHeight - rememberedScreenHeight) > 1.0)
        {
            effectiveScreenWidth = game.expectedHeight * sWidth / sHeight;
            xOffset = (effectiveScreenWidth - game.expectedWidth) * .5f;

            invScreenWidth = 1.0f / (float)Screen.width;
            invScreenHeight = 1.0f / (float)Screen.height;

            rememberedScreenWidth = sWidth;
            rememberedScreenHeight = sHeight;
        }
    }

    // Called from GameController Update()
    public void UpdateCheckInput()
    {
        CheckScreenDimensions();
        effectiveMouseX = Input.mousePosition.x * invScreenWidth * effectiveScreenWidth - xOffset;
        effectiveMouseY = Input.mousePosition.y * invScreenHeight * game.expectedHeight;
        //Debug.Log(string.Format("{0}, {1}", invScreenWidth, effectiveScreenWidth));
        if (Mathf.Abs(effectiveMouseX - lastEMouseX) + Mathf.Abs(effectiveMouseY - lastEMouseY) >= 1.0f)
        {
            lastEMouseX = effectiveMouseX;
            lastEMouseY = effectiveMouseY;
            //Debug.Log("IC HMM");
            game.HandleMouseMove(effectiveMouseX, effectiveMouseY);
        }
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("IC HC");
            game.HandleClick(effectiveMouseX, effectiveMouseY);
        }
    }
}
