using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪共同类
/// </summary>

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour 
{
    //音频对象
    protected AudioSource audioSource;

    //单例音频触发检测器
    protected AudioTrigger audioS;
    //当前弹匣内子弹数
    public int currentAmmoBullets = 10;

    //弹匣容量
    public int ammoCapacity = 10;

    //剩余子弹数
    public int remainBullets = 60;

    //攻击力
    public int atk = 10;

    //开火点位置
    public Transform firePoint;

    //子弹预制件
    //public GameObject buttetPrefab;

    //开火声音片段
    public AudioClip fireSound;

    private GunAnimation anim;


    public GameObject bullet;
    private MuzzleFlash muzzle;

    //为子类提供重写start方法的机会
    protected virtual void Start()
    {
        anim = GetComponent<GunAnimation>();
        muzzle = GetComponentInChildren<MuzzleFlash>();
        audioSource = GetComponent<AudioSource>();
        audioS = AudioTrigger.GetInstance();
    }



    /// <summary>
    /// 开火
    /// </summary>
    /// <param name="direction">子弹朝向</param>
    public void Firing(Vector3 direction)
    {
        //玩家枪发射：枪口方向
        Vector3 distance = (PlayerStatusInfo.Instance.headTF.position - transform.position).normalized;
        //敌人发射：从枪口位置朝向玩家头部

        //准备子弹

        //判断弹匣是否有子弹
        if (anim!=null && Ready() == false)
        {
            return;
        }

        //创建子弹、播放音频、播放动画

        audioSource.PlayOneShot(fireSound);
        
        if (anim)
        {
            anim.action.PlayQueued(anim.fireAnimName);
            
        }
        muzzle.DisplayFlash();


        GameObject bulletGo = Instantiate(bullet, firePoint.position, Quaternion.LookRotation(direction)) as GameObject;
        bulletGo.GetComponent<Bullet>().atk = atk;
    }

    

    /// <summary>
    /// 准备阶段
    /// </summary>
    /// <returns></returns>
    public bool Ready()
    {
        if(currentAmmoBullets<=0 || anim.action.isPlaying(anim.updateAnimName))
        {
            return false;
        }

        currentAmmoBullets--;
        //remainBullets--;

        if (currentAmmoBullets == 0)
        {
            anim.action.Play(anim.lackAnimName);
        }

        return true;
    }

    /// <summary>
    /// 更换弹匣
    /// </summary>
    public void UpdateAmmo()
    {

        if (remainBullets<=0 || currentAmmoBullets == ammoCapacity)
        {

            return;
        }

        anim.action.Play(anim.updateAnimName);
        if (remainBullets >= ammoCapacity)
        {
            remainBullets -= (ammoCapacity - currentAmmoBullets);
            currentAmmoBullets = ammoCapacity;  

        }
        else if(remainBullets< ammoCapacity && remainBullets > 0)
        {
            if((currentAmmoBullets + remainBullets) > ammoCapacity)
            {
                remainBullets = (currentAmmoBullets + remainBullets) - ammoCapacity;
                currentAmmoBullets = 10;
            }
            else
            {
                currentAmmoBullets += remainBullets;
                remainBullets = 0;
            }
        }

    }
}
