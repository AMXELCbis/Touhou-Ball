using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public int PlayerLife;

    public static LevelManager instance;

    public RebornPoint nowRebornPoint;

    public Vector3 lastDownPoint;

    public Vector3 curForward;//µ±Ç°Ç°·½
    public Vector3 curRight//µ±Ç°ÓÒ·½
    {
        get
        {
            if (curForward == Vector3.forward)
            {
                return Vector3.right;
            }
            else if (curForward == Vector3.left)
            {
                return Vector3.forward;
            }
            else if (curForward == Vector3.back)
            {
                return Vector3.left;
            }
            else if (curForward == Vector3.right)
            {
                return Vector3.back;
            }
            return Vector3.zero;
        }
    }
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
                ctrller.ReBorn(nowRebornPoint.bornPosition);
                break;
            case RebornType.Set:
				ctrller.ReBorn(nowRebornPoint.bornPosition);
				break;
            case RebornType.LastDown:
                //statement(s);
                ctrller.ReBorn(lastDownPoint);
                break;
            default: 
                break;
        }
    }

    /// <summary>
    /// param direction 0 ×ó 1 ÓÒ
    /// </summary>
    /// <param name="direction"></param>
    public void ChangeForwardVec(int direction)
    {
        if(direction == 0)
        {
            if(curForward == Vector3.forward)
            {
                curForward = Vector3.left;
            }
            else if(curForward == Vector3.left)
            {
                curForward = Vector3.back;
            }
            else if(curForward == Vector3.back)
            {
                curForward = Vector3.right;
            }
            else if(curForward == Vector3.right)
            {
                curForward = Vector3.forward;
            }
        }
        else
        {
            if (curForward == Vector3.forward)
            {
                curForward = Vector3.right;
            }
            else if (curForward == Vector3.left)
            {
                curForward = Vector3.forward;
            }
            else if (curForward == Vector3.back)
            {
                curForward = Vector3.left;
            }
            else if (curForward == Vector3.right)
            {
                curForward = Vector3.back;
            }
        }
    }
}
