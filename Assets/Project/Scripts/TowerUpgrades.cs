using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrades : MonoBehaviour
{


    //Two different paths, damage and speed or damage and 

    //Creating a list of upgrades, so it is easier to manage
    public TowerUpgradesObj[] pathA;
    public TowerUpgradesObj[] pathB;

    //public GameManager manager;


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

    //Upgrade path A
    public void upgradePathA()
    {
        if (!CanUpgradeA())
        {
            //Debug.Log(tower.towerName + " cannot upgrade A anymore");
            //Debug.Log(pathA);
            return;
        }
        giveUpgrade(pathA[currentALevel], "A");
        currentALevel++;
        return;
        
    }

    public bool CanUpgradeB()
    {
        //Debug.Log("TESTINGINSIT WORKIGN??");
        return pathB != null && currentBLevel < maxBUpgrades && currentBLevel<pathB.Length && pathB[currentBLevel] != null;

    }

    //Upgrade path B
    public void upgradePathB()
    {
        //Debug.Log("TESTINGINSIT WORKIGN??");
        if (!CanUpgradeB())
        {
            //Debug.Log("NO UPGRADES X???");
            return;
        }
        giveUpgrade(pathB[currentBLevel], "B");
        currentBLevel++;
        
    }


    private void giveUpgrade(TowerUpgradesObj upgrade, string path)
    {
        //Adding upgrades
        //Check coins are above upgrade cost
        if (GameManager.Instance.canPlaceTower(upgrade.cost))
        {
            //Debug.Log("Upgrading tower: " + tower.towerName + " with upgrade: " + upgrade.damageInc);
            tower.towerDamage += upgrade.damageInc;
            tower.towerRange += upgrade.rangeInc;
            tower.towerFireRate -= upgrade.fireRate;
            GameManager.Instance.deductCoins(upgrade.cost);
        
            //Investor tower upgrade specific
            ifInvestor(path);
            //Debug.Log("TEST");
            ifFlamethrower(path);
            ifFreeze(path);
        }
    }



    //Lightning Tower
    private void ifLightning(string path)
    {
        if (tower.towerName == "Lightning")
        {
            LightningTowerScript lightning = tower.GetComponent<LightningTowerScript>();

            if (lightning != null)
            {
                if (path == "A")
                {
                    lightning.maxChains += 1;

                }
                else if (path == "B")
                {
                    lightning.chainRadius += 0.5f;
                }
            }
        }
    }

    //Freeze tower specific
    private void ifFreeze(string path)
    {
        if (tower.towerName == "Freeze")
        {
            FreezeTowerScript freeze = tower.GetComponent<FreezeTowerScript>();
            if (freeze != null)
            {
                if (path == "A")
                {
                    freeze.coneAngle += 5f;
                    freeze.slowDownMult -= 0.05f;
                    
                }
                else if (path == "B")
                {
                    freeze.slowDuration += 0.4f;
                }
            }
        }
    }

    private void ifFlamethrower(string path)
    {
        //Debug.Log("TEST");
        if (tower.towerName == "Flame Thrower")
        {
            FlameThrowerTowerScript flame = tower.GetComponent<FlameThrowerTowerScript>();
            if (flame != null)
            {
                if (path == "A")
                {
                    flame.flameBurnDamagePerTick += 1;
                }
                else if (path == "B")
                {
                    flame.burstDuration += 0.25f;
                    flame.coneAngle += 4;
                    flame.burstInterval -= 0.35f;
                }
                Debug.Log(flame.burstInterval);
            
            }
            
        }
    }

    private void ifInvestor(string path)
    { 
        if (tower.towerName == "Investor")
        {
            //Debug.Log("TESTTTT");
            InvestorTowerAbility investor = tower.GetComponent<InvestorTowerAbility>();
            if (investor != null)
            {
                if (path == "A")
                {
                    investor.incomeData.timeInterval -= 0.5f;
                }
                else if (path == "B")
                {
                    investor.incomeData.coinsGenerated += 1;
                    investor.incomeData.maxStoredCoins += 5;
                    
                }

            }
            //Debug.Log(investor.incomeData.timeInterval);
            //Debug.Log(investor.incomeData.coinsGenerated);
        }

    }

    public int GetALevel(){
        return currentALevel;
    }

    public int GetBLevel(){
        return currentBLevel;
    }
}
