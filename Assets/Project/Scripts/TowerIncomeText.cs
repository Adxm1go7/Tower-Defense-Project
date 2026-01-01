using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerIncomeText : MonoBehaviour
{
    public TextMeshProUGUI incomeText;
    public void setIncomeText(int income){
        incomeText.text = income.ToString();
    }
}
