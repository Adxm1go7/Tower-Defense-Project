using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestorTowerAbility : MonoBehaviour // This script allows a tower to generate income
{
    public TowerIncomeStats incomeData;
    public int storedCoins = 0;
    private TowerIncomeText incomeTextScript;
    public GameObject Canvas;

    float timer = 0f;

    void Start(){
        incomeTextScript = Canvas.GetComponent<TowerIncomeText>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= incomeData.timeInterval){
            timer = 0f;
            GenerateCoins();
        }
    }

    void GenerateCoins(){
        if (storedCoins > incomeData.maxStoredCoins){
            return;
        }

        storedCoins += incomeData.coinsGenerated;
        storedCoins = Mathf.Min(storedCoins, incomeData.maxStoredCoins); // ensures max storage isn't exceeded
        incomeTextScript.setIncomeText(storedCoins);
    }

    public void CollectCoins(){
        GameManager.Instance.addCoins(storedCoins);
        storedCoins = 0;
        incomeTextScript.setIncomeText(storedCoins);
    }

    void OnMouseDown(){
        CollectCoins();
    }
}
