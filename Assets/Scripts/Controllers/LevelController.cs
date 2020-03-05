using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] public GameObject[] elementSources;
    [SerializeField] public int expectedRows = 16;
    [SerializeField] public int bottomRowY = 232 - 16 * 8;
    [SerializeField] public int offsetX = 8;
    [SerializeField] public int elementWidth = 16;
    [SerializeField] public int elementHeight = 8;

    GameController game;

    void Start()
    {
        game = FindObjectOfType<GameController>();
        for (int i = 0; i < elementSources.Length; i++)
        {
            elementSources[i].transform.position = new Vector3(0, 0, 1000000);
        }
    }

    public void PopulateLevel(LevelData levelData)
    {
        for (int rowIndex = 0; rowIndex < levelData.mapData.Length; rowIndex++)
        {
            string row = levelData.mapData[rowIndex];
            int[] columnCodes = ColumnCodes(row);
            for (int colIndex = 0; colIndex < columnCodes.Length; colIndex++)
            {
                PlaceElement(columnCodes[colIndex], colIndex, rowIndex, levelData.mapData.Length);
            }
        }
    }

    public void PlaceElement(int code, int colIndex, int rowIndex, int numRows=16)
    {
        if (code < 0) return;

        int x = offsetX + elementWidth * colIndex;
        int rowFromBottom = numRows - rowIndex - 1;
        int y = bottomRowY + rowFromBottom * elementHeight;
        GameObject newElement = Instantiate(elementSources[code]);
        newElement.transform.position = new Vector3(x, y, 0);
        Debug.Log(string.Format("{0},{1} code {2}", x, y, code));
    }

    private int[] ColumnCodes(string mapRow)
    {
        string[] columnStrings = mapRow.Split(',');
        int[] columnCodes = new int[columnStrings.Length];
        for (int colIndex = 0; colIndex < columnStrings.Length; colIndex++)
        {
            string columnString = columnStrings[colIndex];
            int code = -1;
            if (int.TryParse(columnString, out code))
            {
                columnCodes[colIndex] = code;
            }
            else
            {
                columnCodes[colIndex] = -1;
            }
        }
        return columnCodes;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
