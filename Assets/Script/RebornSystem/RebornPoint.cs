using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebornPoint : MonoBehaviour
{
    public int CheckIdx;

    public Transform rebornPoint;

	public Vector3 bornPosition;

    public RebornType rebornType = RebornType.Normal;

	public LayerMask layers;
	[SerializeField]
	private BoxCollider boxCollider;
	private void Awake()
	{
		///使用时放置的平面需要拥有layers中包含的layer才可使用
		RaycastHit[] hits;
		if (rebornType == RebornType.Normal)
		{
			hits = Physics.RaycastAll(transform.position, Vector3.down, 10000, layers);
			if (hits.Length > 0)
			{
				bornPosition = hits[0].point;
				Vector3 setPosition = bornPosition;
				setPosition.y += boxCollider.size.y / 2;
				boxCollider.transform.position = setPosition;  //将碰撞盒至于平面
			}
		}
		else if (rebornType == RebornType.Set)
		{
			hits = Physics.RaycastAll(rebornPoint.position, Vector3.down, 10000, layers);
			if (hits.Length > 0)
			{
				bornPosition = hits[0].point;
			}
		}



	}

	private void OnTriggerEnter(Collider other)
    {
        if (other?.gameObject.tag == "Player")
        {
			
            LevelManager.instance.CheckRebornPoint(this);
        }
    }
}
