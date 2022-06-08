using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPendulum : MonoBehaviour
{
    [SerializeField]
    private int angle;
    [SerializeField]
    private float spinningSpeed;

    private int aim;

    // Start is called before the first frame update
    void Start()
    {
        aim = angle;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nowDirection = transform.eulerAngles;
        if (nowDirection.x >= 270)
            nowDirection.x -= 360;

        float delta = (float)-spinningSpeed / ((float)Math.Abs(aim) + (float)0.5) * (float)Math.Abs(nowDirection.x) + (float)spinningSpeed;

        if ((int)nowDirection.x < aim)
            nowDirection.x += delta;
        else if ((int)nowDirection.x > aim)
            nowDirection.x -= delta;
        else
        {
            nowDirection.x = aim;
            aim = -aim;
        }
        transform.eulerAngles = nowDirection;
    }
}
