using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 敌人生成器
/// </summary>
public class EnemySpawn : MonoBehaviour
{
    /// <summary>
    /// 开始时需要创建的敌人数量
    /// </summary>
    public int startCount = 2;

    //当前产生的敌人数量
    public  int spawnedCount = 0;

    /// <summary>
    /// 可以产生敌人的最大值
    /// </summary>
    public int maxCount = 10;

    /// <summary>
    /// 产生敌人的最大延迟时间
    /// </summary>
    public int maxDelay = 10;

    /// <summary>
    /// 敌人类型
    /// </summary>
    public GameObject[] enemyTypes;

    private int ResidueEnemy;
    public GameObject door;
    private void Start()
    {

        CalculateWayLines();
        for (int i = 0; i < startCount; i++)
        {
            GenerateEnemy();
        }

    }

    private void Update()
    {
        //ResidueEnemy = maxCount - spawnedCount;
        if (spawnedCount == maxCount)
        {

            Destroy(door);
        }
    }

    //计算路线
    private WayLine[] lines;

    /// <summary>
    /// 获取每条路线中的路点坐标
    /// </summary>
    private void CalculateWayLines()
    {
        //创建一个容量为所有路线数的数组
        lines = new WayLine[transform.childCount];
        for (int i = 0; i < lines.Length; i++)
        {
            //每一条路线变换组件的引用
            Transform waylineTF = transform.GetChild(i);
            //创建存储路点的数组对象
            lines[i] = new WayLine(waylineTF.childCount);
            
            //遍历这一条路线下的所有路点
            for (int pointIndex = 0; pointIndex < waylineTF.childCount; pointIndex++)
            {
                //将这一条路线的路点赋值进数组
                lines[i].Points[pointIndex] = waylineTF.GetChild(pointIndex).position;
            }
        }
    }

    /// <summary>
    /// 选择可以使用的路线
    /// </summary>
    private WayLine[] SelectUsableWayLine()
    {
        List<WayLine> result = new List<WayLine>(lines.Length);

        //遍历左右路线
        foreach (var item in lines)
        {
            //如果可用添加到集合中
            if (item.IsUsable)
            {
                result.Add(item);
            }
        }

        //将集合转化为WayLine数组
        return result.ToArray();
    }

    //敌人创建方法的延时调用
    public void GenerateEnemy()
    {
        //判断敌人是否超过上限
        if (spawnedCount <= maxCount-1)
        {
            spawnedCount++;
            Invoke("CreateEnemy", Random.Range(0, maxDelay));
        }
        else
        {
            return;
        }
        
    }

    private void CreateEnemy()
    {
        //得到左右可以使用的路线
        WayLine[] usableWayLines = SelectUsableWayLine();

        //随机选择一条路线
        WayLine randLine = usableWayLines[Random.Range(0, usableWayLines.Length)];

        //随机敌人对象的随机数
        int randimIndex = Random.Range(0, enemyTypes.Length);

        //创建一个敌人对象
        GameObject go =
            Instantiate(enemyTypes[randimIndex], randLine.Points[0], Quaternion.identity) as GameObject;

        //创建敌人马达，并将随机路线传入敌人马达类
        EnemyMotor motor = go.GetComponent<EnemyMotor>();
        motor.wayline = randLine;

        //这条线创建出敌人后，IsUsable改为false，下个敌人就不会再从这条线出生
        randLine.IsUsable = false;

        //传递生成器对象的引用
        go.GetComponent<EnemyStatusInfo>().spawn = this;

    }









}
