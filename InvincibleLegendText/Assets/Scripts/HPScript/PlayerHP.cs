using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 玩家血条显示
/// </summary>

public class PlayerHP : MonoBehaviour 
{
    private Slider slider;
    private Text text;
    public GameObject Player;
    private float HP;
    private void Start()
    {   
        //血量初始化
        HP = Player.GetComponent<PlayerStatusInfo>().HP;

        slider = GetComponent<Slider>();
        slider.maxValue = HP;
        slider.value = slider.maxValue;

        text = GameObject.Find("HPValue").GetComponent<Text>();
        text.text = HP.ToString();

    }

    private void Update()
    {
        HP = Player.GetComponent<PlayerStatusInfo>().HP;
        slider.value = HP;
        text.text = "剩余血量：" + HP.ToString();
    }

}
