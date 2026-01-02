// Scriptable object used to store wave sequences (Levels)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "WaveData/Level")]
public class WavesSequence : ScriptableObject
{
    public List<Wave> waves;
}