using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;


public enum RebornType
{
    Normal = 1,
    Set = 2,
    LastDown = 3,
};
public class RebornCheck : MonoBehaviour
{
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
                LevelManager.instance.Reborn(ctrller);             
            }
        }
    }
}
