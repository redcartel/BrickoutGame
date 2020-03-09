using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugKeys : MonoBehaviour
{
    // Start is called before the first frame update
    GameController game;

    Global global;

    int clickCount = 0;

    int sequencePosition = 0;
    
    void Start()
    {
        game = GetComponentInParent<GameController>();
        global = FindObjectOfType<Global>();
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.F12)) {
                Debug.Log("F12");
                game.foregroundController.Unfreeze();
            }
            if (Input.GetKeyDown(KeyCode.F11)) {
                Debug.Log("F11");
                game.foregroundController.Freeze();
            }
            if (Input.GetKeyDown(KeyCode.F10)) {
                //game.hudController.ShowWinMessage();
            }
            if (Input.GetKeyDown(KeyCode.F9) || Input.GetKeyDown(KeyCode.D)) {
                game.WinLevel();
                //game.hudController.DisableDefaults();
            }
            if (Input.GetKeyDown(KeyCode.F1)) {
                if (game.hudController.IsVisible<DevelopUI>()) {
                    game.hudController.SetVisible<DevelopUI>(false);
                }
                else
                {
                    game.hudController.SetVisible<DevelopUI>(true);
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                advanceCode("U");
            }

            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                advanceCode("D");
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                advanceCode("L");
            }

            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                advanceCode("R");
            }

            if (Input.GetKeyDown(KeyCode.B)) {
                advanceCode("B");
            }

            if (Input.GetKeyDown(KeyCode.A)) {
                advanceCode("A");
            }
    }

    void advanceCode(string symbol) {
        char symb = symbol[0];
        const string code = "UUDDLRLRBA";
        if (symb == code[sequencePosition]) {
            sequencePosition++;
            Debug.Log(sequencePosition);
            if (sequencePosition == code.Length) {
                global.lives = 99;
                sequencePosition = 0;
            }
        }
        else {
            if (symb == "U"[0]) sequencePosition = 1;
            else sequencePosition = 0;
        }
    }

    public void HandleClick(float eX, float eY) {
        if (eY > game.expectedHeight - 32 && eY > game.expectedWidth - 32) {
            clickCount++;
        }
        if (clickCount >= 5) {
            game.WinLevel();
        }
    }
}
