using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Michsky.MUIP;
using System;
using Unity.Mathematics;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class HUD_Loudness : MonoBehaviour
{
	// Start is called before the first frame update
	public CameraController cameraController;
	public UnityEngine.UI.Image Background;
	public UnityEngine.UI.Image Bar;
	public SpriteRenderer Speaker;
	[SerializeField] private Michsky.MUIP.ProgressBar myBar;
	[SerializeField] private Rigidbody player;
	[SerializeField] private PlayerController Controller;
	[SerializeField] private List<Sprite> Speakersprite;
	[SerializeField] public Volume m_Volume;


	private float A_Background;
	private float A_Speaker;
	private float A_InitBar;

	MotionBlur MoBCompent;
	[SerializeField] public float InitMoB;


	private Vector3 P_Speaker;

	private float A_Speed = 0.05f;
	private float A_FastSpeed = 0.7f;
	private float MaxSpeed = 0;
	private float CurrentSpeed = 0;

	void Start()
	{
		MaxSpeed = Controller.maxSpeed;
		A_Speaker = 0.8f;
		A_Background = (float)15 / 255;
		A_InitBar = (float)132 / 255;

		P_Speaker = Speaker.transform.position;


		m_Volume.profile.TryGet<MotionBlur>(out MoBCompent);

		//MotionBlur_Volume = Volume
	}

	void checkA()
	{
		if (!cameraController.isUpPoint)
		{
			Vector4 temp = Background.color;
			temp.w = 0;
			Background.color = Vector4.Lerp(Background.color, temp, A_Speed);
			Speaker.color = Vector4.Lerp(Speaker.color, temp, A_Speed);
			temp = Bar.color;
			temp.w = 0;
			Bar.color = Vector4.Lerp(Bar.color, temp, A_Speed);
		}
		else
		{
			Vector4 temp = Background.color;
			temp.w = A_Background;
			Background.color = Vector4.Lerp(Background.color, temp, A_Speed);
			temp.w = A_Speaker;
			Speaker.color = Vector4.Lerp(Speaker.color, temp, A_Speed);
			temp = Bar.color;
			temp.w = A_InitBar;
			Bar.color = Vector4.Lerp(Bar.color, temp, A_FastSpeed);

		}

	}


	void checkSpeaker()
	{
		if (myBar.currentPercent < 10)
		{
			Speaker.sprite = Speakersprite[0];
		}
		else if (myBar.currentPercent < 45)
		{
			Speaker.sprite = Speakersprite[1];
		}
		else if (myBar.currentPercent < 75)
		{
			Speaker.sprite = Speakersprite[2];
		}
		else if (myBar.currentPercent < 100)
		{
			Speaker.sprite = Speakersprite[3];
		}

	}

	void checkMontionBlur()
	{
		MoBCompent.intensity.value = (myBar.currentPercent / 100f)/2;

		if (MoBCompent.intensity.value < InitMoB)
			MoBCompent.intensity.value = InitMoB;
	}

	void Update()
	{
		checkA();

		CurrentSpeed = player.velocity.magnitude;

		myBar.currentPercent = CurrentSpeed / MaxSpeed * 100;

		checkSpeaker();

		if(cameraController.isUpPoint)
		{
			checkMontionBlur();
		}
		else
		{
			if(MoBCompent.intensity.value!= InitMoB)
				MoBCompent.intensity.value = InitMoB;
		}	

		//P is 0 ~ 100
		float P = myBar.currentPercent;

		//2 parts of percent, 0~50 and 51 ~ 100, one is for R (Vec4.x)
		//another for G (Vec4.y)

		if(P < 50)
		{
			float FirstP = P / 50f;

			Vector4 temp = Bar.color;
			temp.x = FirstP;
			temp.y = 1f;
			Bar.color = temp;

		}
		else
		{
			float SecondP = 1 - ((P-50f) / 25f);

			Vector4 temp = Bar.color;
			temp.y = SecondP;
			Bar.color = temp;

			//Bar.color = Vector4.Lerp(Bar.color, temp, A_FastSpeed);

		}







	}
}
