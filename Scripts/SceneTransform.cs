using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            GameController.Instance.LoadNextScene();
    }
}
