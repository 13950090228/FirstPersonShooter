using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敌人血条显示
/// </summary>

public class HPCreate : MonoBehaviour 
{
    private GameObject HPPrefab;
    private GameObject HPShow;
    private Slider slider;
    private RectTransform sliderRect;
    private Text text;
    private GameObject EnemySlider;
    private float currentHP;
    private float maxHP;
    private void Start()
    {
        //找到画布
        HPShow = GameObject.Find("HPShow");

        //找到Slider滚动条，并复制一个
        HPPrefab = GameObject.Find("PlayerHP");
        EnemySlider = Instantiate(HPPrefab);

        //将原来滚动条的玩家血量脚本删除
        EnemySlider.GetComponent<PlayerHP>().enabled = false;

        //设置父物体为画布
        //EnemySlider.transform.parent = HPShow.transform;
        EnemySlider.transform.SetParent(HPShow.transform);

        //获取敌人的最大生命值
        maxHP = GetComponent<EnemyStatusInfo>().maxHP;

        //将滚动条的最大值设置为敌人的最大值
        slider = EnemySlider.GetComponent<Slider>();
        slider.maxValue = maxHP;
        
        //设置滚动条位置
        sliderRect = EnemySlider.GetComponent<RectTransform>();
        sliderRect.anchoredPosition = new Vector2(-123, 40.5f);

        //设置文字显示
        text = EnemySlider.GetComponentInChildren<Text>();
        text.text = "敌人血量";


    }

    private void Update()
    {
        //将滚动条的当前值设置为敌人的当前值
        currentHP = GetComponent<EnemyStatusInfo>().currentHP;
        slider.value = currentHP;
        text.text = "敌人血量:"+currentHP.ToString();
    }
}
