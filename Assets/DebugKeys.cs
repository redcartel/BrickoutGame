using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugKeys : MonoBehaviour
{
    // Start is called before the first frame update
    GameController game;
    
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
                game.hudController.ShowWinMessage();
            }
            if (Input.GetKeyDown(KeyCode.F9) || Input.GetKeyDown(KeyCode.D)) {
                Debug.Log("F9");
                //game.hudController.DisableDefaults();
            }
    }
}
