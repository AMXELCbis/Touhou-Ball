using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public RebornPoint nowRebornPoint;

    void Awake()
    {
        instance = this;
    }


    public void CheckRebornPoint(RebornPoint rebornPoint)
    {
        if (nowRebornPoint != null)
        {
           if(nowRebornPoint.CheckIdx >= rebornPoint.CheckIdx)
            {
                return;
            }
        }
        nowRebornPoint = rebornPoint;
    }
}
