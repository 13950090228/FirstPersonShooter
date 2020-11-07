using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人动画
/// </summary>

public class EnemyAnimation : MonoBehaviour 
{
    public string runAnimName ;
    public string shootingAnimName ;
    public string idleAnimName;
    public string deathAnimName ;
    public AnimationAction action;
    public void Awake()
    {
        action = new AnimationAction(GetComponentInChildren<Animation>());
    }
}
