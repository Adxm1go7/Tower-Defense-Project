using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradePanelController : MonoBehaviour
{
    public GameObject upgradePanel; // Reference to the upgrade panel UI
    public TextMeshProUGUI leftUpgradeText; //Name of tower text object
    public TextMeshProUGUI rightUpgradeText; //Damage dealt by tower text object

    public TextMeshProUGUI Upgrade1Text; // Upgrade path A text object
    public TextMeshProUGUI Upgrade2Text; // Upgrade path B text object

    public TextMeshProUGUI Upgrade3Text; // Upgrade path C text object [HERO ONLY]
    public TextMeshProUGUI leftUpgradeCostText; // Upgrade path A cost text object

    public TextMeshProUGUI rightUpgradeCostText; // Upgrade path B cost text object

    public TextMeshProUGUI middleUpgradeText; // Upgrade path C for HERO ONLY
    public TextMeshProUGUI middleUpgradeCostText; // Upgrade path C cost HERO ONLY
    private TowerScript selectedTower; // Currently selected tower

    private PlayerAttack2 selectedHero;
    private HeroUpgrader heroUpgrades;


    void Update()
    {
        //Tower Only
        if (selectedTower != null)
        {
            leftUpgradeText.text = "Left Upgrade: " + selectedTower.towerUppies.GetALevel().ToString() + " / 3";
            rightUpgradeText.text = "Right Upgrade: " + selectedTower.towerUppies.GetBLevel().ToString() + " / 3";
            leftUpgradeCostText.text = selectedTower.towerUppies.pathA[0].cost.ToString();
            rightUpgradeCostText.text = selectedTower.towerUppies.pathB[0].cost.ToString();
        }

        if (selectedHero != null && heroUpgrades != null)
        {
            leftUpgradeText.text = $"Path A: {heroUpgrades.GetLevelA()} / 3";
            rightUpgradeText.text = $"Path B: {heroUpgrades.GetLevelB()} / 3";
            middleUpgradeText.text = $"Path C: {heroUpgrades.GetLevelC()} / 3";
        }
    }


    public void OnButtonClick()
    {
        upgradePanel.SetActive(false);
        selectedHero = null;
        GameManager.Instance.levelManager.OpenSidePanel();
        GameManager.Instance.cameraController.ResetCamera();

    }

    public void SetSelectedTower(TowerScript tower)
    {
        selectedTower = tower;
        if (selectedTower.towerName == "Archer")
        {
            Upgrade1Text.text = "Increase Damage";
            Upgrade2Text.text = "Increase FireRate";
        }else if (selectedTower.towerName == "Sniper")
        {
            Upgrade1Text.text = "Increase Damage";
            Upgrade2Text.text = "Increase FireRate";
        }else if (selectedTower.towerName == "Freeze")
        {
            Upgrade1Text.text = "Increase Damage & Slowness";
            Upgrade2Text.text = "Increase FireRate & Slow Duration";
        }else if (selectedTower.towerName == "Flame Thrower")
        {
            Upgrade1Text.text = "Increase Damage & Burn";
            Upgrade2Text.text = "Increase FireRate";
        }else if (selectedTower.towerName == "Swordsman")
        {
            Upgrade1Text.text = "Increase Damage & Range";
            Upgrade2Text.text = "Increase FireRate";
        }else if (selectedTower.towerName == "Hammer")
        {
            Upgrade1Text.text = "Increase Damage";
            Upgrade2Text.text = "Increase FireRate & Range";
        }else if (selectedTower.towerName == "Lightning")
        {
            Upgrade1Text.text = "Increase Damage & Max Chains";
            Upgrade2Text.text = "Increase FireRate & Range";
        }else if (selectedTower.towerName == "Investor")
        {
            Upgrade1Text.text = "Increase Generation Speed";
            Upgrade2Text.text = "Increase Coins Generated & Max Coins";
        }
    }

    public void SetSelectedHero(PlayerAttack2 hero)
    {
        selectedTower = null;
        selectedHero = hero;
        heroUpgrades = hero.GetComponent<HeroUpgrader>();

        Upgrade1Text.text = "Single Target Damage";
        Upgrade2Text.text = "AOE Damage";
        Upgrade3Text.text = "Fire Rate";

        upgradePanel.SetActive(true);
    }

    public void UpgradeHeroPathA()
    {
        if (heroUpgrades != null)
        {
            heroUpgrades.UpgradeA();
        }
    }

    public void UpgradeHeroPathB()
    {
        if (heroUpgrades != null)
        {
            heroUpgrades.UpgradeB();
        }
    }

    public void UpgradeHeroPathC()
    {
        if (heroUpgrades != null)
        {
            heroUpgrades.UpgradeC();
        }
    }

    public void UpgradeTowerPathA()
    {
        Debug.Log("Upgrade Tower button A clicked");
        if (selectedTower != null && selectedTower.towerUppies != null)
        {
            Debug.Log("Attempting to upgrade tower: " + selectedTower.towerName);
            selectedTower.towerUppies.upgradePathA();
            Debug.Log(selectedTower.towerDamage);
        }
    }

    public void UpgradeTowerPathB()
    {
        Debug.Log("Upgrade Tower button B clicked");
        if (selectedTower != null && selectedTower.towerUppies != null)
        {
            Debug.Log("Attempting to upgrade tower: " + selectedTower.towerName);
            selectedTower.towerUppies.upgradePathB();
        }
    }


}
