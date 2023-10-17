using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Michsky.MUIP;

public class HUD_Loudness : MonoBehaviour
{
	// Start is called before the first frame update
	public CameraController cameraController;
	public UnityEngine.UI.Image Background;
	public UnityEngine.UI.Image Bar;
	public TextMeshProUGUI Text;
	[SerializeField] private Michsky.MUIP.ProgressBar myBar;
	[SerializeField] private Rigidbody player;
	[SerializeField] private PlayerController Controller;
	

	private float A_Background;
	private float A_Text;
	private float A_InitBar;

	private float A_Speed = 0.05f;
	private float A_FastSpeed = 0.7f;
	private float MaxSpeed = 0;
	private float CurrentSpeed = 0;

	void Start()
	{
		MaxSpeed = Controller.maxSpeed;
		A_Text = 0.7f;
		A_Background = (float)15 / 255;
		A_InitBar = (float)132 / 255;
	}

	void checkA()
	{
		if (!cameraController.isUpPoint)
		{
			Vector4 temp = Background.color;
			temp.w = 0;
			Background.color = Vector4.Lerp(Background.color, temp, A_Speed);
			Text.color = Vector4.Lerp(Text.color, temp, A_Speed);
			temp = Bar.color;
			temp.w = 0;
			Bar.color = Vector4.Lerp(Bar.color, temp, A_Speed);
		}
		else
		{
			Vector4 temp = Background.color;
			temp.w = A_Background;
			Background.color = Vector4.Lerp(Background.color, temp, A_Speed);
			temp.w = A_Text;
			Text.color = Vector4.Lerp(Text.color, temp, A_Speed);
			temp = Bar.color;
			temp.w = A_InitBar;
			Bar.color = Vector4.Lerp(Bar.color, temp, A_FastSpeed);

		}

	}


	void Update()
	{
		checkA();

		CurrentSpeed = player.velocity.magnitude;

		myBar.currentPercent = CurrentSpeed / MaxSpeed * 100;

		//P is 0 ~ 100
		float P = myBar.currentPercent;

		//2 parts of percent, 0~50 and 51 ~ 100, one is for R (Vec4.x)
		//another for G (Vec4.y)

		if(P < 50)
		{
			float FirstP = P / 50f;

			Vector4 temp = Bar.color;
			temp.x = FirstP;
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
