using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GoNum : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI num1, num2;

	public int curText = 1;
	public int curNum;
	public int roundNum = 3;
	public int lastNum = 3;
	public float slowDownScale = 1.1f;
	public float lastWaitTime = 1f;

	public bool isUp = true;

	Vector3 pos1, pos2;
	int scale;
	private void Awake()
	{
		scale = isUp ? -1 : 1;

		pos1 = num1.transform.localPosition;
		pos2 = num2.transform.localPosition;
	}

	public void SetBeginNum(int num, int defLastNum, int defRoundNum)
	{
		int nextNum = 0;
		curNum = num;
		if (num == 9)
		{
			nextNum = 0;
		}
		else
		{
			nextNum = num + 1;
		}

		num1.text = num.ToString();
		num2.text = nextNum.ToString();
		lastNum = defLastNum;
		roundNum = defRoundNum;
	}
	public void SetNum()
	{
		int nextNum = 0;

		if (curNum == 9)
		{
			nextNum = 0;
		}
		else
		{
			nextNum = curNum + 1;
		}
		if (curNum % 2 == 1)
		{
			num2.text = curNum.ToString();
			num1.text = nextNum.ToString();
		}
		else
		{
			num1.text = curNum.ToString();
			num2.text = nextNum.ToString();
		}

	}

	public void test()
	{
		RollNum(9, 5);
	}

	public void RollNum(int targetNum, float totalTime)
	{
		StartCoroutine(CORollNum(targetNum, totalTime));
	}

	IEnumerator CORollNum(int targetNum, float totalTime)
	{
		int totalRollNum = roundNum * 10 + targetNum;
		int curRollNum = 0;
		while (curRollNum <= totalRollNum - 1)
		{
			float rollTime;
			int waitNum = totalRollNum - lastNum;
			if (curRollNum < waitNum)
			{
				rollTime = (totalTime - lastWaitTime) / waitNum;
			}
			else
			{
				rollTime = lastWaitTime / (slowDownScale * slowDownScale + slowDownScale);
				switch (curRollNum - waitNum )
				{
					case 1:
						//rollTime = rollTime;
						break;
					case 2:
						rollTime = rollTime * slowDownScale;
						break;
					case 3:
						rollTime = rollTime * slowDownScale * slowDownScale;
						break;
				}
				Debug.Log("rollTime" + rollTime);
			}

			curNum++;
			if(curNum == 10)
			{
				curNum = 0;
			}
			ChangeNum(rollTime);
			curRollNum++;
			yield return new WaitForSeconds(rollTime + 0.01f);
		}
	}

	public void ChangeNum(float time)
	{
		num1.transform.DOLocalMove(num1.transform.localPosition + new Vector3(0, num1.rectTransform.rect.height * scale), time);
		Tweener tweener =  num2.transform.DOLocalMove(num2.transform.localPosition + new Vector3(0, num2.rectTransform.rect.height * scale), time);

		tweener.onComplete = () =>
		{
			if ( curNum % 2 == 1)
			{
				num1.transform.localPosition = pos2;
			}
			else if (curNum % 2 == 0)
			{
				num2.transform.localPosition = pos2;
			}
			SetNum();

		};
		
	}
}
