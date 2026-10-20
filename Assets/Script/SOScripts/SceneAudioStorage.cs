using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneAudioStorage", menuName = "Scriptable Objects/Scene Audio Storage")]
public class SceneAudioStorage : ScriptableObject
{
    [SerializeField]List<AudioClip> clip_SceneMusics;
}
