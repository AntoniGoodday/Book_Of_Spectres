using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldDialogue : MonoBehaviour
{
    public string dialogueKnot;
    InkDialogueManager inkDialogue;

    public void StartDialogue()
    {
        if(inkDialogue == null)
        {
            inkDialogue = InkDialogueManager.Instance;
            Debug.Log("found dialogue manager");
        }

        inkDialogue.StartDialogue(dialogueKnot);
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartDialogue();
    }
}
