using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    void Update()
    {
        if (Time.time < 17) return;

        GetComponent<AudioSource>().mute = false;

    }
}
