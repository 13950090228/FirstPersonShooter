using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家子弹
/// </summary>

public class PlayerBullet : Bullet 
{

    //根据敌人部位减血
    private void Start()
    {
        CalculateTargetPoint();
        
        //击中部位的名称
        //base.hit.collider.name;

        float atK = CalculateAttackForce();

        
        if (hit.collider != null && hit.collider.tag=="Enemy")
        {

            //Debug.Log("攻击力"+atk);
            //print(hit.collider.name);

            hit.collider.GetComponentInParent<EnemyStatusInfo>().Damage(atK);
        }
        
    }

    private float CalculateAttackForce()
    {
        switch (hit.collider.name)
        {
            case "Coll_Head":
                return atk * 2;
            case "Coll_Body":
                return atk;

            default:
                return atk;
        }
    }
}
