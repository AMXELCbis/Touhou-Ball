using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
	[SerializeField] private GameObject Rotation_Point;
	[SerializeField] private LevelManager levelManager;
	[SerializeField] private Animator AnimatorNum;
	[SerializeField] private TextMeshProUGUI Text;
	[SerializeField] private GameObject Max;
	[SerializeField] private GameObject Last;

	[SerializeField] private float R_Speed;
	private Vector3 P_Rotation;
	private bool ClockwiseRotate = true;


	void Start()
    {
		Text.text = LevelManager.instance.PlayerLife.ToString();

		P_Rotation = Rotation_Point.transform.localEulerAngles;

	}

	public void IncreaseNum()
	{
		ClockwiseRotate = false;

		LevelManager.instance.PlayerLife++;

		P_Rotation.z = 360;

	}

	public void DecreaseNum()
	{
		//set bool for rotation direction
		ClockwiseRotate = true;

		LevelManager.instance.PlayerLife--;

		P_Rotation.z = (float)359.99;
		Rotation_Point.transform.localEulerAngles = P_Rotation;
		P_Rotation.z = 0;
	}

	public void ChangeLifeNum()
	{
		int Cur_LifeNum = LevelManager.instance.PlayerLife;
		int MaxLifeNum = LevelManager.instance.MaxPlayerLife;

		//increse
		if (Input.GetKeyDown(KeyCode.Keypad1) && Cur_LifeNum < MaxLifeNum && Rotation_Point.transform.localEulerAngles.z ==0)
		{
			IncreaseNum();
		}
		//decrese
		else if (Input.GetKeyDown(KeyCode.Keypad2) && Cur_LifeNum > 0 && Rotation_Point.transform.localEulerAngles.z == 0)
		{
			DecreaseNum();
		}

	}

	void FixRotation()
	{
		if (ClockwiseRotate)
		{
			if (Rotation_Point.transform.localEulerAngles.z < 0.01 && P_Rotation.z == 0)
			{
				P_Rotation.z = 0;
				Rotation_Point.transform.localEulerAngles = P_Rotation;
			}

		}
		else
		{
			if (Rotation_Point.transform.localEulerAngles.z > 359.99 && P_Rotation.z == 360)
			{
				P_Rotation.z = 0;
				Rotation_Point.transform.localEulerAngles = P_Rotation;
			}


		}
	}

	void ChangeText()
	{
		if (Rotation_Point.transform.localEulerAngles.z < 180 && Rotation_Point.transform.localEulerAngles.z > 90)
		{
			Text.text = LevelManager.instance.PlayerLife.ToString();

			if (LevelManager.instance.PlayerLife == LevelManager.instance.MaxPlayerLife)
				Max.SetActive(true);
			else
				Max.SetActive(false);



			if (LevelManager.instance.PlayerLife == 1)
			{
				Last.SetActive(true);
				AnimatorNum.SetBool("Low", true);
			}
			else
			{
				Last.SetActive(false);
				AnimatorNum.SetBool("Low", false);

			}



		}

	}


	void Update()
    {
		ChangeLifeNum();

		//Lerp to rotate the Num using Rotation Point
		Rotation_Point.transform.localEulerAngles = Vector3.Lerp(Rotation_Point.transform.localEulerAngles, P_Rotation, R_Speed);

		FixRotation();
		ChangeText();


		//if (PlayerLifeText != null)
		//{
		//    PlayerLifeText.text = "X " + LevelManager.instance.PlayerLife;
		//}
	}
}
