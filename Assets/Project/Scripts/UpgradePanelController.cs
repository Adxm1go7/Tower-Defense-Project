using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradePanelController : MonoBehaviour
{
    public GameObject upgradePanel; // Reference to the upgrade panel UI
    public TextMeshProUGUI DDTower; //Name of tower text object
    public TextMeshProUGUI SpecialAtk; //Damage dealt by tower text object

    private TowerScript selectedTower; // Currently selected tower

    void Update()
    {
        if (selectedTower != null)
        {
            DDTower.text = "Damage: " + selectedTower.towerDamage.ToString();
            SpecialAtk.text = "Special: ";
        }
    }


    public void OnButtonClick()
    {
        upgradePanel.SetActive(false);
        GameManager.Instance.levelManager.OpenSidePanel();
        GameManager.Instance.cameraController.ResetCamera();

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
