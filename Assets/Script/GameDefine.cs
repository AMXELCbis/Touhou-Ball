using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 namespace GameDefine
{
	/// <summary>
	/// 角色状态枚举
	/// </summary>
	[SerializeField]
    public enum PlayerStateType
    {
        /// <summary>
        /// param = 4 , 1、2号位表示最大速度受风力影响值 3、4号位表示风力附加值
        /// </summary>
        InKaze = 0,
    }

    /// <summary>
    /// 有需要用到一些多处会用到的常量就加在此处 最好不要修改值
    /// </summary>
    public class NormalDefine
    {

    }

}
