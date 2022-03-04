using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoSingleton<CameraController>
{
    private Vector2 startMousePos;
    private Vector2 endMousePos;
    private Vector2 deltaMousePos;

    private bool isOnUI;
    private bool rotationEnabled;
    private int operation = 0; // 1为右 2为左 3为上 4为下

    private int direction;
    private Vector3 expectedDirection;
    private Vector3 front, left, top;

    [SerializeField]
    private int leftBound, rightBound;

    //摄像机视角枚举 Main为主视角 Side为侧视图 Top为俯视图
    [HideInInspector]
    public CameraState cameraState;

    private AudioSource sound;
    [SerializeField]
    private AudioClip leftSound;
    [SerializeField]
    private AudioClip rightSound;
    [SerializeField]
    private AudioClip topSound;
    [SerializeField]
    private AudioClip bottomSound;

    // Start is called before the first frame update
    void Start()
    {
        isOnUI = false;
        rotationEnabled = false;
        direction = 1;
        front = new Vector3(0, 0, 0);
        left = new Vector3(0, 90, 0);
        top = new Vector3(90, 0, 0);
        expectedDirection = front;
        cameraState = CameraState.Main;

        sound = gameObject.AddComponent<AudioSource>();
        sound.playOnAwake = false;
    }

    private void MouseDragRoatation()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                isOnUI = true;
            startMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endMousePos = Input.mousePosition;
            if (isOnUI)
            {
                isOnUI = false;
                startMousePos = endMousePos;
            }
            deltaMousePos = endMousePos - startMousePos;

            bool toLeft = Math.Abs(deltaMousePos.x) >= Math.Abs(deltaMousePos.y) && deltaMousePos.x > 500;
            bool toRight = Math.Abs(deltaMousePos.x) >= Math.Abs(deltaMousePos.y) && deltaMousePos.x < -500;
            bool toTop = Math.Abs(deltaMousePos.x) < Math.Abs(deltaMousePos.y) && deltaMousePos.y < -300;
            bool toBottom = Math.Abs(deltaMousePos.x) < Math.Abs(deltaMousePos.y) && deltaMousePos.y > 300;

            Vector3 nowDirection = transform.eulerAngles;

            if (nowDirection.Equals(front))
            {
                if (toLeft)
                {
                    sound.clip = leftSound;
                    sound.Play();
                    rotationEnabled = true;
                    expectedDirection = left;
                    operation = 2;
                    cameraState = CameraState.Side;
                }
                else if (toTop)
                {
                    sound.clip = topSound;
                    sound.Play();
                    rotationEnabled = true;
                    expectedDirection = top;
                    operation = 3;
                    cameraState = CameraState.Top;
                }
            }
            else if (nowDirection.Equals(left) && toRight)
            {
                sound.clip = rightSound;
                sound.Play();
                rotationEnabled = true;
                expectedDirection = front;
                operation = 1;
                cameraState = CameraState.Main;
            }
            else if (nowDirection.Equals(top) && toBottom)
            {
                sound.clip = bottomSound;
                sound.Play();
                rotationEnabled = true;
                expectedDirection = front;
                operation = 4;
                cameraState = CameraState.Main;
            }

            startMousePos = Vector2.zero;
            endMousePos = Vector2.zero;
        }
    }

    public void LookAtTarget()
    {

        if ((int)transform.eulerAngles.x != (int)expectedDirection.x || (int)transform.eulerAngles.y != (int)expectedDirection.y)
        {
            Vector3 nowDirection = transform.eulerAngles;
            switch (operation)
            {
                case 1:
                    nowDirection.y--;
                    break;
                case 2:
                    nowDirection.y++;
                    break;
                case 3:
                    nowDirection.x++;
                    break;
                case 4:
                    nowDirection.x--;
                    break;
            }
            if (nowDirection.y < 0)
                nowDirection.y += 360;
            if (nowDirection.y >= 360)
                nowDirection.y -= 360;
            if (nowDirection.x < 0)
                nowDirection.y += 360;
            if (nowDirection.x >= 360)
                nowDirection.y -= 360;
            transform.eulerAngles = nowDirection;
        }
        else
        {
            transform.eulerAngles = expectedDirection;
            rotationEnabled = false;
        }

        //Translate(expectedDirection);
    }

    private void BoundaryRestrictor()
    {
        if (transform.parent.position.x < leftBound)
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
        if (transform.parent.position.x > rightBound)
            transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        //print(cameraState);
        if (rotationEnabled)
            LookAtTarget();
        else
            MouseDragRoatation();
        if (cameraState == CameraState.Main)
            BoundaryRestrictor();
    }
}
