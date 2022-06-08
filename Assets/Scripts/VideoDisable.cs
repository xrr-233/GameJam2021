using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoDisable : MonoBehaviour
{
    void Update()
    {
        if (Time.time > 17)
        {
            this.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
