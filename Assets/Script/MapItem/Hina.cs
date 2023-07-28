using GameDefine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hina : MonoBehaviour
{
	public Vector3 rotate;
	public float RotateForce;

	private Transform tran;

	// Start is called before the first frame update
	void Start()
    {
        tran = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
		tran.Rotate(new Vector3(rotate.x, 0, 0));
		tran.Rotate(new Vector3(0, rotate.y, 0));
		tran.Rotate(new Vector3(0, 0, rotate.z));
	}

	void OnCollisionEnter(Collision other)
	{
		if (other?.gameObject.tag == "Player")
		{
			PlayerStateCtrl ctrller = other.gameObject.GetComponent<PlayerStateCtrl>();
			PlayerState state = new PlayerState();
			state.stateType = PlayerStateType.InHina;

			state.ObjectparamList.Add(this.gameObject);
			state.FloatparamList.Add(RotateForce);
			state.FloatparamList.Add(rotate.x);
			state.FloatparamList.Add(rotate.y);
			state.FloatparamList.Add(rotate.z);


			ctrller?.AddState(PlayerStateType.InHina, state, 1);
		}
	}

	void OnCollisionExit(Collision other)
	{
		if (other?.gameObject.tag == "Player")
		{
			PlayerStateCtrl ctrller = other.gameObject.GetComponent<PlayerStateCtrl>();
			PlayerState state = new PlayerState();
			state.stateType = PlayerStateType.InHina;

			state.ObjectparamList.Add(this.gameObject);
			state.FloatparamList.Add(RotateForce);
			state.FloatparamList.Add(rotate.x);
			state.FloatparamList.Add(rotate.y);
			state.FloatparamList.Add(rotate.z);


			ctrller?.RemoveState(PlayerStateType.InHina, state, 1);

		}
	}

}
