using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDefine;
using UnityEditor;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class PlayerStateCtrl : MonoBehaviour
{
	////////////////////////////////////////////////////////////////OnGui显示相关
	[SerializeField]
	public Rect startRect = new Rect(512, 100, 750f, 500f);
	private GUIStyle style;
	private string show = "";
	[SerializeField]
	private Vector2 scrollPosition = Vector2.zero;
	[SerializeField]
	private Rect scrollRect = Rect.zero;
	//////////////////////////////////////////////////////////////

	private GameObject PlayerSet;


	[SerializeField]
	PlayerController playerController;
	[SerializeField]
	public Dictionary<PlayerStateType, PlayerState> stateDic = new Dictionary<PlayerStateType, PlayerState>();

	private NormalDefine normalDefine;
	public void AddState(PlayerStateType type, PlayerState state, int layer = 1)
	{
		state.stateType = type;
		if (stateDic.ContainsKey(type))
		{
			state.layer = layer;
			stateDic[type] += state;
		}
		else
		{
			state.isOn = true;
			state.layer = layer;
			stateDic.Add(type, state);
		}
	}

	public void RemoveState(PlayerStateType type, PlayerState state, int layer = 1)
	{
		state.stateType = type;
		if (stateDic.ContainsKey(type))
		{
			state.layer = layer;
			stateDic[type] -= state;
		}
		else
		{

		}
	}
	private void Start()
	{
		PlayerSet = GameObject.Find("PlayerSet");
	}
	public void OnInWind(PlayerState state)
	{
		if (state.isOn)
		{
			playerController.windSlowDown.x = state.FloatparamList[0];
			playerController.windSlowDown.y = state.FloatparamList[1];
			playerController.windForce.x = state.FloatparamList[2];
			playerController.windForce.y = state.FloatparamList[3];
		}
		else
		{
			playerController.windSlowDown.x = 0;
			playerController.windSlowDown.y = 0;
			playerController.windForce.x = 0;
			playerController.windForce.y = 0;
		}
	}


	public void OnInHina(PlayerState state)
	{
		if (state.isOn)
		{
			playerController.HinaObject = state.ObjectparamList[0];
			playerController.HinaForce = state.FloatparamList[0];
			playerController.HinaRotate.x = state.FloatparamList[1];
			playerController.HinaRotate.y = state.FloatparamList[2];
			playerController.HinaRotate.z = state.FloatparamList[3];

		}
		else
		{
			playerController.HinaObject = PlayerSet;
			playerController.HinaForce = 0;
			playerController.HinaRotate.x = 0;
			playerController.HinaRotate.y = 0;
			playerController.HinaRotate.z = 0;


		}

	}

	public void OnSlippy(PlayerState state)
	{
		if (state.isOn && playerController.isOnGround)
		{
			playerController.rb.drag = state.FloatparamList[0];
			playerController.nitoriScale = state.FloatparamList[1];
		}
		else
		{
			playerController.rb.drag = 1;
			playerController.nitoriScale = 1;
		}

	}


#if UNITY_EDITOR
	public void OnGUI()
	{

		if (style == null)
		{
			style = new GUIStyle(GUI.skin.label);
			style.normal.textColor = Color.white;
			style.alignment = TextAnchor.UpperLeft;
		}
		startRect = GUI.Window(0, startRect, DoMyWindow, "");

		
	}
	 
	void DoMyWindow(int windowID)
	{ 
		Rect showRect = new Rect(0, 0, startRect.width - 20, 20000);
		Rect showScrollRect = new Rect(0, 0, startRect.width, startRect.height - 10);
		scrollPosition = GUI.BeginScrollView(showScrollRect, scrollPosition, showRect);
		show = "";
		if (this != null)
		{

			foreach (var state in stateDic)
			{
				show += "状态类型:  " + state.Value.stateType.ToString() + "  激活状态:  " + state.Value.isOn + "\r\n";
				for (int i = 0; i < state.Value.FloatparamList.Count; i++)
				{
					show += " 参数" + i + ":  " + state.Value.FloatparamList[i] + "\r\n";
				}
				show += "/////////////////////分隔符////////////////////\r\n";
			}
		}
		GUI.Label(showRect, show, style);
		GUI.EndScrollView();
		GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
	}
#endif
}
