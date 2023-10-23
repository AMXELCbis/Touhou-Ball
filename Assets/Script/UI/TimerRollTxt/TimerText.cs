using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
	public GameObject GoRollNum;
	public Transform content;
	[Header("结尾字符位置")]
	public Vector3 beginPos = new Vector3(101f, 0f, 0f);
	[Header("间隔时间")]
	public float rollTime = 0.2f;
	[Header("总间隔时间")]
	public float totalTime = 2f;
	[Header("使用总时间")]
	public bool useTotalTime = false;
	int targetNum;
	public int curNum;
	public float time, endTime;

	public List<TimerGoNum> targets = new List<TimerGoNum>();

	public TextMeshProUGUI timeCount;

	public void SetBeginNum(int beginNum)
	{
		curNum = beginNum;
		string numStr = beginNum.ToString();
		for(int i = 0;  i < numStr.Length; i++)
		{
			GameObject go = GameObject.Instantiate(GoRollNum, Vector3.zero, Quaternion.identity);
			go.transform.parent = content;
			go.transform.localScale = Vector3.one;

			go.transform.localEulerAngles = Vector3.zero;

			TimerGoNum gn = go.GetComponent<TimerGoNum>();
			gn.transform.localPosition = beginPos;
			int strIdx = numStr.Length - i - 1;
			gn.SetBeginNum(int.Parse(numStr[strIdx] + ""));
			targets.Add(gn);
		}
	}

	public void StartRoll(int changeNum)
	{
		StartCoroutine(ChangeNum(changeNum)) ;
		time = Time.time;
	}

	IEnumerator ChangeNum(int changeNum)
	{
		if(useTotalTime)
		{
			rollTime = totalTime / Mathf.Abs(changeNum - curNum);
		}
		if (changeNum > curNum)
		{
			while (changeNum > curNum)
			{
				PlusNum();
				yield return new WaitForSeconds(rollTime);

			}
			endTime = Time.time;
		}
		else if(changeNum < curNum)
		{
			while (changeNum < curNum)
			{
				MinuesNum();
				yield return new WaitForSeconds(rollTime);

			}
			endTime = Time.time;
		}
		//timeCount.text = (endTime - time) + "";


	}

	public void PlusNum()
	{
		curNum ++;
		string numStr = curNum.ToString();
		string lastStr = (curNum - 1).ToString();
		if (curNum == 0 || (lastStr.Length < numStr.Length))
		{
			GameObject go = GameObject.Instantiate(GoRollNum, Vector3.zero, Quaternion.identity);
			go.transform.parent = content;
			go.transform.localScale = Vector3.one;

			go.transform.localEulerAngles = Vector3.zero;


			TimerGoNum gn = go.GetComponent<TimerGoNum>();
			gn.transform.localPosition = beginPos;
			targets.Add(gn);
		}

		for (int i = 0; i < numStr.Length; i++)
		{
			int strIdx = numStr.Length - i - 1;
			int targetNum = int.Parse(numStr[strIdx] + "");
			if (i == 2)
			{
				Debug.Log("PlusNum " + i + " " + targetNum);
			}
			targets[i].BeginChange(targetNum, rollTime);


		}
	}


	public void MinuesNum()
	{
		curNum--;
		string numStr = curNum.ToString();
		string lastStr = (curNum + 1).ToString();
		if (lastStr.Length > numStr.Length)
		{

			TimerGoNum gn = targets[targets.Count - 1];
			targets.Remove(gn);
			GameObject.Destroy(gn.gameObject);
		}

		for (int i = 0; i < numStr.Length; i++)
		{
			int strIdx = numStr.Length - i - 1;
			int targetNum = int.Parse(numStr[strIdx] + "");
			if(i == 2)
			{
				Debug.Log("targetNum " + i + " " + targetNum);
			}
			targets[i].BeginChange(targetNum, rollTime);


		}
	}
}
