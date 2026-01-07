using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcanaTowers : MonoBehaviour
{
    public GameObject ArcherTowerInfo;
    public GameObject SniperTowerInfo;
    public GameObject FlameTowerInfo;
    public GameObject FreezeTowerInfo;
    public GameObject HammerTowerInfo;
    public GameObject LightningTowerInfo;
    public GameObject InvestorTowerInfo;
    public GameObject SwordTowerInfo;

    public void OpenArcherTowerInfo()
    {
        CloseInfoPanels();
        ArcherTowerInfo.SetActive(true);
    }
    public void OpenSniperTowerInfo()
    {
        CloseInfoPanels();
        SniperTowerInfo.SetActive(true);
    }
    public void OpenFlameTowerInfo()
    {
        CloseInfoPanels();
        FlameTowerInfo.SetActive(true);
    }
    public void OpenLightningTowerInfo()
    {
        CloseInfoPanels();
        LightningTowerInfo.SetActive(true);
    }
    public void OpenFreezeTowerInfo()
    {
        CloseInfoPanels();
        FreezeTowerInfo.SetActive(true);
    }
    public void OpenHammerTowerInfo()
    {
        CloseInfoPanels();
        HammerTowerInfo.SetActive(true);
    }
    public void OpenInvestorTowerInfo()
    {
        CloseInfoPanels();
        InvestorTowerInfo.SetActive(true);
    }
    public void OpenSwordTowerInfo()
    {
        CloseInfoPanels();
        SwordTowerInfo.SetActive(true);
    }
    
    public void CloseInfoPanels()
    {
        ArcherTowerInfo.SetActive(false);
        SniperTowerInfo.SetActive(false);
        FlameTowerInfo.SetActive(false);
        LightningTowerInfo.SetActive(false);
        FreezeTowerInfo.SetActive(false);
        HammerTowerInfo.SetActive(false);
        InvestorTowerInfo.SetActive(false);
        SwordTowerInfo.SetActive(false);
    }

    public void ReturnButton()
    {
        CloseInfoPanels();
        SceneStackManager.Instance.ReturnToPreviousScene(); // Load home menu scene
    }
}
