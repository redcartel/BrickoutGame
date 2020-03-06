using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text scoreOutput;
    [SerializeField] Text livesOutput;
    [SerializeField] Text levelOutput;
    public void SetScore(int value) { scoreOutput.text = value.ToString("D6"); }
    public void SetLives(int value) { livesOutput.text = value.ToString("D2"); }
    public void SetLevel(int value) { levelOutput.text = value.ToString("D2"); }
}
