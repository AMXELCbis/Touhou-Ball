using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDefine;

public class PlayerStateCtrl : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    public Dictionary<PlayerStateType, PlayerState> stateDic = new Dictionary<PlayerStateType, PlayerState>();

    public void AddState(PlayerStateType type, PlayerState state, int layer = 1)
    {
        if (stateDic.ContainsKey(type))
        {
            state.layer = layer;
            stateDic[type] += state;
        }
        else
        {
            state.isOn = true;
            state.layer = layer;
            stateDic.Add(type, state);
        }
    }

    public void RemoveState(PlayerStateType type, PlayerState state, int layer = 1)
    {
        if (stateDic.ContainsKey(type))
        {
            state.layer = layer;
            stateDic[type] -= state;
        }
        else
        {

        }
    }
}
