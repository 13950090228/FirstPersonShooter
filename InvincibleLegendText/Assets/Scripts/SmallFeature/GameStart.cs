using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class GameStart : MonoBehaviour 
{
    public GameObject player;
    public GameObject Interface;
    public Camera cameraObj;
    public GameObject Enemy;

    public void PlayGame()
    {

        Destroy(this);
        Destroy(Interface);
        Destroy(cameraObj);
        player.SetActive(true);
        Enemy.SetActive(true);
    }
}
