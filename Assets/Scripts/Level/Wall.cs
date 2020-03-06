using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LevelController level;
    [SerializeField] public int soundNum = 3;
    [SerializeField] string soundTag = "HARDBOUNCE";

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(string.Format("Wall Bounce with gameObject {0}", collision.gameObject.name));
        if (collision.gameObject.tag == "ball")
        {
            level.game.PlaySound(soundTag);
        }
    }
}
