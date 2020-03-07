using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] public GameObject[] elementSources;
    [SerializeField] public int expectedRows = 16;
    [SerializeField] public int stageTop = 224; // TODO: make variable, set in LevelData 
    [SerializeField] public int brickXOffset = 0;
    [SerializeField] public int brickYOffset = 0;
    
    // [SerializeField] public int offsetX = 8;
    [SerializeField] public int elementWidth = 16;
    [SerializeField] public int elementHeight = 8;

    [SerializeField] public GameObject generatedElementsContainer;

    public GameController game;
    public int blockCount = 0;
    public bool levelInitialized = false;

    void Start()
    {
        Debug.Log("LevelController Start");
        for (int i = 0; i < elementSources.Length; i++)
        {
            elementSources[i].transform.position = new Vector3(1000000, 1000000, 1000000);
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
        levelInitialized = true;
    }

    public void PlaceElement(int code, int colIndex, int rowIndex, int numRows=16)
    {
        if (code < 0) return;
        if (code > elementSources.Length) return;

        int x = (int)game.borderWidth + (int)elementWidth * (int)colIndex + brickXOffset;
        int rowFromBottom = numRows - rowIndex - 1;
        int y = stageTop - (rowIndex + 1) * elementHeight + brickYOffset;
        GameObject newElement = Instantiate(elementSources[code]);
        if (newElement.tag == "mustDestroy") {
            game.AddBlock();
        }
        newElement.transform.position = new Vector3(x, y, 0);
        newElement.transform.parent = generatedElementsContainer.transform;
    }

    // Turn a string of comma separated integers into an array of ints.
    // non-integer values returned as -1 (blank)
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
}
