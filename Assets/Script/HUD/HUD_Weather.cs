using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Weather : MonoBehaviour
{
	[SerializeField] private GameObject Scale_Rotate_Point;
	[SerializeField] private GameObject Rotate_Point;
	[SerializeField] private GameObject Dail;
	[SerializeField] private LevelManager levelManager;

	[SerializeField] private Image Background;
	[SerializeField] private Image InnerBackground;
	[SerializeField] private Color ClearColor;
	[SerializeField] private Color WindColor;
	[SerializeField] private Color RainColor;
	[SerializeField] private Color SnowColor;
	private Color T_Color;
	[SerializeField] private float C_Speed;

	private Vector3 OriginalDialScale;
	private Vector3 T_DialScale;
	[SerializeField]
	private float S_Speed = 0.05f;
	[SerializeField]
	private float ScaleSize = 2f;


	private Vector3 P_Rotation;
	private Vector3 P_ScaleRotation;

	[SerializeField]
	private float R_Speed = 0.05f;


	void Start()
    {
		P_Rotation = Rotate_Point.transform.localEulerAngles;
		P_ScaleRotation = Scale_Rotate_Point.transform.localEulerAngles;

		OriginalDialScale = Dail.transform.localScale;
		T_DialScale = OriginalDialScale;

		T_Color = ClearColor;
	}

	void ChangeWeather()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			levelManager.weather = Weather.clear;
			Dail.transform.localScale = OriginalDialScale * ScaleSize;
			Scale_Rotate_Point.transform.localScale = OriginalDialScale * ScaleSize;

		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			levelManager.weather = Weather.wind;
			Dail.transform.localScale = OriginalDialScale * ScaleSize;
			Scale_Rotate_Point.transform.localScale = OriginalDialScale * ScaleSize;


		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			levelManager.weather = Weather.rain;
			Dail.transform.localScale = OriginalDialScale * ScaleSize;
			Scale_Rotate_Point.transform.localScale = OriginalDialScale * ScaleSize;



		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			levelManager.weather = Weather.snow;
			Dail.transform.localScale = OriginalDialScale * ScaleSize;
			Scale_Rotate_Point.transform.localScale = OriginalDialScale * ScaleSize;



		}
	}

	void CheakWeather()
	{
		//give me a same switch function as below but also set the P_ScaleRotation.z as it set P_Rotation.z

		//function below is to set the P_Rotation.z



		switch (levelManager.weather)
		{
			case Weather.clear:
				T_Color = ClearColor;

				if (Rotate_Point.transform.localEulerAngles.z >180)
					P_Rotation.z = 360;
				else
					P_Rotation.z = 0;
				break;
			case Weather.wind:
				T_Color = WindColor;

				P_Rotation.z = 90;
				break;
			case Weather.rain:
				T_Color = RainColor;

				P_Rotation.z = 180;
				break;
			case Weather.snow:
				T_Color = SnowColor;

				P_Rotation.z = 270;
				break;

		}
	}

    // Update is called once per frame
    void Update()
    {
		ChangeWeather();
		CheakWeather();

		//rotate the point
		Rotate_Point.transform.localEulerAngles = Vector3.Lerp(Rotate_Point.transform.localEulerAngles, P_Rotation, R_Speed);

		Vector3 temp = P_ScaleRotation;
		temp.z = P_Rotation.z;

		Scale_Rotate_Point.transform.localEulerAngles = Vector3.Lerp(Scale_Rotate_Point.transform.localEulerAngles, temp, R_Speed);


		//Background color changing
		Background.color = Vector4.Lerp(Background.color, T_Color, C_Speed);
		InnerBackground.color = Vector4.Lerp(Background.color, T_Color, C_Speed);
		//resize the dial
		Dail.transform.localScale = Vector3.Lerp(Dail.transform.localScale, T_DialScale, S_Speed);
		Scale_Rotate_Point.transform.localScale = Vector3.Lerp(Scale_Rotate_Point.transform.localScale, T_DialScale, S_Speed);

		//Fix rotation
		if (Rotate_Point.transform.localEulerAngles.z > 359.999 && P_Rotation.z == 360)
		{
			P_Rotation.z = 0;
			Rotate_Point.transform.localEulerAngles = P_Rotation;
		}

		//same function above but use Scale_Rotate_Point
		if (Scale_Rotate_Point.transform.localEulerAngles.z > 359.999 && P_Rotation.z == 360)
		{
			P_Rotation.z = 0;
			Scale_Rotate_Point.transform.localEulerAngles = P_Rotation;
		}

	}
}
