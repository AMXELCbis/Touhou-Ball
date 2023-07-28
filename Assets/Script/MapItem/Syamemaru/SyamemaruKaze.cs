using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDefine;

public class SyamemaruKaze : MonoBehaviour
{
    
    [SerializeField,Header("风力大小")]
    public float kazePower;
    [SerializeField, Header("风力方向")]
    public Vector3 kazeVec = Vector3.zero;
    [SerializeField, Header("是否受起终点控制风力方向")]
    bool pointCtrl = true;
    [SerializeField, Header("起点")]
    Transform startPoint;
    [SerializeField, Header("终点")]
    Transform endPoint;
    [SerializeField, Header("风力影响最大速度参数")]
    private float windSlowPower;

    private void Start()
    {
        if(pointCtrl)
        {
            kazeVec = endPoint.position - startPoint.position;
            kazeVec = kazeVec.normalized;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other?.gameObject.tag == "Player")
        {
            PlayerStateCtrl ctrller = other.gameObject.GetComponent<PlayerStateCtrl>();
            PlayerState state = new PlayerState();
            state.FloatparamList.Add(kazeVec.x * windSlowPower);
            state.FloatparamList.Add(kazeVec.z * windSlowPower);
            state.FloatparamList.Add(kazeVec.x * kazePower);
            state.FloatparamList.Add(kazeVec.z * kazePower);

            ctrller?.AddState(PlayerStateType.InKaze,  state, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other?.gameObject.tag == "Player")
        {
            PlayerStateCtrl ctrller = other.gameObject.GetComponent<PlayerStateCtrl>();
            PlayerState state = new PlayerState();
            state.FloatparamList.Add(kazeVec.x * windSlowPower);
            state.FloatparamList.Add(kazeVec.z * windSlowPower);
            state.FloatparamList.Add(kazeVec.x * kazePower);
            state.FloatparamList.Add(kazeVec.z * kazePower);

            ctrller?.RemoveState(PlayerStateType.InKaze, state, 1);
        }
    }
}
