using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹，定义子弹共有行为
/// </summary>

public class Bullet : MonoBehaviour 
{
    [HideInInspector]
    public float attackDistance = 200;
    public LayerMask mask;

    //[HideInInspector]
    protected RaycastHit hit;

    public float speed = 100;
    public Vector3 targetPoint;

    [HideInInspector]
    public Transform EnemyObj;
    //攻击力
    [HideInInspector]
    public int atk = 10;



    protected virtual void Update()
    {
        CalculateTargetPoint();
        Movement();
        EnemyObj = transform;
        if ((transform.position - targetPoint).sqrMagnitude < 0.1f)
        {
            GenerateContactEffect();
            Destroy(gameObject);
            
        }
    }

    //计算目标点
    public void CalculateTargetPoint()
    {
        if(Physics.Raycast(transform.position, transform.forward,out hit, attackDistance, mask))
        {

            targetPoint = hit.point;
        }
        else
        {

            targetPoint = transform.TransformPoint(0, 0, attackDistance);
        }
    }

    //移动
    private void Movement()
    {

        transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
        EnemyObj = transform;
    }

    



    //特效创建
    public void GenerateContactEffect()
    {

        //根据目标物体标签，加相应特效
        if (hit.collider == null)
        {
            return;
        }

        //通过代码读取资源
        //资源必须放到Resources目录下
        string str = "ContactEffects/Effects" + hit.collider.tag;
        GameObject go = Resources.Load<GameObject>(str);

        if (go)
        {               //资源预设  ，目标点位置  向法线方向移动0.01m ,z轴朝向法线方向
            Instantiate(go, targetPoint+hit.normal*0.01f,Quaternion.LookRotation(hit.normal));
        }
    }
}
