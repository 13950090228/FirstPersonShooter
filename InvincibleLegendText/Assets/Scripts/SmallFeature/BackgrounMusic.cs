using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 音乐设置
/// </summary>

public class BackgrounMusic : MonoBehaviour 
{

    //AudioSource组件
    private AudioSource back;
    private AudioSource gun1Music;
    private AudioSource gun2Music;

    //UI控制音乐组件
    public Slider backMusic;
    public Slider GunMusic;

    //对象
    public GameObject player;   //玩家对象
    public GameObject canvas;   //画布
    public GameObject Gun1Obj;
    public GameObject Gun2Obj;//枪对象

    private bool playerBool = true; //玩家初始显隐
    private bool sliderBool = false;  //Slider组件的初始显隐

    public void Start()
    {
        gun1Music = Gun1Obj.GetComponent<AudioSource>();
        gun2Music = Gun2Obj.GetComponent<AudioSource>();
        back = GetComponent<AudioSource>();
        back.loop = true; //设置循环播放  
        back.volume = 0.0f;//设置音量最大，区间在0-1之间

        //back.Play(); //播放背景音乐，
        
    }

    public void Update()
    {
        if (canvas.gameObject.activeInHierarchy)
        {
            back.volume = backMusic.value;
            gun1Music.volume = GunMusic.value;
            gun2Music.volume = GunMusic.value;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (playerBool == true)
            {
                backMusic.value = back.volume;
                GunMusic.value = gun1Music.volume;
                player.SetActive(false);
                canvas.gameObject.SetActive(true);
                playerBool = false;
                sliderBool = true;
            }
            else
            {
                player.SetActive(true);
                canvas.gameObject.SetActive(false);
                playerBool = true;
                sliderBool = false;
            }
        }


    }

}
