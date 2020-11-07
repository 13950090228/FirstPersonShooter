using UnityEngine;
using System.Collections;
using UnityEngine.AI;
/// <summary>
/// 人工智能
/// </summary>

[RequireComponent(typeof(EnemyAnimation))]
[RequireComponent(typeof(EnemyMotor))]
[RequireComponent(typeof(EnemyStatusInfo))]
public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// 敌人状态
    /// </summary>
    public enum State
    {
        /// <summary>
        /// 攻击状态
        /// </summary>
        Attack,
        /// <summary>
        /// 死亡状态
        /// </summary>
        Death,
        /// <summary>
        /// 寻路状态
        /// </summary>
        Pathfinding,
        /// <summary>
        /// 游荡状态
        /// </summary>
        Wander,
        /// <summary>
        /// 受击状态
        /// </summary>
        Hit,



    }
    private AudioTrigger audioS;
    private EnemyStatusInfo statusInfo;
    private EnemyMotor motor;
    private EnemyAnimation anim;
    private float atKTimer;
    private Gun gun;

    /// <summary>
    /// 
    /// </summary>
    private Transform[] directPoints;
    private int index = 0;             //路点数     用于刷新寻路目标
    private NavMeshAgent navMeshAgent;
    private int pointCount;            //路点长度   用于判断长度
    private float timer = 0;           //计时器
    public float patroTime =0.5f;       //到达某一点停止等待时间
    //攻击间隔
    public float atKInterval = 3;

    private void Start()
    {
        audioS = AudioTrigger.GetInstance();
        statusInfo = GetComponent<EnemyStatusInfo>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<EnemyAnimation>();
        motor = GetComponent<EnemyMotor>();
        gun = GetComponentInChildren<Gun>();
        for (int i = 0; i < motor.wayline.Points.Length; i++)
        {

            //directPoints[i].position = motor.wayline.Points[i];
        }
        pointCount = motor.wayline.Points.Length;

    }

    /// <summary>
    /// 敌人状态默认寻路
    /// </summary>
    public State state = State.Pathfinding;

    private void Update()
    {
        switch (state)
        {
            case State.Pathfinding:
                PathFinding();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Wander:
                Wander();
                break;
            case State.Hit:
                Hit();
                break;

        }
        if (audioS.GetState() && Vector3.Distance(transform.position, PlayerStatusInfo.Instance.headTF.position) < 15)
        {
            print("音频被触发！");
            state = State.Hit;
        }
    }

    /// <summary>
    /// 跑步动画
    /// </summary>
    private void PathFinding()
    {

        anim.action.Play(anim.runAnimName);
        //如果跑步动画没在播就转为游荡状态
        if (motor.Pathfinding() == false)
        {
            state = State.Wander;
        };
    }

    /// <summary>
    /// 敌人受伤进行反击
    /// </summary>
    public void Hit()
    {

        navMeshAgent.isStopped = true;
        motor.LookRotation(PlayerStatusInfo.Instance.headTF.position);

        if (atKTimer < Time.time)
        {
            anim.action.Play(anim.shootingAnimName);
            atKTimer = Time.time + atKInterval;
            gun.Firing(PlayerStatusInfo.Instance.headTF.position - gun.firePoint.position);
        }

        StartCoroutine(IsHit());

    }


    

    private IEnumerator IsHit()
    {
        float currentHP = statusInfo.currentHP;
        yield return new WaitForSeconds(5);
        if(currentHP== statusInfo.currentHP)
        {
            state = State.Wander;
        }
    }

    /// <summary>
    /// 游荡动画
    /// </summary>
    private void Wander()
    {
        navMeshAgent.isStopped = false;
        //周围没敌人时四处走动
        anim.action.Play(anim.runAnimName);

        //navMeshAgent.destination = motor.wayline.Points[index];
        navMeshAgent.SetDestination(motor.wayline.Points[index]);

        if (navMeshAgent.remainingDistance < 0.5f)

        {

            timer += Time.deltaTime;

            if (timer >= patroTime)

            {

                index++;

                index %= pointCount;//在4个点之间循环巡逻

                timer = 0;

 

                navMeshAgent.SetDestination(motor.wayline.Points[index]);

            }

        }

        //当敌人在一定范围内播放攻击动画
        if (Vector3.Distance(transform.position, PlayerStatusInfo.Instance.headTF.position) < 10)
        {
            navMeshAgent.isStopped = true;
            state = State.Attack;
        }
    }

    /// <summary>
    /// 攻击动画
    /// </summary>
    private void Attack()
    {

        //如果在攻击范围内则进行攻击
        //if (Vector3.Distance(transform.position, PlayerStatusInfo.Instance.headTF.position) < 10)
        //{
        motor.LookRotation(PlayerStatusInfo.Instance.headTF.position);


        if (atKTimer < Time.time)
        {
            anim.action.Play(anim.shootingAnimName);
            atKTimer = Time.time + atKInterval;
            gun.Firing(PlayerStatusInfo.Instance.headTF.position - gun.firePoint.position);
        }
        //}
        if (Vector3.Distance(transform.position, PlayerStatusInfo.Instance.headTF.position) > 10)
        {
            //如果没在攻击范围内则转为游荡状态
            state = State.Wander;
        }

    }
}
