using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    GameController game;

    [SerializeField] public float effectiveMouseX;
    [SerializeField] public float effectiveMouseY;
    private float lastEMouseX = 0;
    private float lastEMouseY = 0;

    private float effectiveScreenWidth;
    private float xOffset;
    private float invScreenWidth;
    private float invScreenHeight;

    void Start()
    {
        game = FindObjectOfType<GameController>();
        CalculateScreenGeometry();
    }

    void CalculateScreenGeometry()
    {
        effectiveScreenWidth = game.expectedHeight * ((float)Screen.width / (float)Screen.height);
        xOffset = (effectiveScreenWidth - game.expectedWidth) / 2f;
        invScreenWidth = 1.0f / (float)Screen.width;
        invScreenHeight = 1.0f / (float)Screen.height;
    }

    // Update is called once per frame
    void Update()
    {  
        effectiveMouseX = Input.mousePosition.x * invScreenWidth * effectiveScreenWidth - xOffset;
        effectiveMouseY = Input.mousePosition.y * invScreenHeight * game.expectedHeight;
        if (Mathf.Abs(effectiveMouseX - lastEMouseX) + Mathf.Abs(effectiveMouseY - lastEMouseY) >= 1.0f) {
            lastEMouseX = effectiveMouseX;
            lastEMouseY = effectiveMouseY;
            game.HandleMouseMove(effectiveMouseX, effectiveMouseY);
        }
        if (Input.GetMouseButtonDown(0))
        {
            game.HandleClick(effectiveMouseX, effectiveMouseY);
        }
    }
}
