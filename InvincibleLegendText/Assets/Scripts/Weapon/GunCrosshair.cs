using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 枪准心
/// </summary>

public class GunCrosshair : MonoBehaviour 
{
    private RaycastHit hit;
    public LayerMask layer;
    public Transform gunTans;
    public RectTransform ui;
    private LineRenderer line;



    public void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    public void Update()
    {
        if (Physics.Raycast(gunTans.position, gunTans.forward, out hit, layer.value))
        {
            Debug.DrawLine(gunTans.position, hit.point, Color.red);
            Vector3 gunpoint = Camera.main.WorldToScreenPoint(hit.point);

            ui.position = gunpoint;
            ui.gameObject.SetActive(true);

            line.enabled = true;  //单独组件得显隐
            line.SetPosition(0, gunTans.position);
            line.SetPosition(1, hit.point);

        }
        else
        {
            ui.gameObject.SetActive(false);
            line.enabled = false;

        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    carryObj.parent = null;
        //    carryObj.localEulerAngles = new Vector3(0, carryObj.transform.position.y, -180);
        //}
    }
}
