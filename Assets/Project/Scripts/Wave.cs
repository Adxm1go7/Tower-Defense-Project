// Scriptable object to store a wave composed of wave chunks

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "WaveData/Wave")]
public class Wave : ScriptableObject
{
    public List<WaveChunk> waveChunks;
    public float TimeBeforeNextWave = 5f;
}