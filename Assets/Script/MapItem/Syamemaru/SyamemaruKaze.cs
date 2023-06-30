using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDefine;

public class SyamemaruKaze : MonoBehaviour
{
    
    [SerializeField,Header("������С")]
    public float kazePower;
    [SerializeField, Header("��������")]
    public Vector3 kazeVec = Vector3.zero;
    [SerializeField, Header("�Ƿ������յ���Ʒ�������")]
    bool pointCtrl = true;
    [SerializeField, Header("���")]
    Transform startPoint;
    [SerializeField, Header("�յ�")]
    Transform endPoint;
    [SerializeField, Header("����Ӱ������ٶȲ���")]
    private float kazeSlowPower;

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
            state.paramList.Add(kazeVec.x * kazeSlowPower);
            state.paramList.Add(kazeVec.z * kazeSlowPower);
            state.paramList.Add(kazeVec.x * kazePower);
            state.paramList.Add(kazeVec.z * kazePower);

            ctrller?.AddState(PlayerStateType.InKaze,  state, 1);
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other?.gameObject.tag == "Player")
    //    {
    //        PlayerController ctrller = other.gameObject.GetComponent<PlayerController>();
    //        ctrller?.rb?.AddForce(kazeVec * kazePower);
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other?.gameObject.tag == "Player")
        {
            PlayerStateCtrl ctrller = other.gameObject.GetComponent<PlayerStateCtrl>();
            PlayerState state = new PlayerState();
            state.paramList.Add(kazeVec.x * kazeSlowPower);
            state.paramList.Add(kazeVec.z * kazeSlowPower);
            state.paramList.Add(kazeVec.x * kazePower);
            state.paramList.Add(kazeVec.z * kazePower);

            ctrller?.RemoveState(PlayerStateType.InKaze, state, 1);
        }
    }
}
