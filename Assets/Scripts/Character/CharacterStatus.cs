using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///</summary>
namespace Platform.Character
{
    public class CharacterStatus : MonoBehaviour
    {
	    [Tooltip("动画参数")]
        public CharacterAnimationParameter chParam;

        //public float HP;
        //public float maxHP;

        //public float SP;
        //public float maxSP;

        //public float baseATK;

        //public float defence;

        //public float attackDistance;
        //public float attackInterval;


        //public void Damage(float amount)
        //{
        //    amount -= defence;

        //    if (amount <= 0) return;

        //    HP -= amount;

        //    if (HP <= 0)
        //    {
        //        Death();
        //    }
        //}

        ////调用父类死亡方法 执行子类死亡方法： 多态
        //protected virtual void Death()
        //{
        //    GetComponentInChildren<Animator>().SetBool(chParam.death, true);
        //}
    }

}
