using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelController : MonoBehaviour
{
    public GameObject upgradePanel; // Reference to the upgrade panel UI
    public GameObject towerNameObject; // Reference to the tower name UI object
    public GameObject damageDealtObject; // Reference to the damage dealt UI object
    public void OnButtonClick()
    {
        upgradePanel.SetActive(false);
        towerNameObject.SetActive(false);
        damageDealtObject.SetActive(false);

    }
}
