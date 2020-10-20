using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private Texture2D levelMap;
    [SerializeField]
    private LevelColor[] prefabs;

    private Texture2D actualLevelMap;
    private LevelCell[,] levelMatrix;
    
    public void GenerateLevel()
    {
        if (levelMatrix == null || actualLevelMap.height != levelMap.height || actualLevelMap.width != levelMap.width)
        {
            actualLevelMap = levelMap;
            levelMatrix = GenerateMatrix(actualLevelMap.width, actualLevelMap.height);
        }
        
        for (int x = 0; x < levelMap.width; x++)
        {
            for (int y = 0; y < levelMap.height; y++)
            {
                GenerateCell(x, y);
            }
        }
    }

    public void DestroyLevel()
    {
        foreach(Transform child in transform)
        {
            for(int i = 0; i < child.childCount; i++)
            {
                DestroyImmediate(child.GetChild(i).gameObject);
            }
        }

        levelMatrix = null;
    }

    private LevelCell[,] GenerateMatrix(int x, int y)
    {
        LevelCell[,] matrix = new LevelCell[x, y];

        for (int i = 0; i < x; i++)
        {
            for (int e = 0; e < y; e++)
            {
                matrix[i, e] = new LevelCell();
            }
        }

        return matrix;
    }

    private void GenerateCell(int x, int y)
    {
        Color pixelColor = levelMap.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            //El pixel es transparente
            return;
        }

        foreach (LevelColor prefab in prefabs)
        {
            if (prefab.color == pixelColor && levelMatrix[x, y].thisColor.color != prefab.color)
            {
                Vector3 position = new Vector3(x * 5, 0, y * 5);
                levelMatrix[x, y].DestroyCell();
                levelMatrix[x, y] = new LevelCell(prefab, Instantiate(prefab.prefab, position, Quaternion.identity, prefab.parent));
            }
        }
    }
}
