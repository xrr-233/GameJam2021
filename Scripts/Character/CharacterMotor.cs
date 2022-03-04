using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///</summary>
namespace Platform.Character
{
    public class CharacterMotor : MonoBehaviour
    {
        [Tooltip("旋转速度")]
        public float rotateSpeed = 20;
        [Tooltip("移动速度")]
        public float speed = 20;
        [Tooltip("跳跃速度")]
        public float jumpSpeed = 8;

        public Vector3 velocity;

        public bool isOnMovingPlatform = false;

        [Tooltip("跳跃高度")]
        public float firstJumpHeight = 3f;
        [Tooltip("跳跃高度")]
        public float secondJumpHeight = 3f;
        [Tooltip("重力加速度")]
        public float gravity = 9.8f;

        private CharacterController characterController;
        [Tooltip("跳跃次数")]
        public int jumpCount;
        [HideInInspector]
        public bool jumpPressed = false;
       
        private void Start()
        {
            characterController = GetComponent<CharacterController>();
        }

        public void Movement(Vector3 direction)
        {
            Quaternion dir = Quaternion.LookRotation(direction);
            Vector3 euler = dir.eulerAngles;
            transform.Find("Model").eulerAngles = new Vector3(0, euler.y, 0);
            characterController.Move(direction * speed * Time.deltaTime);
        }

        private void Update()
        {
            //if (Time.time < 17) return;
            if(!isOnMovingPlatform || jumpPressed)
                DecreaseYVelocityByGravity();

            if (characterController.isGrounded) jumpCount = 0;

            if (transform.position.y < -50)
                GameController.Instance.RestartGame();

            //InputAccelerateDetect();    
        }

        private void DecreaseYVelocityByGravity()
        {
            if (characterController.isGrounded && velocity.y < 0)
                velocity.y = -2;

            velocity.y -= gravity * Time.deltaTime;

            characterController.Move(velocity * Time.deltaTime);
        }

        public void Jump()
        {
            if (!characterController.isGrounded && jumpCount == 2) return;

            if (jumpCount == 0)
                velocity.y = Mathf.Sqrt(firstJumpHeight * 2f * gravity);
            else if (jumpCount == 1)
                velocity.y = Mathf.Sqrt(secondJumpHeight * 2f * gravity);

            jumpCount++;
            //characterController.velocity = new Vector3(0, velocity.y, 0);
        }

        //private float currentAccelertate;
        //private float lastAccelerate;

        //public void InputAccelerateDetect()
        //{
        //    if (Input.acceleration.y < -0.6)
        //        lastAccelerate = Input.acceleration.y;

        //    if(Input.acceleration.y > 0.6)
        //        currentAccelertate = Input.acceleration.y;

        //    Test.Instance.printText = Mathf.Abs(currentAccelertate - lastAccelerate).ToString();
        //    if (Mathf.Abs(currentAccelertate - lastAccelerate) > 1.2)
        //    {
        //        gravity = -gravity;
        //        GetComponent<CharacterInputController>().InverseAllButton();
        //        lastAccelerate = 0;
        //        currentAccelertate = 0;
        //    }

        //}

    }

}
