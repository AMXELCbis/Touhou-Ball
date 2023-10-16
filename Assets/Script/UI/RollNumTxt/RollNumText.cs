using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class RollNumText : MonoBehaviour
{
	public GameObject GoRollNum;
	public Transform content;
	[Header("��β�ַ�λ��")]
	public Vector3 beginPos = new Vector3 (101f, -5f, 0f);
	[Header("�ַ����")]
	public float offsetVertical = -37;
	[Header("�ܹ�����ʱ��")]
	public float totalTime = 2;
	[Header("ÿ���ٲ���")]
	public float slowDownScale = 1.02f;
	int targetNum;

	public List<Transform> targets = new List<Transform>();


	/// <summary>
	/// ���ÿ�������
	/// </summary>
	/// <param name="num"></param>
	public void StartRollNum(int num)
	{
		string numStr = num.ToString();
		targetNum = num;
		//float checkScale = 1;
		//for (int i = 1; i < numStr.Length; i++)
		//{
		//	int idx = i;
		//	if (idx > 2)
		//	{
		//		idx = 2;
		//	}
		//	checkScale += math.pow(slowDownScale, idx + 1);
		//}

		//destory previous children
		int Numchild = content.childCount;

		for (int i =0; i< Numchild;i++)
		{
			Object.Destroy(content.GetChild(i).gameObject);
		}



		for (int i = 0; i < numStr.Length; i++)
		{
			int curIdx = numStr.Length - 1 - i;
			GameObject go = GameObject.Instantiate(GoRollNum, Vector3.zero, Quaternion.identity, content);
			go.transform.localEulerAngles = Vector3.zero;

			GoNum gn = go.GetComponent<GoNum>();
			gn.transform.localPosition = beginPos + new Vector3(i * offsetVertical, 0, 0);
			Debug.Log("curIdx  " + curIdx + " " + i + " " + gn.transform.localPosition + " " + numStr[curIdx]);

			targets.Add(gn.transform);
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
}
