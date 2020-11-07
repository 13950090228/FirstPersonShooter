using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class AutomaticGun : Gun
{

    protected override void Start()
    {
        base.Start();

    }

    private int count = 0;
    private void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            count += 1;
            if(count == 5)
            {
                Firing(firePoint.forward);
                count = 0;
            }
            
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
