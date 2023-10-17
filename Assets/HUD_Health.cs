using RengeGames.HealthBars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Health : MonoBehaviour
{

	[SerializeField] private RadialSegmentedHealthBar Healthbar;
	[SerializeField] private GameObject player;

	// Start is called before the first frame update
	void Start()
    {
		//Healthbar.

	}

    // Update is called once per frame
    void Update()
    {
        Vector3 position = player.transform.position;

		this.transform.position = position;

	}
}
