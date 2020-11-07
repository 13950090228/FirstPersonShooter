using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单发枪
/// </summary>

public class SingleGun : Gun
{
    //覆盖父类的start
    //public void Start()
    //{
    //    print("singGun--start");
    //}
    
    protected override void Start()
    {

        base.Start();
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Firing(firePoint.forward);
        }

        if (Input.GetMouseButtonDown(1))
        {
            UpdateAmmo();
        }

        if (audioSource.isPlaying)
        {
            

            audioS.StateRevise(true);

        }
        else
        {
            audioS.StateRevise(false);

        }

    }
}
