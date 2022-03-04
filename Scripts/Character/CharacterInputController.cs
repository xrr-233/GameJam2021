using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///角色控制器
///</summary>
namespace Platform.Character
{
    public class CharacterInputController : MonoBehaviour
    {
        private ETCJoystick joystick;
        private CharacterMotor motor;
        private Animator anim;
        private ETCButton jumpButton;
        private CharacterStatus status;

        private float startJumpTime;
        public float jumpInterval = 1;

        private AudioSource sound;
        private AudioSource sound2;
        [SerializeField]
        private AudioClip jump;
        [SerializeField]
        private AudioClip[] material_1 = new AudioClip[4];
        [SerializeField]
        private AudioClip[] material_2 = new AudioClip[4];
        [SerializeField]
        private AudioClip[] material_3 = new AudioClip[4];
        [SerializeField]
        private AudioClip[] material_4 = new AudioClip[4];
        [SerializeField]
        private AudioClip[] material_1_jump = new AudioClip[4];
        [SerializeField]
        private AudioClip[] material_2_jump = new AudioClip[4];
        [SerializeField]
        private AudioClip[] material_3_jump = new AudioClip[4];
        [SerializeField]
        private AudioClip[] material_4_jump = new AudioClip[4];

        [SerializeField]
        private Collider initCollider;
        private int material;

        private void Awake()
        {
            //查找组件
            motor = GetComponent<CharacterMotor>();
            status = GetComponentInChildren<CharacterStatus>();
            anim = GetComponentInChildren<Animator>();
            jumpButton = FindObjectOfType<ETCButton>();
            joystick = FindObjectOfType<ETCJoystick>();

            sound = gameObject.AddComponent<AudioSource>();
            sound.playOnAwake = false;
            sound2 = gameObject.AddComponent<AudioSource>();
            sound2.playOnAwake = false;
        }

        private void OnEnable()
        {
            //注册事件
            joystick.onMove.AddListener(OnJoystickMove);
            joystick.onMoveStart.AddListener(OnJoystickMoveStart);
            joystick.onMoveEnd.AddListener(OnJoystickMoveEnd);
            jumpButton.onDown.AddListener(OnJumpButtonDown);
            jumpButton.onUp.AddListener(OnJumpButtonUp);
        }

        private void OnJumpButtonUp()
        {
            anim.SetBool(status.chParam.jump, false);
            motor.jumpPressed = false;
        }

        private void OnJumpButtonDown()
        {
            switch (material)
            {
                case 1:
                    sound2.clip = material_1_jump[UnityEngine.Random.Range(0, 4)];
                    break;
                case 2:
                    sound2.clip = material_2_jump[UnityEngine.Random.Range(0, 4)];
                    break;
                case 3:
                    sound2.clip = material_3_jump[UnityEngine.Random.Range(0, 4)];
                    break;
                case 4:
                    sound2.clip = material_4_jump[UnityEngine.Random.Range(0, 4)];
                    break;
                default:
                    sound2.clip = null;
                    break;
            }
            if (sound2.clip != null)
                sound2.Play();
            sound.clip = jump;
            sound.Play();
            material = 0;

            anim.SetBool(status.chParam.jump, true);
            motor.jumpPressed = true;
            if (startJumpTime < Time.time) 
            {
                motor.Jump();
                startJumpTime = Time.time + jumpInterval;
            }
        }


        private void OnJoystickMove(Vector2 dir)
        {
            if (!sound.isPlaying)
            {
                switch (material)
                {
                    case 1:
                        sound.clip = material_1[UnityEngine.Random.Range(0, 4)];
                        break;
                    case 2:
                        sound.clip = material_2[UnityEngine.Random.Range(0, 4)];
                        break;
                    case 3:
                        sound.clip = material_3[UnityEngine.Random.Range(0, 4)];
                        break;
                    case 4:
                        sound.clip = material_4[UnityEngine.Random.Range(0, 4)];
                        break;
                    default:
                        sound.clip = null;
                        break;
                }
                if(sound.clip != null)
                    sound.Play();
            }
                

            CameraState state = CameraController.Instance.cameraState;
            if (state == CameraState.Top)
                motor.Movement(new Vector3(0, 0, dir.y));
            else if (state == CameraState.Main)
                motor.Movement(new Vector3(dir.x, 0, 0));
            else if (state == CameraState.Side)
                motor.Movement(new Vector3(0, 0, -dir.x));
        }

        private void OnJoystickMoveStart()
        {
            anim.SetBool(status.chParam.fly, true);
        }

        private void OnJoystickMoveEnd()
        {
            anim.SetBool(status.chParam.fly, false);
        }

        private void OnDisable()
        {
            //注销事件
            joystick.onMove.RemoveListener(OnJoystickMove);
            joystick.onMoveStart.RemoveListener(OnJoystickMoveStart);
            joystick.onMoveEnd.RemoveListener(OnJoystickMoveEnd);
            jumpButton.onPressed.RemoveListener(OnJumpButtonDown);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Player")
                other = initCollider;
            print(other.gameObject);
            material = other.gameObject.transform.GetComponent<MaterialAttribute>().getMaterial();
            print(material);
        }

        //public void InverseAllButton()
        //{
        //    var joyStickRectTF = joystick.GetComponent<RectTransform>();
        //    var buttonRectTF = jumpButton.GetComponent<RectTransform>();

        //    float joyX = joyStickRectTF.anchoredPosition.x;
        //    float joyY = joyStickRectTF.anchoredPosition.y;
        //    joyStickRectTF.anchoredPosition = new Vector2(-joyX, -joyY);
        //    joyStickRectTF.rotation = Quaternion.Euler(new Vector3(0, 0, 180));

        //    float btnX = buttonRectTF.anchoredPosition.x;
        //    float btnY = buttonRectTF.anchoredPosition.y;
        //    buttonRectTF.anchoredPosition = new Vector2(-btnX, -btnY);
        //}

    }

}
