using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Timer : MonoBehaviour
{
	[SerializeField] private float Sec;
	[SerializeField] private int Min;
	[SerializeField] private GameObject Left;
	[SerializeField] private GameObject Right;

	[SerializeField] private Vector3 Left_Rotation;
	[SerializeField] private Vector3 Right_Rotation;

	[SerializeField] private TimerText Sec_Text;

	[SerializeField] private float R_Speed;


	// Start is called before the first frame update


	void OneSecond()
	{
		Sec--;
		if (Sec < 0)
			Sec = 59;

	}

	void StartRotate()
	{
		if(Sec < 10 && Sec > 0)
			Right_Rotation.z += 120f;

	}

	void Start()
    {
		Sec_Text.SetBeginNum((int)Sec);
		InvokeRepeating("OneSecond", .01f, 1.0f);
		InvokeRepeating("StartRotate", .01f, 1.0f);



		Left_Rotation = Left.transform.localEulerAngles;
		Right_Rotation = Right.transform.localEulerAngles;
	}
	
	void ChangeTimer()
	{
		//Min
		if (Input.GetKeyDown(KeyCode.Keypad4))
		{
			Left_Rotation.z = 360f;
		}
		//Sec
		if (Input.GetKeyDown(KeyCode.Keypad5))
		{
			Right_Rotation.z = 360f;
		}
	}



	void RotationAndFixRoataion()
	{
		//Lerp to rotate the Left and Right using Rotation Point
		Left.transform.localEulerAngles = Vector3.Lerp(Left.transform.localEulerAngles, Left_Rotation, R_Speed);
		Right.transform.localEulerAngles = Vector3.Lerp(Right.transform.localEulerAngles, Right_Rotation, R_Speed);

		if (Left.transform.localEulerAngles.z > 359.99 && Left_Rotation.z == 360)
		{
			Left_Rotation.z = 0;
			Left.transform.localEulerAngles = Left_Rotation;
		}
		if (Right.transform.localEulerAngles.z > 359.99 && Right_Rotation.z == 360)
		{
			Right_Rotation.z = 0;
			Right.transform.localEulerAngles = Right_Rotation;
		}


	}

	void Update()
    {
		ChangeTimer();
		RotationAndFixRoataion();



		//if (Sec >= 0)
		//{
		//	Sec -= Time.deltaTime;
		//}
		//else
		//{
		//	Sec = 59;
		//}


		if (Sec > 9)
		{
			Sec_Text.StartRoll((int)Sec);
		}
		else
		{

		}

	}
}
