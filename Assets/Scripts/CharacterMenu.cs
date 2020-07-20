﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //Text fields
    public Text levelText, hitpointText, goldText, upgradeCostText, xpText;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    // Character Selection
    public void OnArrowClick(bool right)
    {
        if(right)
        {
            currentCharacterSelection++;

            if(currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnselectionChange();
        }
        else
        {
            currentCharacterSelection--;

            if(currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnselectionChange();
        }
    }

    private void OnselectionChange()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    //Weapon Upgrade
    public void OnUpgradeClick()
    {
        if(GameManager.instance.tryUpgradeWeapon())
            UpdateMenu();
    }

    //Update character info
    public void UpdateMenu()
    {
        //WEAPON
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrice.Count)
            upgradeCostText.text = "MAX";
        
        else
            upgradeCostText.text = GameManager.instance.weaponPrice[GameManager.instance.weapon.weaponLevel].ToString();

        //Meta
        hitpointText.text = GameManager.instance.player.hitpoint.ToString() + "/" + GameManager.instance.player.maxHitpoint.ToString();
        goldText.text = GameManager.instance.gold.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        //xpBar
        int currentLevel = GameManager.instance.GetCurrentLevel();

        if(currentLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString();
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currentLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currentLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + "/" + diff;
        }

    }

}
