using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFallingTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            GetComponentInChildren<MovingTree>().MakeItEnabled();
        }
    }
}
