﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string[] sceneNames;
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            //teleport Player
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            SceneManager.LoadScene(sceneName);
            GameManager.instance.player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        }
    }
}
