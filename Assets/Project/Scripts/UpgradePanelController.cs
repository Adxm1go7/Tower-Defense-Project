using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradePanelController : MonoBehaviour
{
    public GameObject upgradePanel; // Reference to the upgrade panel UI
    public GameObject towerNameObject; // Reference to the tower name UI object
    public GameObject damageDealtObject; // Reference to the damage dealt UI object
    public TextMeshPro DDTower; //Damage dealt by tower text object


    private TowerScript selectedTower; // Currently selected tower

    void Update()
    {
        // This method can be used to initialize or update the upgrade panel if needed
        DDTower.text = "Δ" + selectedTower.towerDamage.ToString();
    }

    public void OnButtonClick()
    {
        upgradePanel.SetActive(false);
        towerNameObject.SetActive(false);
        damageDealtObject.SetActive(false);

    }

    public void SetSelectedTower(TowerScript tower)
    {
        selectedTower = tower;
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
