using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;


public enum RebornType
{
    Normal = 1,
    Set = 2,
    Self = 3,
};
public class RebornCheck : MonoBehaviour
{
    public Transform rebornPoint;
    [SerializeField]
    public RebornType rebornType = RebornType.Normal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController ctrller = other.gameObject.GetComponent<PlayerController>();
            if(ctrller != null)
            {
                switch (rebornType)
                {
                    case RebornType.Normal:
                        ctrller.ReBorn(LevelManager.instance.nowRebornPoint.transform.position);
                        break;
                    case RebornType.Set:
                        ctrller.ReBorn(rebornPoint.position);
                        break;
                    case RebornType.Self:
                        //statement(s);
                        break;

                    /* 您可以有任意数量的 case 语句 */
                    default: /* 可选的 */
                        break;
                }


            }
        }
    }
}
