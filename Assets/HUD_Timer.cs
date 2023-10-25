using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class HUD_Timer : MonoBehaviour
{
	[SerializeField] public float Sec;
	[SerializeField] public int Min;
	[SerializeField] private GameObject Left;
	[SerializeField] private GameObject Right;

	[SerializeField] private Vector3 Left_Rotation;
	[SerializeField] private Vector3 Right_Rotation;


	[SerializeField] private GameObject Viewpoint;
	private Vector3 OrginalP_Viewpoint;


	[SerializeField] private TextMeshProUGUI Fake_Min_Num;

	[SerializeField] private TextMeshProUGUI Fake_R0_Num;
	[SerializeField] private TextMeshProUGUI Fake_R120_Num;
	[SerializeField] private TextMeshProUGUI Fake_R240_Num;
	[SerializeField] private GameObject Fake_R0;
	[SerializeField] private GameObject Fake_R120;
	[SerializeField] private GameObject Fake_R240;

	[SerializeField] private PlayerLife playerLife;

	private bool isShine_Left;
	private bool isShine_Right;
	[SerializeField] private Color Light_Red;
	[SerializeField] private Color Red;
	[SerializeField] private float C_Speed;
	private Color T_Color;


	public bool isTimeOver = false;

	[SerializeField] private TimerText Sec_Text;

	[SerializeField] private float R_Speed;


	// Start is called before the first frame update
	void setFake_R0Text()
	{
		Fake_R0_Num.text = Sec.ToString();
	}
	void setFake_R120Text()
	{
		Fake_R120_Num.text = Sec.ToString();
	}
	void setFake_R240Text()
	{
		Fake_R240_Num.text = Sec.ToString();
	}

	//function like above but setting the Fake_Min_Num text
	void setFake_MinText()
	{
		Fake_Min_Num.text = Min.ToString();
	}

	void Timer()
	{
		Sec--;
		if (Sec < 0)
		{
			if (Min > 0)
				Sec = 59;
			else //Time over
			{
				Sec = 0;
				setFake_R120Text();
				isTimeOver = true;
			}
			//Min-- but don't let Min less or equal than 0
			if (Min > 0)
				Min--;
		}

		if(Sec < 10)
		{

			// invoke function "setFakeNumText" delay 0.1f
			Invoke("setFake_R0Text", 0.1f);
			Invoke("setFake_R120Text", 0.1f);
			Invoke("setFake_R240Text", 0.1f);

		}

		switch (Sec)
		{
			case 10:
				Fake_R120.SetActive(true);
				break;
			case 8:
				Vector3 temp = OrginalP_Viewpoint;
				temp.y -= 1000; // temp move the origianl view point out of the timer
				Viewpoint.transform.localPosition = temp;
				Sec_Text.StartRoll(59);

				Fake_R0.SetActive(true);
				Fake_R240.SetActive(true);
				break;
			case 0:
				Fake_R0.SetActive(false);
				Fake_R240.SetActive(false);
				break;
			case 59:

				//temp
				//Sec is 59 and Min is 0, out of time. Enable TempRestart
				//playerLife.Temp_CheckRestart();

				Invoke("setFake_MinText", 0.1f);
				Viewpoint.transform.localPosition = OrginalP_Viewpoint;
				
				break;
		}



	}

	void StartRotate()
	{
		if (Sec < 10 && Sec >= 0 && !isTimeOver)
		{
			Right_Rotation.z += 120f;
		}
		else if (Sec == 59)
		{
			//not rotation, just to disable the left fake num
			Fake_R120.SetActive(false);


			Right_Rotation.z += 240f;
			Left_Rotation.z += 360f;
		}

	}

	void Start()
    {
		Sec_Text.SetBeginNum((int)Sec);
		InvokeRepeating("Timer", .01f, 1.0f);
		InvokeRepeating("StartRotate", .01f, 1.0f);

		isShine_Left = false;
		isShine_Right = false;

		Fake_Min_Num.text = Min.ToString();

		OrginalP_Viewpoint = Viewpoint.transform.localPosition;
		Left_Rotation = Left.transform.localEulerAngles;
		Right_Rotation = Right.transform.localEulerAngles;

		T_Color = Red;
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

		if (Left.transform.localEulerAngles.z > 359)
		{
			Left_Rotation.z = 0;
			Left.transform.localEulerAngles = Left_Rotation;
		}
		if (Right.transform.localEulerAngles.z > 359)
		{
			Right_Rotation.z = 0;
			Right.transform.localEulerAngles = Right_Rotation;
		}


	}

	void ChangeTextColor()
	{
		if (Min == 0)
		{
			//get Left Image component
			Image Left_Image = Left.GetComponent<Image>();
			Image Right_Image = Right.GetComponent<Image>();


			if (Sec < 30)
			{
				if (Right_Image.color.a != T_Color.a && isShine_Right == false)
				{
					T_Color = Red;
					isShine_Right = false;
				}
				else if (Right_Image.color.a == T_Color.a)
				{
					T_Color = Light_Red;
					isShine_Right = true;

				}

				Right_Image.color = Color.Lerp(Right_Image.color, T_Color, C_Speed * 3);

				//Fix color lerp
				if (Right_Image.color.a > 0.99 && isShine_Right == false)
				{
					Color temp = Right_Image.color;
					temp.a = 1;
					Right_Image.color = temp;
				}

				if (Right_Image.color.a < 0.205 && isShine_Right == true)
				{
					Right_Image.color = T_Color;
					isShine_Right = false;
					T_Color = Red;
				}


			}

			if (Left_Image.color.a != T_Color.a && isShine_Left == false)
			{
				T_Color = Red;
				isShine_Left = false;
			}
			else if (Left_Image.color.a == T_Color.a)
			{
				T_Color = Light_Red;
				isShine_Left = true;

			}

			Left_Image.color = Color.Lerp(Left_Image.color, T_Color, C_Speed*3);

			//Fix color lerp
			if (Left_Image.color.a > 0.99 && isShine_Left == false)
			{
				Color temp = Left_Image.color;
				temp.a = 1;
				Left_Image.color = temp;
			}

			if (Left_Image.color.a < 0.205 && isShine_Left == true)
			{
				Left_Image.color = T_Color;
				isShine_Left = false;
				T_Color = Red;
			}





		}


		//lerp Left_Image.color to T_Color but leave the alpha as 1


	}

	void Update()
    {
		ChangeTimer();
		RotationAndFixRoataion();

		if (Sec > 9)
		{
			Sec_Text.StartRoll((int)Sec);
		}

		ChangeTextColor();


	}
}
