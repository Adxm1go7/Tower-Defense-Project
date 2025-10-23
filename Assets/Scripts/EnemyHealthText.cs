using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthText : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    public void setHealthText(float health){
        healthText.text = health.ToString();
    } 
}
