using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;


public class TimerGoNum : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI num1, num2, num3, curTextNum, otherTextNum;

	TextMeshProUGUI waitTextNum
	{
		get
		{
			if (curTextNum == num1)
			{
				if(otherTextNum == num2)
				{
					return num3;
				}
				else
				{
					return num2;
				}
			}
			else if(curTextNum == num2)
			{
				if(otherTextNum == num3)
				{
					return num1;
				}
				else
				{
					return num3;
				}
			}
			else if (curTextNum == num3)
			{
				if(otherTextNum == num1)
				{
					return num2;
				}
				else
				{
					return num1;
				}
			}

			return null;
		}
	}

	public int curText = 1;
	public int curNum = -1;
	public int roundNum = 3;
	public int lastNum = 3;
	public float slowDownScale = 1.1f;
	public float lastWaitTime = 1f;

	public bool isUp = true;
	bool lastUp;


	Vector3 showPos, upPos, downPos;

	public delegate void ShowNextNum(int num);

	private void Awake()
	{
		showPos = num1.transform.localPosition;
		upPos = num2.transform.localPosition;
		downPos = num3.transform.localPosition;
	}

	public void SetBeginNum(int num)
	{
		curNum = num;

		num1.text = num.ToString();
		num2.text = calcNextPlus().ToString();
		num3.text = calcNextMinues().ToString();
		curTextNum = num1;
		otherTextNum = num2;
		isUp = true;
		lastUp = true;
	}

	public void SetNum()
	{
		int nextNum = 0;

		if(isUp)
		{
			nextNum = calcNextPlus();
			//if (curNum % 2 == 1)
			//{
			//	num2.text = curNum.ToString();
			//	num1.text = nextNum.ToString();
			//}
			//else
			//{
			//	num1.text = curNum.ToString();
			//	num2.text = nextNum.ToString();
			//}
			otherTextNum.text = nextNum.ToString();
			curTextNum.text = curNum.ToString();
			waitTextNum.text = calcNextMinues().ToString();
		}
		else
		{
			nextNum = calcNextMinues();
			//if (curNum % 2 == 1)
			//{
			//	num3.text = curNum.ToString();
			//	num1.text = nextNum.ToString();
			//}
			//else
			//{
			//	num1.text = curNum.ToString();
			//	num3.text = nextNum.ToString();
			//}

			otherTextNum.text = nextNum.ToString();
			curTextNum.text = curNum.ToString();
			waitTextNum.text = calcNextPlus().ToString();
		}
		

	}

	public int calcNextPlus()
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
		return nextNum;
	}

	public int calcNextMinues()
	{
		int nextNum = 0;
		if (curNum == 0)
		{
			nextNum = 9;
		}
		else
		{
			nextNum = curNum - 1;
		}
		return nextNum;
	}
	public void ChangeNum(float time)
	{
		if(isUp == true)
		{
			curTextNum.transform.DOLocalMove(curTextNum.transform.localPosition + new Vector3(0, curTextNum.rectTransform.rect.height), time);
			Tweener tweener = otherTextNum.transform.DOLocalMove(otherTextNum.transform.localPosition + new Vector3(0, otherTextNum.rectTransform.rect.height), time);

			tweener.onComplete = () =>
			{
				TextMeshProUGUI temp = curTextNum;
				curTextNum = otherTextNum;
				otherTextNum = temp;
				otherTextNum.transform.localPosition = upPos;
				SetNum();

			};
		}
		else
		{
			curTextNum.transform.DOLocalMove(curTextNum.transform.localPosition - new Vector3(0, curTextNum.rectTransform.rect.height), time);
			Tweener tweener = otherTextNum.transform.DOLocalMove(otherTextNum.transform.localPosition - new Vector3(0, otherTextNum.rectTransform.rect.height), time);

			tweener.onComplete = () =>
			{
				TextMeshProUGUI temp = curTextNum;
				curTextNum = otherTextNum;
				otherTextNum = temp;
				otherTextNum.transform.localPosition = downPos;

				SetNum();

			};
		}
		

	}

	public void test(int nextNum)
	{
		//BeginChange(nextNum, false);
	}

	public void BeginChange(int nextNum, float time, bool up = true)
	{
		if (nextNum == curNum)
		{
			return;
		}
		if(curNum == -1)
		{
			num1.text = "";
			num2.text = nextNum.ToString();
			num3.text = nextNum.ToString();
			curTextNum = num1;
			isUp = up;
			lastUp = up;
			if(isUp)
			{
				otherTextNum = num2;
			}
			else
			{
				otherTextNum = num3;
			}
		}
		else
		{
			if (curNum > nextNum)
			{
				if (curNum == 9 && nextNum == 0)
				{
					isUp = true;
				}
				else
				{
					isUp = false;
				}
			}
			else
			{
				if (curNum == 0 && nextNum == 9)
				{
					isUp = false;
				}
				else
				{
					isUp = true;
				}
			}
		}
		if(lastUp != isUp)
		{
			otherTextNum = waitTextNum;
			lastUp = isUp;
		}

		curNum = nextNum;


		ChangeNum(time);
	}
}
