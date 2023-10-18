using RengeGames.HealthBars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD_Health : MonoBehaviour
{

	[SerializeField] private RadialSegmentedHealthBar Healthbar;
	[SerializeField] private PlayerController player;

	private Vector4 FullHealthBorderColor;
	private Vector4 NormalBorderColor;
	private Vector4 HealthrColor;

	private Vector3 BarRotation;


	private float R_Speed = 0.05f;


	void Start()
	{
		FullHealthBorderColor = Healthbar.BorderColor.Value;
		NormalBorderColor = new Vector4(20f / 255f, 20f / 255f, 20f / 255f, 1f);
		HealthrColor = Healthbar.InnerColor.Value;

		BarRotation = Healthbar.transform.eulerAngles;



	}

	void ControlHealth()
	{

		if (Input.GetKeyDown(KeyCode.F1))
		{
			player.CurrentHealth++;
		}
		if (Input.GetKeyDown(KeyCode.F2))
		{
			Healthdecrase();
			player.CurrentHealth--;

		}

	}

	void Healthincrese()
	{

	}


	void Healthdecrase()
	{
		//if(player.CurrentHealth == player.MaxHealth)
		{
			BarRotation = Healthbar.transform.eulerAngles;
			BarRotation.y = 360;
		}
	}


    // Update is called once per frame
    void Update()
    {
        Vector3 position = player.transform.position;
		this.transform.position = position;

		ControlHealth();

		Healthbar.transform.eulerAngles = Vector3.Lerp(Healthbar.transform.eulerAngles, BarRotation, R_Speed);

		if(Healthbar.transform.eulerAngles.y > 359.8)
		{

			BarRotation.y = 0;
			Healthbar.transform.eulerAngles = BarRotation;
		}


	}
}
