using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪动画
/// </summary>

public class GunAnimation : MonoBehaviour 
{
    public string fireAnimName;
    public string updateAnimName;
    public string lackAnimName;

    public AnimationAction action;
    public void Awake()
    {
        action = new AnimationAction(GetComponentInChildren<Animation>());
    }
}
