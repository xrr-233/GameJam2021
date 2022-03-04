using System.Collections;
using System.Collections.Generic;
using Platform.Character;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 移动平台
/// </summary>

public class MovingPlatform : MonoBehaviour
{

    public Transform[] patrolPoints;
    public float speed;
    public int currentPointIndex;

    private float waitTime;
    public float startWaitTime;


    private void Start()
    {
        transform.position = patrolPoints[0].position;
        waitTime = startWaitTime;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
        if (transform.position == patrolPoints[currentPointIndex].position)
        {
            if (waitTime <= 0)
            {
                if (currentPointIndex + 1 < patrolPoints.Length)
                {
                    currentPointIndex++;
                }
                else
                {
                    currentPointIndex = 0;
                }
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.parent = transform;
            collision.GetComponent<CharacterMotor>().isOnMovingPlatform = true;
        }
    }


    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.parent = null;
            collision.GetComponent<CharacterMotor>().isOnMovingPlatform = false;
        }
    }



}
