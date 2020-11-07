using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ink.Runtime;
using TMPro;
public class SimpleText : MonoBehaviour
{
    public UnityEvent setTextEvent;
    public string knotString;
    public Story story;
    TextMeshProUGUI tmp;
    AnimatedTextTyper textAnim;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        textAnim = GetComponent<AnimatedTextTyper>();
        if (InkDialogueManager.Instance != null)
        {
            story = new Story(InkDialogueManager.Instance.inkJSONAsset.text);
        }
    }

    private void OnEnable()
    {
        CombatMenu.OnMenuPaused += SetFlavourText;
    }

    private void OnDisable()
    {
        CombatMenu.OnMenuPaused -= SetFlavourText;
    }

    void SetFlavourText()
    {
        if (story == null)
        {
            story = new Story(InkDialogueManager.Instance.inkJSONAsset.text);
        }

        story.ChoosePathString(knotString);
        if (story.canContinue)
        {
            // Continue gets the next line of the story
            string _text = story.Continue();
            // This removes any white space from the text.
            _text = _text.Trim();

            textAnim.ShowText(_text);

            textAnim.StartShowingText();

            textAnim.SetTypewriterSpeed(2);

        }
    }
}
