using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData 
{
    public int health;
    public int level;
    public int xpAmount;
    public int gold;

    public int weaponLevel;

    public float[] position;
    public int currScene;

    public GameData (GameManager GameManager)
    {
        health = GameManager.instance.player.hitpoint;
        xpAmount = GameManager.instance.experience;
        gold = GameManager.instance.gold;
        currScene = GameManager.instance.GetCurrentScene();

        weaponLevel = GameManager.instance.weapon.weaponLevel;

        position = new float[3];
        position[0] = GameManager.instance.player.transform.position.x;
        position[1] = GameManager.instance.player.transform.position.y;
        position[2] = GameManager.instance.player.transform.position.z;
    }
    
}
