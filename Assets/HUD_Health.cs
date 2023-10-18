using RengeGames.HealthBars;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD_Health : MonoBehaviour
{

	[SerializeField] private RadialSegmentedHealthBar Healthbar;
	[SerializeField] private PlayerController player;

	private Vector4 FullHealthBorderColor;
	private Vector4 NormalBorderColor;
	private Vector4 HealthColor;

	private Vector4 T_HealthColor;
	private Vector4 T_BorderColor;

	private Vector3 BarRotation;


	private float R_Speed = 0.05f;


	void Start()
	{
		FullHealthBorderColor = Healthbar.BorderColor.Value;
		NormalBorderColor = new Vector4(20f / 255f, 20f / 255f, 20f / 255f, 1f);
		HealthColor = Healthbar.InnerColor.Value;

		T_HealthColor = HealthColor;
		T_BorderColor = FullHealthBorderColor;

		BarRotation = Healthbar.transform.eulerAngles;



	}

	void ControlHealth()
	{

		if (Input.GetKeyDown(KeyCode.F1))
		{
			player.CurrentHealth++;
			Healthincrese();
		}
		if (Input.GetKeyDown(KeyCode.F2))
		{
			Healthdecrase();
			player.CurrentHealth--;

		}

	}

	void Healthincrese()
	{
		if (player.CurrentHealth == player.MaxHealth)
		{
			T_BorderColor = FullHealthBorderColor;
		}
		BarRotation = Healthbar.transform.eulerAngles;
		BarRotation.y = 359.99f;
		Healthbar.transform.eulerAngles = BarRotation;
		BarRotation.y = 0;

	}


	void Healthdecrase()
	{
		if(player.CurrentHealth == player.MaxHealth)
		{
			T_BorderColor = NormalBorderColor;
		}
		BarRotation = Healthbar.transform.eulerAngles;
		BarRotation.y = 0.01f;
		Healthbar.transform.eulerAngles = BarRotation;
		BarRotation.y = 360;
	}


    // Update is called once per frame
    void Update()
    {
        Vector3 position = player.transform.position;
		this.transform.position = position;

		ControlHealth();

		Healthbar.transform.eulerAngles = Vector3.Lerp(Healthbar.transform.eulerAngles, BarRotation, R_Speed);
		Healthbar.RemoveSegments.Value = Mathf.Lerp(Healthbar.RemoveSegments.Value, player.MaxHealth - player.CurrentHealth, R_Speed);

		Healthbar.BorderColor.Value = Vector4.Lerp(Healthbar.BorderColor.Value, T_BorderColor, R_Speed);

		//Fix rotation
		if (Healthbar.transform.eulerAngles.y > 359.8 && BarRotation.y == 360)
		{
			BarRotation.y = 0;
			Healthbar.transform.eulerAngles = BarRotation;
		}
		else if (Healthbar.transform.eulerAngles.y < 0.2 && BarRotation.y == 0)
		{
			BarRotation.y = 0;
			Healthbar.transform.eulerAngles = BarRotation;

		}


	}
}
