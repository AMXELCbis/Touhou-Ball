using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour {

	// store a public reference to the Player game object, so we can refer to it's Transform
	public GameObject player;

	// Store a Vector3 offset from the player (a distance to place the camera from the player at all times)
	private Vector3 offset;
    [SerializeField]
    private Animator cameraRotateAnim;
    [SerializeField]
    private Transform _downPoint; //落下镜头方位
    [SerializeField]
    private Transform _upPoint; //抬起镜头方位
    [SerializeField]
	private Transform _camera; //角色摄像机
	[SerializeField]
	private Transform _zoom; //pause screen position, stand for Ispasued
	[SerializeField]
	public Volume m_Volume; //G_Volume
	[SerializeField]

	private bool isUpPoint = false;
	private bool isZoom = false;
	private bool isOption = false;
	private float time = 0.1f;

	DepthOfField dofComponent;
	int PauseButtonPosition = 0;

	private Transform _targetTrans
    {
        get 
        {
			if (isZoom)
			{
				return _zoom;
			}

			if (isUpPoint && !isZoom)
            {
                return _upPoint;
            }

            return _downPoint; 
        }
    }//当前目标点
    [SerializeField]
    private float moveSpeed = 0.05f;   ///镜头升降移动速度
    [SerializeField]
    private float rotateSpeed = 0.05f;  ///镜头升降旋转速度
    // At the start of the game..
    void Start ()
	{
		// Create an offset by subtracting the Camera's position from the player's position
		offset = transform.position - player.transform.position;
		//get depth volume for pause screen


		m_Volume.profile.TryGet<DepthOfField>(out dofComponent);

	}

	// After the standard 'Update()' loop runs, and just before each frame is rendered..
	void LateUpdate ()
	{
		// Set the position of the Camera (the game object this script is attached to)
		// to the player's position, plus the offset amount

		transform.position = player.transform.position + offset;
	}

    private void Update()
    {
		CheckCameraUp();
		CheckCameraRotate();



		switch (PauseButtonPosition)
		{
			case 0:
				//2.68
				dofComponent.focusDistance.value = 2.68f;
				break;
			case 1:
				//2.18
				dofComponent.focusDistance.value = 2.18f;
				break;
			case 2:
				//1.89
				dofComponent.focusDistance.value = 1.89f;
				break;
			case 3:
				//1.66
				dofComponent.focusDistance.value = 1.66f;

				break;

		}

		_camera.position = Vector3.Lerp(_camera.position, _targetTrans.position, moveSpeed);
		_camera.transform.localEulerAngles = Vector3.Lerp(_camera.localEulerAngles, _targetTrans.localEulerAngles, rotateSpeed);

	}
	private void FixedUpdate()
    {
        //float distance = 1 / ((_camera.position - _targetTrans.position).magnitude);
        //_camera.position = Vector3.Lerp(_camera.position, _targetTrans.position, distance * moveSpeed);
       // _camera.position = Vector3.Lerp(_camera.position, _targetTrans.position, moveSpeed);
        //float angle = 1 / ((_camera.localEulerAngles - _targetTrans.localEulerAngles).magnitude);
        //_camera.transform.localEulerAngles = Vector3.Lerp(_camera.localEulerAngles, _targetTrans.localEulerAngles, angle * rotateSpeed);
       // _camera.transform.localEulerAngles = Vector3.Lerp(_camera.localEulerAngles, _targetTrans.localEulerAngles, rotateSpeed);


    }

    void CheckCameraUp()
	{

        if (Input.GetKeyDown(KeyCode.LeftShift))
		{
            isUpPoint = !isUpPoint;

        }
	}
	
	void CheckCameraRotate()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            cameraRotateAnim.SetTrigger("Left");
		}
        else
		{
            cameraRotateAnim.ResetTrigger("Left");

        }
		
		if(Input.GetKeyDown(KeyCode.E))
        {
            cameraRotateAnim.SetTrigger("Right");
        }
		else
		{
            cameraRotateAnim.ResetTrigger("Right");
        }
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PauseResumeTheGame();
		}

	}

	public void ButtonOption_GoOption()
	{
		isOption = !isOption;
		cameraRotateAnim.SetBool("Option", isOption);

		if(isOption)
		{
			dofComponent.focalLength.value = 128;
		}
		else
		{
			dofComponent.focalLength.value = 300;

		}
	}

	private void PauseResumeTheGame()
	{
		isZoom = !isZoom;


		if (isZoom)
		{
			Time.timeScale = 0;
			cameraRotateAnim.SetBool("Pause", true);
			dofComponent.focalLength.value = 300;

		}
		else if (!isZoom)
		{
			Time.timeScale = 1;
			cameraRotateAnim.SetBool("Pause", false);
			dofComponent.focalLength.value = 1;

		}
	}


	public void ButtonResume_ResumeGame()
	{
		PauseResumeTheGame();
	}



	public void ButtonResume()
	{
		PauseButtonPosition = 0;
	}
	public void ButtonOption()
	{
		PauseButtonPosition = 1;
	}
	public void ButtonMenu()
	{
		PauseButtonPosition = 2;
	}
	public void ButtonQuit()
	{
		PauseButtonPosition = 3;
	}
}
