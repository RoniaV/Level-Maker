using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCell
{
    public LevelColor thisColor;
    public GameObject actualObj;

    public LevelCell()
    {
        thisColor = new LevelColor();
    }

    public LevelCell(LevelColor thisColor, GameObject obj)
    {
        this.thisColor = thisColor;
        actualObj = obj;
    }

    public void DestroyCell()
    {
        Object.DestroyImmediate(actualObj);
    }
}
