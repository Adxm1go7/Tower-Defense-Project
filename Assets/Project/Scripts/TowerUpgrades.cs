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
        
        return currentALevel < maxAUpgrades && pathA.Length > currentALevel;

    }

    public void upgradePathA()
    {
        if (!CanUpgradeA())
        {
            return;

        }

        giveUpgrade(pathA[currentALevel]);
        currentALevel++;
    }


    private void giveUpgrade(TowerUpgradesObj upgrade)
    {
        tower.towerDamage += upgrade.damageInc;
        tower.towerRange += upgrade.rangeInc;
        tower.towerFireRate *= upgrade.fireRate;

    }
}
