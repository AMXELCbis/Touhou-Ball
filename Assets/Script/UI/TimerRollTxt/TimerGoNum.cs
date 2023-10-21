using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;


public class TimerGoNum : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI num1, num2, num3;

	public int curText = 1;
	public int curNum;
	public int roundNum = 3;
	public int lastNum = 3;
	public float slowDownScale = 1.1f;
	public float lastWaitTime = 1f;

	public bool isUp = true;

	Vector3 showPos, upPos, downPos;

	private void Awake()
	{
		showPos = num1.transform.localPosition;
		upPos = num2.transform.localPosition;
		downPos = num3.transform.localPosition;
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

	public void ChangeNum(float time)
	{
		num1.transform.DOLocalMove(num1.transform.localPosition + new Vector3(0, num1.rectTransform.rect.height), time);
		Tweener tweener = num2.transform.DOLocalMove(num2.transform.localPosition + new Vector3(0, num2.rectTransform.rect.height), time);

		tweener.onComplete = () =>
		{
			if (curNum % 2 == 1)
			{
				num1.transform.localPosition = upPos;
			}
			else if (curNum % 2 == 0)
			{
				num2.transform.localPosition = upPos;
			}
			//SetNum();

		};

	}
}
