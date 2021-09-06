using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    // text fields
    public Text levelText, hitpointText, pesosText, upgradeCostText, xpText;
    public Text healthBarText;
    public Text weaponDamageText;

    // logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;
    public RectTransform healthBar;
    private int currentLevel = 1;

    public void FixedUpdate()
    {
        LevelUp();
        // health Bar
        healthBarText.text = GameManager.instance.player.hitpoint.ToString() + "/" + GameManager.instance.player.maxHitpoint;

        float percent;
        float maxHealth = GameManager.instance.player.maxHitpoint * 1.0f;
        percent = GameManager.instance.player.hitpoint / maxHealth;

        healthBar.sizeDelta = new Vector2(percent * 710, 56);
    }

    // Character Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            if (++currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChanged();
        }
        else
        {
            if (--currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnSelectionChanged();
        }
    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
    }

    // Update the caracter info
    public void UpdateMenu()
    {
        // weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel < GameManager.instance.weaponSprites.Count - 1)
        {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }
        else
        {
            upgradeCostText.text = "MAX LEVEL";
        }
        

        // meta
        hitpointText.text = GameManager.instance.player.hitpoint.ToString() + "/" + GameManager.instance.player.maxHitpoint;
        pesosText.text = GameManager.instance.pesos.ToString() + " rsd";
        

        int currentLevel = GameManager.instance.GetCurrentLevel();
        

        if (GameManager.instance.experience > GameManager.instance.GetXpToLevel(GameManager.instance.maxLevel)) 
        {
            xpText.text = "MAX XP";
            levelText.text = (currentLevel + 1).ToString();
        }
        else
        {
            xpText.text = "XP: " + GameManager.instance.experience.ToString() + "/" + GameManager.instance.GetXpToLevel(currentLevel);
            levelText.text = currentLevel.ToString();
        }
        

        // xp bar
        float percent;
        float maxXp = GameManager.instance.GetXpToLevel(int.Parse(levelText.text)) * 1.0f;

        percent = GameManager.instance.experience / (maxXp * 1.0f);

        if(percent <= 1)
        {
            xpBar.sizeDelta = new Vector2(percent * 760, 52);
        }
        else
        {
            xpBar.sizeDelta = new Vector2(760, 52);
        }

        // weapon damage text
        weaponDamageText.text = "Damage: " + GameManager.instance.weapon.damagePoint[GameManager.instance.weapon.weaponLevel];
        

    }

    public void LevelUp()
    {
        int level = GameManager.instance.GetCurrentLevel();
        while(currentLevel < level)
        {
            Debug.Log(currentLevel);
            Debug.Log(level);
            GameManager.instance.player.maxHitpoint += GameManager.instance.healtIncrease[currentLevel - 1];
            GameManager.instance.player.hitpoint += GameManager.instance.healtIncrease[currentLevel - 1];
            currentLevel++;
        }
    }
   

}
