using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(Hud);
            Destroy(Menu);
            return;
        }

        instance = this;
        
    }

    // Resources
    public List <Sprite> playerSprites;
    public List <Sprite> weaponSprites;
    public List <int> weaponPrice;
    public List <int> xpTable;

    //References
    public Player player;
    public Sword weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public GameObject Hud;
    public GameObject Menu;
    public Animator deathMenuAnimator;
    public CharacterMenu charMenu;
    BaseDatos baseDatos = new BaseDatos();

    //Logic
    public int gold = 0;
    public int experience = 0;

    public void Start()
    {
        baseDatos.CrearTabla();
    }
    public int GetCurrentScene()
    {
        int currScene = SceneManager.GetActiveScene().buildIndex;
        return currScene;

    }

    //Floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Upgrade weapo
    public bool tryUpgradeWeapon()
    {
        if(weaponPrice.Count <= weapon.weaponLevel)
            return false;

        if(gold >= weaponPrice[weapon.weaponLevel])
        {
            gold -= weaponPrice[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //Hitpoint Bar
    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }

    //Experience system
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if(r == xpTable.Count)
                return r;
        }

        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while(r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }
    public void GrantXp(int xp)
    {
        int currentLevel = GetCurrentLevel();
        experience += xp;
        if(currentLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }
    public void OnLevelUp()
    {
        player.OnLevelUp();
        OnHitpointChange();
        Debug.Log("LevelUp");
    }

    //OnSceneLoaded
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    //Respawn
    public void Respawn()
    {
        deathMenuAnimator.SetTrigger("Hide");
        player.Respawn();
        SceneManager.LoadScene("start");
        
        instance.player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
        Debug.Log("Game saved");
    }
    public void LoadGame()
    {
        GameData data = SaveSystem.LoadGame();

        SceneManager.LoadScene(data.currScene);

        instance.player.hitpoint = data.health;
        experience = data.xpAmount;
        gold = data.gold;
        weapon.SetWeaponLevel(data.weaponLevel);

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        GameManager.instance.player.transform.position = position;

        charMenu.UpdateMenu();

        Debug.Log("GameLoaded");
    }

}
