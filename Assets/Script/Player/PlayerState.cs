using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDefine;
public class PlayerState
{
    /// <summary>
    /// 参数列表
    /// </summary>
    public List<float> paramList = new List<float>();

    public bool isOn = false;

    public int layer = 0;

    public PlayerStateType stateType;

    public static PlayerState operator +(PlayerState first, PlayerState second)
    {
        PlayerState result = first;
        if (first != null && second != null)
        {
            if(first.stateType == second.stateType)
            {
                for(int i = 0; i < result.paramList.Count; i++)
                {
                    result.paramList[i] += second.paramList[i];
                }
                result.layer = result.layer + second.layer;
                result.isOn = result.layer > 0;
            }
        }

        return result;

    }
    public static PlayerState operator -(PlayerState first, PlayerState second)
    {
        PlayerState result = first;
        if (first != null && second != null)
        {
            if(first.stateType == second.stateType)
            {
                for (int i = 0; i < result.paramList.Count; i++)
                {
                    result.paramList[i] -= second.paramList[i];
                    result.paramList[i] = result.paramList[i] < 0? 0 : result.paramList[i];
                }
                result.layer = result.layer - second.layer;

                result.layer = result.layer < 0 ? 0 : result.layer;
                result.isOn = result.layer > 0;
            }
        }

        return result;

    }

}
