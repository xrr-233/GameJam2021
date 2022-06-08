using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    Collider goal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.name == goal.name)
            GameController.Instance.LoadNextScene();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
