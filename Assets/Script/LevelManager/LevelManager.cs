using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public RebornPoint nowRebornPoint;

    public Vector3 lastDownPoint;

    void Awake()
    {
        instance = this;
    }


    public void CheckRebornPoint(RebornPoint rebornPoint)
    {
        if (nowRebornPoint != null)
        {
           if(nowRebornPoint.CheckIdx > rebornPoint.CheckIdx)
            {
                return;
            }
        }
        nowRebornPoint = rebornPoint;
    }

    public void SetLastDownPoint(Vector3 ldPoint)
    {
        lastDownPoint = ldPoint;
    }

    public void Reborn(PlayerController ctrller)
    {
        switch (nowRebornPoint.rebornType)
        {
            case RebornType.Normal:
                ctrller.ReBorn(nowRebornPoint.transform.position);
                break;
            case RebornType.Set:
                ctrller.ReBorn(nowRebornPoint.rebornPoint.position);
                break;
            case RebornType.LastDown:
                //statement(s);
                ctrller.ReBorn(lastDownPoint);
                break;
            default: 
                break;
        }
    }
}
