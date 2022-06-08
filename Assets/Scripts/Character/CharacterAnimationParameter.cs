using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///角色动画参数类
///</summary>
namespace Platform.Character
{
    [Serializable] //序列化 将当前对象嵌入到脚本后，可在编译器中显示
    public class CharacterAnimationParameter
    {
        public string jump = "jump";
        //public string death = "death";
        public string idle = "idle";
        //public string attack01 = "attack1";
        //public string attack02 = "attack2";
        //public string attack03 = "attack3";
        public string fly = "fly";

    }
}
