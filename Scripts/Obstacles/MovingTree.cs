using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTree : MonoBehaviour
{
    private bool isShaking;
    private bool isFalling;
    private bool isBouncing;
    private bool used;

    private int shakeTime;
    private int aim;
    private float delta;

    private GameObject father;

    // Start is called before the first frame update
    void Start()
    {
        father = transform.parent.gameObject;

        isShaking = false;
        isFalling = false;
        isBouncing = false;
        used = false;

        shakeTime = 0;
        aim = 2;
        delta = (float)0.125;
    }

    // Update is called once per frame
    void Update()
    {
        if (!used && isShaking)
            Shake();
        else if (isFalling)
            Fall();
        else if (isBouncing)
            Bounce();
    }

    //private void OnBecameVisible()
    //{
    //    isShaking = true;
    //}

    public void MakeItEnabled()
    {
        isShaking = true;
    }

    private void Shake()
    {
        Vector3 nowDirection = father.transform.eulerAngles;
        if (nowDirection.x >= 270)
            nowDirection.x -= 360;

        if ((int)nowDirection.x < aim)
            nowDirection.x += (float)0.125;
        else if ((int)nowDirection.x > aim)
            nowDirection.x -= (float)0.125;
        else
        {
            nowDirection.x = aim;
            aim = -aim;
            shakeTime++;
        }
        father.transform.eulerAngles = nowDirection;

        if (shakeTime == 6)
        {
            isShaking = false;
            isFalling = true;
            used = true;
            aim = 80;
        }
    }

    private void Fall()
    {
        Vector3 nowDirection = father.transform.eulerAngles;
        if (nowDirection.x > 180)
            nowDirection.x -= 360;

        if ((int)nowDirection.x < aim)
        {
            nowDirection.x += delta;
            delta += (float)0.002;
        }
        else
        {
            nowDirection.x = aim;
            isFalling = false;
            isBouncing = true;

            shakeTime = 70;
            aim = shakeTime;
            delta = (float)0.4;
        }
        father.transform.eulerAngles = nowDirection;
    }

    private void Bounce()
    {
        Vector3 nowDirection = father.transform.eulerAngles;
        if (nowDirection.x > 180)
            nowDirection.x -= 360;

        if ((int)nowDirection.x < aim)
        {
            nowDirection.x += delta;
            delta += (float)0.001;
        }

        else if ((int)nowDirection.x > aim)
        {
            nowDirection.x -= delta;
            delta -= (float)0.001;
        }

        else
        {

            nowDirection.x = aim;

            if (aim == 80)
            {
                shakeTime += 2;
                aim = shakeTime;
            }
            else
            {
                aim = 80;
                delta = (float)0.005;
            }

            if (shakeTime == 80)
            {
                isBouncing = false;
                used = true;
            }

        }
        father.transform.eulerAngles = nowDirection;
    }
}
