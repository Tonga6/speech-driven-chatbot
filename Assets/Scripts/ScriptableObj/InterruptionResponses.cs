using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InterruptionClips", order = 1)]
public class InterruptionResponses : ScriptableObject
{
    public List<AudioClip> interruptions = new List<AudioClip>();
    public List<AudioClip> gateways = new List<AudioClip>();
}
