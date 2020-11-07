using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
[System.Serializable]
public class DialogueClass
{
    public SpeakerAsset[] speakerInfo;
    public Expression[] setExpression;
    public bool[] holdExpression;
    [TextArea]
    public string[] speakerDialogue;
    public DialogueBoxPosition[] location;
}
