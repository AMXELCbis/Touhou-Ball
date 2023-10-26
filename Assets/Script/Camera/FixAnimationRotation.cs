using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAnimationRotation : MonoBehaviour
{
	[SerializeField] private CameraController cameraController;

	void SetLeftFalse()
	{
		cameraController.cameraRotateAnim.SetBool("Left", false);	
	}

	// Start is called before the first frame update
	void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
