using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using UnityEngine.UIElements;

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
    private bool isUpPoint = false;

	private Transform _targetTrans
    {
        get 
        { 
            if (isUpPoint)
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
    }
    private void FixedUpdate()
    {
        //float distance = 1 / ((_camera.position - _targetTrans.position).magnitude);
        //_camera.position = Vector3.Lerp(_camera.position, _targetTrans.position, distance * moveSpeed);
        _camera.position = Vector3.Lerp(_camera.position, _targetTrans.position, moveSpeed);
        //float angle = 1 / ((_camera.localEulerAngles - _targetTrans.localEulerAngles).magnitude);
        //_camera.transform.localEulerAngles = Vector3.Lerp(_camera.localEulerAngles, _targetTrans.localEulerAngles, angle * rotateSpeed);
        _camera.transform.localEulerAngles = Vector3.Lerp(_camera.localEulerAngles, _targetTrans.localEulerAngles, rotateSpeed);
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
	}
}