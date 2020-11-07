using UnityEngine;
using System.Collections;

/// <summary>
/// 敌人状态信息类
/// </summary>
public class EnemyStatusInfo : MonoBehaviour
{
    /// <summary>
    /// 当前血量
    /// </summary>
    public float currentHP = 100;
    /// <summary>
    /// 最大血量
    /// </summary>
    public float maxHP = 100;

    private EnemyAI enemyAi;

    public EnemySpawn spawn;

    private void Start()
    {
        enemyAi = GetComponent<EnemyAI>();
    }
    public void Damage(float amount)
    {
        //if (currentHP <= 0) return;

        

        if (currentHP <= 0)
        {
            Death();
            return;
        }
        currentHP -= amount;
        enemyAi.state = EnemyAI.State.Hit;

    }

    public float deathDelay = 0.8f;
    public void Death()
    {

        //播放死亡动画
        var anim = GetComponent<EnemyAnimation>();
        anim.action.Play(anim.deathAnimName);

        //销毁游戏对象
        Destroy(gameObject);

        //重新设置路线状态
        GetComponent<EnemyMotor>().wayline.IsUsable = true;
        spawn.GenerateEnemy();

    }
}
