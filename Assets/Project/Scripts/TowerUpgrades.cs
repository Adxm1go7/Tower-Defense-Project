using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrades : MonoBehaviour
{


    //Two different paths, damage and speed or damage and 
    public TowerUpgradesObj[] pathA;
    public TowerUpgradesObj[] pathB;


    // Maximum amount of upgrades each path can do
    public int maxAUpgrades = 3;
    public int maxBUpgrades = 3;

    //Current path level
    private int currentALevel = 0;
    private int currentBLevel = 0;

    private TowerScript tower;
    // Start is called before the first frame update
    void Start()
    {

        tower = GetComponent<TowerScript>();

    }

    // Update is called once per frame

    //Path A

    public bool CanUpgradeA()
    {
        //Debug.Log("TESTINGINSIT WORKIGN??");
        return pathA != null && currentALevel < maxAUpgrades && currentALevel<pathA.Length && pathA[currentALevel] != null;


    }

    public void upgradePathA()
    {
        if (!CanUpgradeA())
        {
            Debug.Log(pathA);
            return;
        }
        giveUpgrade(pathA[currentALevel]);
        currentALevel++;
        return;
        
    }

    public bool CanUpgradeB()
    {
        //Debug.Log("TESTINGINSIT WORKIGN??");
        return pathB != null && currentBLevel < maxBUpgrades && currentBLevel<pathB.Length && pathB[currentBLevel] != null;

    }

    public void upgradePathB()
    {
        Debug.Log("TESTINGINSIT WORKIGN??");
        if (!CanUpgradeB())
        {
            Debug.Log("NO UPGRADES X???");
            return;
        }
        giveUpgrade(pathB[currentBLevel]);
        currentBLevel++;
        
    }


    private void giveUpgrade(TowerUpgradesObj upgrade)
    {
        Debug.Log("Upgrading tower: " + tower.towerName + " with upgrade: " + upgrade.damageInc);
        tower.towerDamage += upgrade.damageInc;
        tower.towerRange += upgrade.rangeInc;
        tower.towerFireRate *= upgrade.fireRate;

    }
}
