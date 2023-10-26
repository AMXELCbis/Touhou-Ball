using RengeGames.HealthBars;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System;

public class HUD_Health : MonoBehaviour
{

	[SerializeField] private RadialSegmentedHealthBar Healthbar;
	[SerializeField] private PlayerController player;
	[SerializeField] public Volume m_Volume;

	private Vector4 FullHealthBorderColor;
	private Vector4 NormalBorderColor;
	private Vector4 HealthColor;

	private Vector4 BloomGreenColor;
	private Vector4 BloomRedColor;


	Bloom BloomCompent;
	[SerializeField] private float BloomIntensity;

	Vignette VignetteCompent;
	private float VignetteIntensity;

	private Vector4 T_HealthColor;
	private Vector4 T_BorderColor;

	private Vector3 BarRotation;

	private Vector4 HealthColor_Target;
	private Vector4 HealthColor_N1;
	private Vector4 HealthColor_N2;
	private Vector4 HealthColor_N3;


	[SerializeField]
	private float R_Speed = 0.05f;
	[SerializeField]
	private float Bloom_Speed = 0.05f;
	[SerializeField]
	private float V_Speed = 0.05f;

	[SerializeField] private AudioClip HealSFX_Normal;
	[SerializeField] private AudioClip HealSFX_Max;
	[SerializeField] private List<AudioClip> HitSFX;
	[SerializeField] private AudioSource audiosource;


	void Start()
	{
		FullHealthBorderColor = new Vector4(1,1,1, 1f);
		NormalBorderColor = new Vector4(20f / 255f, 20f / 255f, 20f / 255f, 1f);
		HealthColor = new Vector4(51f / 255f, 238f / 255f, 108f / 255f, 1f);

		HealthColor_N1 = HealthColor;
		HealthColor_N2 = new Vector4(198f / 255f, 238f / 255f, 51f / 255f, 1f);
		HealthColor_N3 = new Vector4(238f / 255f, 51f / 255f, 61f / 255f, 1f);

		BloomGreenColor = new Vector4(0, 1, 0, 1f);
		BloomRedColor = new Vector4(1, 0, 0, 1f);

		T_HealthColor = HealthColor;
		HealthColor_Target = HealthColor;
		T_BorderColor = FullHealthBorderColor;

		BarRotation = Healthbar.transform.eulerAngles;

		m_Volume.profile.TryGet<Bloom>(out BloomCompent);
		m_Volume.profile.TryGet<Vignette>(out VignetteCompent);
		VignetteIntensity = 0;

	}

	void ControlHealth()
	{

		if (Input.GetKeyDown(KeyCode.F1) && player.CurrentHealth<player.MaxHealth)
		{
			player.CurrentHealth++;

			if(player.CurrentHealth != player.MaxHealth)
				audiosource.PlayOneShot(HealSFX_Normal);
			else
				audiosource.PlayOneShot(HealSFX_Max);

			Healthincrese();
			BloomCompent.tint.value = BloomGreenColor;
			BloomCompent.intensity.value = BloomIntensity;

			ChangeHealthBarColor();

			float Num = (float)player.CurrentHealth / (float)player.MaxHealth;

			//for disable low health effect
			if (Num > 0.35)
			{
				VignetteIntensity = 0;
			}


		}
		if (Input.GetKeyDown(KeyCode.F2) && player.CurrentHealth > 0)
		{
			AudioClip clip = HitSFX[UnityEngine.Random.Range(0, HitSFX.Count)];
			audiosource.PlayOneShot(clip);

			Healthdecrase();
			player.CurrentHealth--;
			BloomCompent.tint.value = BloomRedColor;
			BloomCompent.intensity.value = BloomIntensity;

			ChangeHealthBarColor();

			float Num = (float)player.CurrentHealth / (float)player.MaxHealth;

			//for disable low health effect
			if (Num < 0.35)
			{
				VignetteIntensity = 0.5f;
			}


		}

	}

	void ChangeHealthBarColor()
	{
		float Num = (float)player.CurrentHealth / (float)player.MaxHealth;

		if (Num > 0.99) 
		{
			HealthColor_Target = HealthColor_N1;
		}
		else if(Num > 0.6)
			HealthColor_Target = HealthColor_N2;
		else if (Num > 0.3)
			HealthColor_Target = HealthColor_N3;

	}

	void Healthincrese()
	{
		float Num = (float)player.CurrentHealth / (float)player.MaxHealth;

		if (player.CurrentHealth == player.MaxHealth)
		{
			T_BorderColor = FullHealthBorderColor;
		}

		//rotate the bar
		BarRotation = Healthbar.transform.eulerAngles;
		BarRotation.y = 359.99f;
		Healthbar.transform.eulerAngles = BarRotation;
		BarRotation.y = 0;


	}


	void Healthdecrase()
	{
		float Num = (float)player.CurrentHealth / (float)player.MaxHealth;

		if (player.CurrentHealth == player.MaxHealth)
		{
			T_BorderColor = NormalBorderColor;
		}


		//rotate the bar
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

		//for bloom effect when increse/decrese health
		BloomCompent.intensity.value = Mathf.Lerp(0, BloomCompent.intensity.value, Bloom_Speed);

		//for low health effect
		VignetteCompent.intensity.value = Mathf.Lerp(VignetteCompent.intensity.value, VignetteIntensity, V_Speed);

		//health bar color changing
		Healthbar.InnerColor.Value = Vector4.Lerp(Healthbar.InnerColor.Value, HealthColor_Target, R_Speed);


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

		if(player.CurrentHealth == 0) 
		{
			//reborn and reset all values
			LevelManager.instance.Reborn(player);
			player.CurrentHealth = player.MaxHealth;
			Start();
		}

	}
}
