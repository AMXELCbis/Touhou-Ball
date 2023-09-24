using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDefine;
[SerializeField]
public class PlayerState
{
	/// <summary>
	/// 参数列表
	/// </summary>
	public List<float> FloatparamList = new List<float>();

	public List<GameObject> ObjectparamList = new List<GameObject>();


	public bool isOn = false;

	public int layer = 0;

	public PlayerStateType stateType;

	public static PlayerState operator +(PlayerState first, PlayerState second)
	{
		PlayerState result = first;
		if (first != null && second != null)
		{
			if (first.stateType == second.stateType)
			{
				for (int i = 0; i < result.FloatparamList.Count; i++)
				{
					Debug.Log("ADD " + result.FloatparamList[i] + " " + second.FloatparamList[i]);

					result.FloatparamList[i] += second.FloatparamList[i];
				}
				for (int i = 0; i < second.ObjectparamList.Count; i++)
				{
					result.ObjectparamList.Add(second.ObjectparamList[i]);

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
			if (first.stateType == second.stateType)
			{
				for (int i = 0; i < result.FloatparamList.Count; i++)
				{
					Debug.Log("Minues " + result.FloatparamList[i] + " " + second.FloatparamList[i]);
					result.FloatparamList[i] -= second.FloatparamList[i];
					//result.FloatparamList[i] = result.FloatparamList[i] < 0 ? 0 : result.FloatparamList[i];
				}
				for (int i = 0; i < second.ObjectparamList.Count; i++)
				{
					result.ObjectparamList.Remove(second.ObjectparamList[i]);

				}
				result.layer = result.layer - second.layer;

				result.layer = result.layer < 0 ? 0 : result.layer;
				result.isOn = result.layer > 0;
			}
		}

		return result;

	}

}
