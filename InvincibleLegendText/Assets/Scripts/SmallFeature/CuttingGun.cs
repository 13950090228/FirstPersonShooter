using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 切枪类
/// </summary>

public class CuttingGun : MonoBehaviour 
{
    public GameObject gun01;
    public GameObject gun02;
    private bool isMain = true; 

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isMain == true)
            {
                gun02.SetActive(false);
                gun01.SetActive(true);
                isMain = false;
            }
            else
            {
                gun02.SetActive(true);
                gun01.SetActive(false);
                isMain = true;
            }
        }

    }
}
