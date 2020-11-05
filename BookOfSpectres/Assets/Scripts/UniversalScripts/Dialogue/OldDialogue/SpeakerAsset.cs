using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Dialogue/SpeakerAsset")]
public class SpeakerAsset : ScriptableObject
{
    public string speakerName;
    public Sprite[] expressionSprites;
}
