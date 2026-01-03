// Scriptable object for the investor tower

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Income Stats", menuName = "Tower/Income Stats")]
public class TowerIncomeStats : ScriptableObject
{
    public int coinsGenerated;
    public float timeInterval;
    public int maxStoredCoins;
}
