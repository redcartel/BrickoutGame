using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugKeys : MonoBehaviour
{
    // Start is called before the first frame update
    GameController game;

    int clickCount = 0;
    
    void Start()
    {
        game = GetComponentInParent<GameController>();
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
