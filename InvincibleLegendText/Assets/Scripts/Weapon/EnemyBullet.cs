using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class EnemyBullet : Bullet 
{


    private void Start()
    {

        //OnTriggerEnter();
    }


    //private void FixedUpdate()
    //{
    //    print("敌人子弹位置："+transform.position);
    //    print("玩家位置：" + PlayerStatusInfo.Instance.headTF.position);
    //    if(Vector3.Distance(transform.position, PlayerStatusInfo.Instance.headTF.position) < 1)
    //    {
    //        print("玩家与子弹小于1米");
    //    }
        
    //}

    protected override void Update()
    {
        base.Update();
        if(Vector3.Distance(transform.position, PlayerStatusInfo.Instance.headTF.position) < 0.8)
        {
            PlayerStatusInfo.Instance.Damage(atk);
            Destroy(gameObject);
        }
        
    }

    //private void OnTriggerEnter()
    //{
    //    //如果与玩家接触，玩家减血

    //    if (hit.collider != null && hit.collider.tag == "Player")
    //    {
    //        print("子弹位置：" + EnemyObj.position);
    //        print("人物位置：" + hit.collider.transform.position);
    //        if (Vector3.Distance(hit.collider.transform.position, EnemyObj.position) < 0.2)
    //        {

    //            PlayerStatusInfo.Instance.Damage(atk);
    //        }


    //        //Destroy(gameObject);
    //    }
    //}
}
