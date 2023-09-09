using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;
using GameDefine;
using System.Collections;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour {

    // Create public variables for player speed, and for the Text UI game objects
    public float speed;
    public Text SpeedText;
	public float maxSpeed;
	public float maxSpeedX;
	public float maxSpeedZ;
	public bool checkRebornOnGround; //重生判断落地
	public float addForceScale = 1; //施加的移动力大小

    // Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
    public Rigidbody rb;
	public LayerMask layers;

	private int count;
	[SerializeField]
	private LayerMask groundCheck;
	[SerializeField]
	private Transform groundCheckPoint;
	[SerializeField]
	private PlayerStateCtrl playerState;
    [SerializeField]
    public Vector2 kazeSlowDown = Vector2.zero;
    public Vector2 kazeForce = Vector2.zero;
    private Vector3 curMovement = Vector3.zero;
	private SphereCollider sphereCollider;
	[SerializeField]
	float normalForceScale = 1, uTurnForceScale = 10;
	float rebornFxShowTime = 1;


	[SerializeField]
	GameObject rebornFX;
	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();
		sphereCollider = GetComponent<SphereCollider>();

    }

    // Each physics step..

	//Check the speed and the current direction, if not the same, increase the force
	//until current speed is equal to 0
    void Checkdirection(Vector3 movement, Vector3 currentSpeed, float AddedSpeed)
	{

		//x axis
		if ((currentSpeed.x > 0 && movement.x >=0 ) ||
            (currentSpeed.x < 0 && movement.x <= 0)) 
		{
            AddedSpeed = 1;
		}
		else
		{
           AddedSpeed = 50;
		}

        //z axis
        if ((currentSpeed.z > 0 && movement.z >= 0) ||
			(currentSpeed.z < 0 && movement.z <= 0))
        {
            AddedSpeed = 1;
        }
        else
        {
            AddedSpeed = 50;
        }

    }

    void FixedUpdate ()
	{
		// Set some local float variables equal to the value of our Horizontal and Vertical Inputs
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 currentSpeed = rb.velocity;
		//int addForce = 1;

		// Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
		curMovement = moveVertical * LevelManager.instance.curForward + moveHorizontal * LevelManager.instance.curRight;
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		// check if wsad get pressed or not
        if (curMovement != Vector3.zero) 
			Checkdirection(curMovement, currentSpeed, addForceScale);


        // Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
        // multiplying it by 'speed' - our public player speed that appears in the inspector

        Vector3 kazePower = new Vector3(kazeForce.x, 0, kazeForce.y);
        rb.AddForce(curMovement * speed * addForceScale + kazePower);

        //if (XZvelocity.magnitude > curMaxSpeed)
        //{
        //    XZvelocity = XZvelocity.normalized * curMaxSpeed;
        //}
        Vector2 XZvelocity = calcMoveSpeed();
        rb.velocity = new Vector3(XZvelocity.x, rb.velocity.y, XZvelocity.y);

        // display speed
        if (SpeedText != null )
		{
            SpeedText.text = "speed: " + currentSpeed.x.ToString() + "\t" + currentSpeed.z.ToString();
        }


        CheckGroundPoint();
    }

    private void Update()
    {
        checkPlayerState();//计算人物状态.
		checkGround();//检测地面

	}

    public void ReBorn(Vector3 rebornPoint)
	{
		Vector3 rebornPos = rebornPoint;
		rebornPos.y += sphereCollider.radius;
		transform.position = rebornPos;
		rb.velocity = Vector3.zero; //重置原有速度
		rb.isKinematic = true;
		checkRebornOnGround = false;

		//decrease life number
		LevelManager.instance.PlayerLife--;
		rebornFX.SetActive(true);
		StartCoroutine(closeRebornFx());
	}

	IEnumerator closeRebornFx()
	{
		yield return new WaitForSeconds(rebornFxShowTime);
		rebornFX.SetActive(false);
	}

    public void CheckGroundPoint()
    {
		if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), 1, groundCheck))
		{
			LevelManager.instance.lastDownPoint = groundCheckPoint.position;
        }
    }
    /// <summary>
    /// 计算当前最大速度
    /// </summary>
    /// <returns></returns>
    Vector2 calcMaxSpeed()
    {        
        float speedX = 0;
        float speedZ = 0;
        int vecScale = curMovement.x > 0 ? 1 : -1;
        speedX = maxSpeedX * vecScale + kazeSlowDown.x;
        vecScale = curMovement.z > 0 ? 1 : -1;
        speedZ = maxSpeedZ * vecScale + kazeSlowDown.y;

        Vector2 speedCheck = new Vector2(speedX, speedZ);
        return speedCheck;
    }
    /// <summary>
    /// 计算当前速度
    /// </summary>
    /// <returns></returns>
    Vector2 calcMoveSpeed()
    {
        Vector2 XZvelocity = new Vector2(rb.velocity.x, rb.velocity.z);
        Vector2 curMaxSpeed = calcMaxSpeed();

        if (curMovement.x * curMaxSpeed.x >= 0)
        {
            float speedNum = Mathf.Abs(XZvelocity.x);
            float speedmaxNum = Mathf.Abs(curMaxSpeed.x);
            if (XZvelocity.x * curMaxSpeed.x >= 0)
            {               
                XZvelocity.x = speedNum > speedmaxNum ? curMaxSpeed.x : XZvelocity.x;
            }
            else
            {
                XZvelocity.x = speedNum > maxSpeedX ? maxSpeedX : XZvelocity.x;
            }

        }
        else
        {

        }

       if (curMovement.z * curMaxSpeed.y >= 0)
        {
            float speedNum = Mathf.Abs(XZvelocity.y);
            float speedmaxNum = Mathf.Abs(curMaxSpeed.y);
            if (XZvelocity.y * curMaxSpeed.y >= 0)
            {
                XZvelocity.y = speedNum > speedmaxNum ? curMaxSpeed.y : XZvelocity.y;
            }
            else
            {
                XZvelocity.y = speedNum > maxSpeedZ ? maxSpeedZ : XZvelocity.y;
            }
        }
        else
        {

        }

        return XZvelocity;
    }
    /// <summary>
    /// 检测当前角色所有状态
    /// </summary>
    public void checkPlayerState()
    {
        if(playerState  != null)
        {
            foreach(var pair in playerState.stateDic)
            {               
                    switch (pair.Key)
                    {
                        case PlayerStateType.InKaze:
						{
							playerState.OnInWind(pair.Value);
							break;

						}
				}               
            }
        }
    }
	/// <summary>
	///	根据是否要落地保持静止来控制是否需要开启地面碰撞检测
	/// </summary>
	/// <param name="collision"></param>
	void checkGround()
	{
		if (!checkRebornOnGround)
		{
			if (Physics.Raycast(transform.position, Vector3.down, 0.51f, layers))
			{

				rb.velocity = Vector3.zero;
				rb.isKinematic = false;
				checkRebornOnGround = true;
			}
		}

	}
}
