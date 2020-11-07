using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 子弹补充
/// </summary>

public class BulletSupplement : MonoBehaviour 
{
    public GameObject playerGun;
    public GameObject ProgressBar;

    //手枪
    public GameObject Gun1;
    private SingleGun gun1;
    //步枪
    private AutomaticGun gun2;
    public GameObject Gun2;
    private Slider sliderSupply;

    private void Start()
    {
        //Gun1 = playerGun.GetComponentInChildren<SingleGun>();
        //Gun2 = playerGun.GetComponentInChildren<AutomaticGun>();
        gun1 = Gun1.GetComponent<SingleGun>();
        gun2 = Gun2.GetComponent<AutomaticGun>();
        sliderSupply = ProgressBar.GetComponent<Slider>();
        sliderSupply.maxValue = 100;
    }

    private void Update()
    {

        if (Vector3.Distance(transform.position, playerGun.transform.position)<2)
        {

            if (Input.GetKey(KeyCode.E))
            {
                
                ProgressBar.SetActive(true);
                sliderSupply.value += 2;
                if(sliderSupply.value == sliderSupply.maxValue)
                {
                    gun1.remainBullets = 50;
                    gun2.remainBullets = 150;
                    ProgressBar.SetActive(false);
                    
                    
                }
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                ProgressBar.SetActive(false);
                sliderSupply.value = 0;
            }



        }
    }
}
