using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebornPoint : MonoBehaviour
{
    public int CheckIdx;

    public Transform rebornPoint;

    public RebornType rebornType = RebornType.Normal;

    private void OnTriggerEnter(Collider other)
    {
        if (other?.gameObject.tag == "Player")
        {
            LevelManager.instance.CheckRebornPoint(this);
        }
    }
}
