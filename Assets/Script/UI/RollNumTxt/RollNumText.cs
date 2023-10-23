using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class RollNumText : MonoBehaviour
{
	public GameObject GoRollNum;
	public Transform content;
	[Header("结尾字符位置")]
	public Vector3 beginPos = new Vector3 (101f, -5f, 0f);
	[Header("字符间隔")]
	public float offsetVertical = -37;
	[Header("总共滚动时间")]
	public float totalTime = 2;
	[Header("每格降速参数")]
	public float slowDownScale = 1.02f;
	int targetNum;
	public int curTextIdx;

	public List<GoNum> targets = new List<GoNum>();


	/// <summary>
	/// 调用开启滚动
	/// </summary>
	/// <param name="num"></param>
	public void StartRollNum(int num)
	{
		string numStr = num.ToString();
		targetNum = num;
		float checkScale = 1;
		for (int i = 1; i < numStr.Length; i++)
		{
			int idx = i;
			if (idx > 2)
			{
				idx = 2;
			}
			checkScale += math.pow(slowDownScale, idx + 1);
		}

		//for (int i = 0; i < numStr.Length; i++)
		for (int i = 0; i < 1; i++)
		{
			curTextIdx = i;
			int curIdx = numStr.Length - 1 - i;
			GameObject go = GameObject.Instantiate(GoRollNum, Vector3.zero, Quaternion.identity);
			go.transform.parent = content;

			GoNum gn = go.GetComponent<GoNum>();
			gn.transform.localPosition = beginPos + new Vector3(i * offsetVertical, 0, 0);
			Debug.Log("curIdx  " + curIdx + " " + i + " " + gn.transform.localPosition + " " + numStr[curIdx]);

			targets.Add(gn);
			int idx = i;
			if (idx > 2)
			{
				idx = 2;
			}
			//float time = (totalTime / checkScale) * math.pow(slowDownScale, i + 1);
			totalTime *= math.pow(slowDownScale, curIdx);
			gn.SetBeginNum(0, 0, curIdx);
			gn.RollNum(int.Parse(numStr[curIdx] + ""), totalTime);
		}
	}

	public void StartNewNum()
	{
		
	}
}
