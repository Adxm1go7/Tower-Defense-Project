using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelController : MonoBehaviour
{
    public GameObject upgradePanel; // Reference to the upgrade panel UI
    public GameObject towerNameObject; // Reference to the tower name UI object
    public GameObject damageDealtObject; // Reference to the damage dealt UI object
    private TowerScript selectedTower; // Currently selected tower
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

    public void UpgradeTower()
    {
        Debug.Log("Upgrade Tower button clicked");
    }


}
