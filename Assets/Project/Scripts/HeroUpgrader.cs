using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroUpgrader : MonoBehaviour
{

    public HeroUpgrades[] pathA;
    public HeroUpgrades[] pathB;
    

    public int maxA = 3;
    public int maxB = 3;
    
    
    private int currentLevelA = 0;
    private int currentLevelB = 0;
    
    private PlayerAttack2 hero;
    // Start is called before the first frame update
    void Awake()
    {
        hero = GetComponent<PlayerAttack2>();
    }

    // Update is called once per frame

    public bool CanUpgradeA() => CanUpgrade(pathA, currentLevelA, maxA);
    public bool CanUpgradeB() => CanUpgrade(pathB, currentLevelB, maxB);
    

    bool CanUpgrade(HeroUpgrades[] path, int level, int max)
    {
        return path != null &&
               level < max &&
               level < path.Length &&
               GameManager.Instance.canPlaceTower(path[level].cost);
    }


    public void UpgradeA()
    {
        Debug.Log("BUTONNA AA PRESED");
        Upgrade(pathA, ref currentLevelA);
    }
    public void UpgradeB()
    {
        Upgrade(pathB, ref currentLevelB);
    }
    

    public void Upgrade(HeroUpgrades[] path, ref int level)
    {
        if (!CanUpgrade(path, level, int.MaxValue)) return;
        HeroUpgrades he = path[level];
        hero.ApplyUpgrade(he);
        GameManager.Instance.deductCoins(he.cost);

        level++;
    }
    public int GetLevelA() => currentLevelA;
    public int GetLevelB() => currentLevelB;
    
}
