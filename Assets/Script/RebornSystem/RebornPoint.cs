using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebornPoint : MonoBehaviour
{
    public int CheckIdx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LevelManager.instance.CheckRebornPoint(this);
        }
    }
}
