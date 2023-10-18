using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;
using GameDefine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    // Create public variables for player speed, and for the Text UI game objects
    public float speed;
    public Text SpeedText;
	public float maxSpeed;
	public bool checkRebornOnGround; //重生判断落地
	public float addForceScale = 1; //施加的移动力大小
	public float nitoriScale = 1;//荷取能力影响施力参数

	public int CurrentHealth;// for Health bar
	public int MaxHealth = 3;// for Health bar


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
    public Vector2 windSlowDown = Vector2.zero;
    public Vector2 windForce = Vector2.zero;
	public bool isOnGround = false;
    private Vector3 curMovement = Vector3.zero;
	private SphereCollider sphereCollider;
	[SerializeField]
	float normalForceScale = 1, uTurnForceScale = 10;
	float rebornFxShowTime = 1;
	[SerializeField]
	bool isReborning = false;

	// Hina
	public GameObject HinaObject = null;
	public float HinaForce = 0;
	public Vector3 HinaRotate = Vector3.zero;

	[SerializeField]
	GameObject rebornFX;
	// At the start of the game..
	void Start ()
	{
		CurrentHealth = MaxHealth;
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();
		sphereCollider = GetComponent<SphereCollider>();

    }

    // Each physics step..

	//Check the speed and the current direction, if not the same, increase the force
	//until current speed is equal to 0
    void Checkdirection(Vector3 movement, Vector3 currentSpeed)
	{

		//x axis
		//if ((currentSpeed.x > 0 && movement.x >=0 ) ||
		//          (currentSpeed.x < 0 && movement.x <= 0)) 
		//{
		//	addForceScale = normalForceScale;
		//}
		//else
		//{
		//	addForceScale = uTurnForceScale;
		//}

		//      //z axis
		//      if ((currentSpeed.z > 0 && movement.z >= 0) ||
		//	(currentSpeed.z < 0 && movement.z <= 0))
		//      {
		//	addForceScale = normalForceScale;
		//      }
		//      else
		//      {
		//	addForceScale = uTurnForceScale;
		//      }

		if ((currentSpeed.x * movement.x < 0) && (currentSpeed.z * movement.z < 0))
		{
			addForceScale = uTurnForceScale;
		}
		else
		{
			addForceScale = normalForceScale;
		}
		addForceScale *= nitoriScale;


	}

	void FixedUpdate ()
	{
		checkMove();
		CheckGroundPoint();
    }

    private void Update()
    {
        checkPlayerState();//计算人物状态.
		checkGround();//检测地面
		checkHina();//Checking whether on the top of the Hina item
	}

	void checkMove()
	{
		if(isReborning)
		{
			return;
		}
		// Set some local float variables equal to the value of our Horizontal and Vertical Inputs
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Rigidbody rb = GetComponent<Rigidbody>();
		Vector3 currentSpeed = rb.velocity;
		//int addForce = 1;

		// Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
		curMovement = moveVertical * LevelManager.instance.curForward + moveHorizontal * LevelManager.instance.curRight;
		//Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		// check if wsad get pressed or not
		if (curMovement != Vector3.zero)
			Checkdirection(curMovement, currentSpeed);


		// Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
		// multiplying it by 'speed' - our public player speed that appears in the inspector

		Vector3 kazePower = new Vector3(windForce.x, 0, windForce.y);
		Vector3 moveForce = curMovement * speed * addForceScale + kazePower;

		rb.AddForce(moveForce);

		//if (XZvelocity.magnitude > curMaxSpeed)
		//{
		//    XZvelocity = XZvelocity.normalized * curMaxSpeed;
		//}
		Vector2 XZvelocity = calcMoveSpeed();
		rb.velocity = new Vector3(XZvelocity.x, rb.velocity.y, XZvelocity.y);

		// display speed
		if (SpeedText != null)
		{
			SpeedText.text = "speed: " + currentSpeed.x.ToString() + "\t" + currentSpeed.z.ToString();
		}

	}

	void checkHina()
	{
		//setting the parent
		if(HinaObject != null)
		{
			if (this.gameObject.transform.parent != HinaObject.transform)
			{ 
				this.gameObject.transform.SetParent(HinaObject.transform, true);
			}
		}

		//adding an additional Hina Force
		if (HinaForce != 0)
		{
			Rigidbody rb = GetComponent<Rigidbody>();
			Vector2 ForceDirect =
				new Vector2
				(rb.transform.position.x - HinaObject.transform.position.x,
				 rb.transform.position.z - HinaObject.transform.position.z);

			ForceDirect = ForceDirect.normalized * HinaForce;

			//The exact same Hina Force but vectical
			Vector2 VecticalForceDirect = Vector2.zero;

			//Check the rotation
			if (HinaRotate.y >0)
				VecticalForceDirect = new Vector2(ForceDirect.y, -ForceDirect.x);
			else if (HinaRotate.y < 0)
				VecticalForceDirect = new Vector2(-ForceDirect.y, ForceDirect.x);


			rb.AddForce(ForceDirect.x, 0, ForceDirect.y);
				if(VecticalForceDirect != Vector2.zero)
			rb.AddForce(VecticalForceDirect.x, 0, VecticalForceDirect.y);
		}
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
		isReborning = true;
		yield return new WaitForSeconds(rebornFxShowTime);
		rebornFX.SetActive(false);
		isReborning = false;
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
		//float speedX = 0;
		//float speedZ = 0;
		//int vecScale = curMovement.x > 0 ? 1 : -1;
		//speedX = maxSpeedX * vecScale + windSlowDown.x;
		//vecScale = curMovement.z > 0 ? 1 : -1;
		//speedZ = maxSpeedZ * vecScale + windSlowDown.y;

		//Vector3 slowDown = new Vector3(windSlowDown.x, 0, windSlowDown.y);
		Vector2 velocity = new Vector2(rb.velocity.x, rb.velocity.z);
		Vector2 speedCheck = Vector2.zero;
		float angle = Vector2.Angle(velocity, windSlowDown);
		speedCheck.x = MathF.Abs(windSlowDown.x) * Mathf.Sin(angle * Mathf.Deg2Rad);
		speedCheck.y = MathF.Abs(windSlowDown.y) * Mathf.Cos(angle * Mathf.Deg2Rad);


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
		float nowMaxSpeed = maxSpeed + curMaxSpeed.x + curMaxSpeed.y;
		//print("curMaxSpeed" + nowMaxSpeed + "  " + XZvelocity.magnitude);
		if (XZvelocity.magnitude > nowMaxSpeed)
		{
			XZvelocity = XZvelocity.normalized * nowMaxSpeed;
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
					case PlayerStateType.InHina:
						{
							playerState.OnInHina(pair.Value);
							break;

						}
					case PlayerStateType.Slippy:
						{
							playerState.OnSlippy(pair.Value);
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
		RaycastHit[] hits;
		hits = Physics.RaycastAll(transform.position, Vector3.down, sphereCollider.radius + 1f, layers);
		if (hits.Length > 0)
		{
			if (!checkRebornOnGround)
			{
				rb.velocity = Vector3.zero;
				rb.isKinematic = false;
				checkRebornOnGround = true;
			}
			isOnGround = true;
		}
		else
		{
			isOnGround = false;
		}

		rebornFX.transform.position = transform.position;
	}
}
