using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例音频触发检测器
/// </summary>

public class AudioTrigger : MonoBehaviour 
{
    private static AudioTrigger audioState;
    private static bool isTrigger = false;


    private AudioTrigger()
    {

    }


    public static AudioTrigger GetInstance()
    {
        if (audioState == null)
        {
            return new AudioTrigger();
        }

        return audioState;
    }

    public void StateRevise(bool value)
    {
        isTrigger = value;

    }


    public bool GetState()
    {
        return isTrigger;
    }


}
