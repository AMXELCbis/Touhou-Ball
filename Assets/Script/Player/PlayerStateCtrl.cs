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




	[SerializeField]
	PlayerController playerController;
	[SerializeField]
	public Dictionary<PlayerStateType, PlayerState> stateDic = new Dictionary<PlayerStateType, PlayerState>();

	public void AddState(PlayerStateType type, PlayerState state, int layer = 1)
	{
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
		if (stateDic.ContainsKey(type))
		{
			state.layer = layer;
			stateDic[type] -= state;
		}
		else
		{

		}
	}

	public void OnInWind(PlayerState state)
	{
		if (state.isOn)
		{
			playerController.kazeSlowDown.x = state.paramList[0];
			playerController.kazeSlowDown.y = state.paramList[1];
			playerController.kazeForce.x = state.paramList[2];
			playerController.kazeForce.y = state.paramList[3];
		}
		else
		{
			playerController.kazeSlowDown.x = 0;
			playerController.kazeSlowDown.y = 0;
			playerController.kazeForce.x = 0;
			playerController.kazeForce.y = 0;
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
				for (int i = 0; i < state.Value.paramList.Count; i++)
				{
					show += " 参数" + i + ":  " + state.Value.paramList[i] + "\r\n";
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
