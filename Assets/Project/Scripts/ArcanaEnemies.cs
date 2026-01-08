using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcanaEnemies : MonoBehaviour
{
    public GameObject BasicSlymeInfo;
    public GameObject SpeederSlymeInfo;
    public GameObject TankSlymeInfo;
    public GameObject AllomancerSlymeInfo;
    public GameObject GigaTankSlymeInfo;
    public GameObject MotherSlymeInfo;
    public GameObject SuperSpeederInfo;
    public GameObject JesterSlymeInfo;
    public GameObject BossSlymeInfo;

    public void OpenBasicSlymeInfo()
    {
        CloseInfoPanels();
        BasicSlymeInfo.SetActive(true);
    }
    public void OpenSpeederSlymeInfo()
    {
        CloseInfoPanels();
        SpeederSlymeInfo.SetActive(true);
    }
    public void OpenTankSlymeInfo()
    {
        CloseInfoPanels();
        TankSlymeInfo.SetActive(true);
    }
    public void OpenAllomancerSlymeInfo()
    {
        CloseInfoPanels();
        AllomancerSlymeInfo.SetActive(true);
    }
    public void OpenGigaTankSlymeInfo()
    {
        CloseInfoPanels();
        GigaTankSlymeInfo.SetActive(true);
    }
    public void OpenMotherSlymeInfo()
    {
        CloseInfoPanels();
        MotherSlymeInfo.SetActive(true);
    }
    public void OpenSuperSpeederInfo()
    {
        CloseInfoPanels();
        SuperSpeederInfo.SetActive(true);
    }
    public void OpenJesterSlymeInfo()
    {
        CloseInfoPanels();
        JesterSlymeInfo.SetActive(true);
    }
    public void OpenBossSlymeInfo()
    {
        CloseInfoPanels();
        BossSlymeInfo.SetActive(true);
    }
    
    public void CloseInfoPanels()
    {
        BasicSlymeInfo.SetActive(false);
        SpeederSlymeInfo.SetActive(false);
        TankSlymeInfo.SetActive(false);
        AllomancerSlymeInfo.SetActive(false);
        GigaTankSlymeInfo.SetActive(false);
        MotherSlymeInfo.SetActive(false);
        SuperSpeederInfo.SetActive(false);
        JesterSlymeInfo.SetActive(false);
        BossSlymeInfo.SetActive(false);
    }

    public void ReturnButton()
    {
        CloseInfoPanels();
        SceneStackManager.Instance.ReturnToPreviousScene(); // Load home menu scene
    }
}
