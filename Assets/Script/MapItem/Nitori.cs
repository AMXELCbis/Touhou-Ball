using GameDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitori : MonoBehaviour
{
	[SerializeField, Header("������������Ӱ�����")]
	private float slipperyPower;
	[SerializeField, Header("����Ӱ��ʩ������")]
	private float slipperyApplyPower;
	// Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{
		if (other?.gameObject.tag == "Player")
		{
			PlayerStateCtrl ctrller = other.gameObject.GetComponent<PlayerStateCtrl>();
			PlayerState state = new PlayerState();
			state.FloatparamList.Add(slipperyPower);
			state.FloatparamList.Add(slipperyApplyPower);
			ctrller?.AddState(PlayerStateType.Slippy, state, 1);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other?.gameObject.tag == "Player")
		{
			PlayerStateCtrl ctrller = other.gameObject.GetComponent<PlayerStateCtrl>();
			PlayerState state = new PlayerState();
			state.FloatparamList.Add(slipperyPower);
			state.FloatparamList.Add(slipperyApplyPower);

			ctrller?.RemoveState(PlayerStateType.Slippy, state, 1);
		}
	}
}
