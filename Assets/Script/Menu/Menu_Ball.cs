using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Ball : MonoBehaviour
{
	[SerializeField]float rotationSpeed = 100f;
	bool dragging = false;
	private float damp = 2;
	Rigidbody rb;

	private Quaternion defaultRotation;
	void Start()
    {
        rb = GetComponent<Rigidbody>();
		defaultRotation = transform.rotation;
	}

	 void OnMouseDrag()
	{
		dragging = true;
	}

	// Update is called once per frame
	void Update()
    {

		if (Input.GetMouseButtonUp(0)) 
		{
			dragging = false;
		}
    }

	 void FixedUpdate()
	{
		Quaternion TempRotation =transform.rotation;

		if (dragging) 
		{
			float x = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
			float y = Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;
			rb.AddTorque(Vector3.back * x);
			rb.AddTorque(Vector3.left * y);


		}
		if (!dragging)
		{
			//transform.eulerAngles = new Vector3(transform.eulerAngles.x+1f, transform.eulerAngles.y, transform.eulerAngles.z);

			//transform.rotation = Quaternion.Euler(transform.eulerAngles.x + 1f, 0,0);
			

			//transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, Time.deltaTime * damp);
		}

		//if (TempRotation.y < -90f && time < 1)
		//{
		//	time += Time.deltaTime / 2;
		//}
		//if (TempRotation.y > -90f && time > 0)
		//{
		//	time += Time.deltaTime / 2;
		//}

		//TempRotation.y = Mathf.Lerp(TempRotation.y, -90, time);

		//transform.eulerAngles = TempRotation;
	}
}
