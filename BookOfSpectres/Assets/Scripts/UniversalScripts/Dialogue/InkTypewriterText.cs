using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedBlueGames.Tools.TextTyper;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using EnumScript;
public class InkTypewriterText : MonoBehaviour
{
    [SerializeField]
    private AudioClip printSoundEffect;

    [Header("UI References")]

    [SerializeField]
    private Button printNextButton;

    [SerializeField]
    private Button printNoSkipButton; 

    [SerializeField]
    Image[] imagePoints;

    [SerializeField]
    TextMeshProUGUI[] namePoints;

    private Queue<string> dialogueLines = new Queue<string>();

    [SerializeField]
    private TextTyper textTyper;

    int currentLineNumber;

    public int changeExpression;
    public int changeSpeaker;

    [SerializeField]
    SpeakerAsset currentSpeaker;

    [SerializeField]
    private TextAsset inkJSONAsset = null;
    [SerializeField]
    public Story story;
    [SerializeField]
    List<Button> choiceButtons;

    List<GameObject> activateButtons = new List<GameObject>();

    public bool endDialogue = false;

    public DialogueBoxPosition dialogueBoxPosition = DialogueBoxPosition.Left;
    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSONAsset.text);

        currentSpeaker = Resources.Load<SpeakerAsset>("DialogueSpeakers/PhoebeDialogue");

        currentLineNumber = -1;
        this.textTyper.PrintCompleted.AddListener(this.HandlePrintCompleted);
        this.textTyper.CharacterPrinted.AddListener(this.HandleCharacterPrinted);

        //this.printNextButton.onClick.AddListener(this.HandlePrintNextClicked);
        //this.printNoSkipButton.onClick.AddListener(this.HandlePrintNoSkipClicked);

        



        /*for (int i = 0; i < _dialogueClass.speakerDialogue.Length; i++)
        {
            _tempArray[i] = AlignToSide(i) + _dialogueClass.speakerDialogue[i];
        }

        foreach (string s in _tempArray)
        {
            dialogueLines.Enqueue(s);
        }*/

        InkStory();

        /*foreach (string s in _dialogueClass.speakerDialogue)
        {
            dialogueLines.Enqueue(s);
        }*/

        /*dialogueLines.Enqueue("Hello! My name is... <delay=0.5>NPC</delay>. Got it, <i>bub</i>?");
        dialogueLines.Enqueue("You can <b>use</b> <i>uGUI</i> <size=40>text</size> <size=20>tag</size> and <color=#ff0000ff>color</color> tag <color=#00ff00ff>like this</color>.");
        dialogueLines.Enqueue("bold <b>text</b> test <b>bold</b> text <b>test</b>");
        dialogueLines.Enqueue("You can <size=40>size 40</size> and <size=20>size 20</size>");
        dialogueLines.Enqueue("You can <color=#ff0000ff>color</color> tag <color=#00ff00ff>like this</color>.");
        dialogueLines.Enqueue("Sample Shake Animations: <anim=lightrot>Light Rotation</anim>, <anim=lightpos>Light Position</anim>, <anim=fullshake>Full Shake</anim>\nSample Curve Animations: <animation=slowsine>Slow Sine</animation>, <animation=bounce>Bounce Bounce</animation>, <animation=crazyflip>Crazy Flip</animation>");*/

        ShowScript();

        SetExpression();

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tilde))
        {

            var tag = RichTextTag.ParseNext("blah<color=red>boo</color");
            LogTag(tag);
            tag = RichTextTag.ParseNext("<color=blue>blue</color");
            LogTag(tag);
            tag = RichTextTag.ParseNext("No tag in here");
            LogTag(tag);
            tag = RichTextTag.ParseNext("No <color=blueblue</color tag here either");
            LogTag(tag);
            tag = RichTextTag.ParseNext("This tag is a closing tag </bold>");
            LogTag(tag);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandlePrintNextClicked();
        }
    }

    private void HandlePrintNextClicked()
    {
        if (this.textTyper.IsSkippable() && this.textTyper.IsTyping)
        {
            this.textTyper.Skip();
        }
        else
        {
            ShowScript();
        }
    }

    private void HandlePrintNoSkipClicked()
    {
        ShowScript();
    }

    private void ShowScript()
    {
        if(dialogueLines.Count == 0 && story.currentChoices.Count > 0)
        {
            foreach (GameObject g in activateButtons)
            {
                g.SetActive(true);
            }
        }

        if (dialogueLines.Count <= 0)
        {
            if(endDialogue == true)
            {
                EndStory();
            }
            return;
        }

        OnNext();
        this.textTyper.TypeText(dialogueLines.Dequeue(), this);
    }

    private void LogTag(RichTextTag tag)
    {
        if (tag != null)
        {
            Debug.Log("Tag: " + tag.ToString());
        }
    }

    private void HandleCharacterPrinted(string printedCharacter)
    {
        // Do not play a sound for whitespace
        if (printedCharacter == " " || printedCharacter == "\n")
        {
            return;
        }

        var audioSource = this.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = this.gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = this.printSoundEffect;
        audioSource.Play();
    }

    private void HandlePrintCompleted()
    {
        Debug.Log("TypeText Complete");
    }

    private void OnNext()
    {

        /*if (currentLineNumber + 1 < dialogueLines.Count)
        {
            currentLineNumber++;

            switch (dialogueBoxPosition)
            {
                case (DialogueBoxPosition.Left):
                    {


                        imagePoints[0].enabled = true;
                        namePoints[0].text = currentSpeaker.speakerName;

                        break;
                    }
                case (DialogueBoxPosition.Right):
                    {
                        imagePoints[1].enabled = true;
 
                        namePoints[1].text = currentSpeaker.speakerName;
                        break;
                    }
            }

        }
        else
        {
            Debug.Log("Ran out of text, but there might still be choices");

        }*/


    }

    public void TagSetAlignment(int f)
    {
        Enum.TryParse(f.ToString(), out DialogueBoxPosition _position);
        dialogueBoxPosition = _position;
    }

    public void TagSetExpression(int f)
    {
        changeExpression = f;
    }

    public void SetExpression()
    {

        Enum.TryParse(changeExpression.ToString(), out Expression expression);

        //Switch for additional effects, such as a screen shake or a sound
        switch (expression)
        {
            case (Expression.Neutral):
                {

                    break;
                }
            case (Expression.Angry):
                {

                    break;
                }
            case (Expression.Smug):
                {

                    break;
                }
            case (Expression.Think):
                {

                    break;
                }

            default:
                {
                    break;
                }
        }

        

        changeExpression = (int)expression;

    }

    public void TagSetSpeaker(int f)
    {
        changeSpeaker = f;
        SetSpeaker();
    }

    public void SetSpeaker()
    {
        Enum.TryParse(changeSpeaker.ToString(), out Speaker speaker);

        //in case event happens on character change
        switch (speaker)
        {
            case (Speaker.Phoebe):
                {

                    break;
                }
            case (Speaker.Axolotl):
                {

                    break;
                }

            default:
                {
                    break;
                }
        }

        changeSpeaker = (int)speaker;
        currentSpeaker = Resources.Load<SpeakerAsset>("DialogueSpeakers/" + speaker + "Dialogue");

    }

    public void RefreshSpeaker()
    {
        imagePoints[(int)dialogueBoxPosition].enabled = true;
        imagePoints[(int)dialogueBoxPosition].sprite = currentSpeaker.expressionSprites[changeExpression];
        namePoints[(int)dialogueBoxPosition].text = currentSpeaker.speakerName;
    }

    void InkStory()
    {
        if (activateButtons != null)
        {
            activateButtons.Clear();
        }
        foreach(Button b in choiceButtons)
        {
            b.onClick.RemoveAllListeners();
            b.gameObject.SetActive(false);
        }

        while (story.canContinue)
        {
            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!
            dialogueLines.Enqueue(text);

        }

        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                if(!choiceButtons[i].gameObject.activeSelf)
                {
                    activateButtons.Add(choiceButtons[i].gameObject);
                }
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = choice.text.Trim();
                
                // Tell the button what to do when we press it
                choiceButtons[i].onClick.AddListener(delegate {
                    OnClickChoiceButton(choice);
                });
            }

        }
        // If we've read all the content and there's no choices, the story is finished!
        else
        {
            endDialogue = true;
        }
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);

        InkStory();

        ShowScript();
    }

    void EndStory()
    {
        Debug.Log("disable");
        foreach(RectTransform t in GetComponentInChildren<RectTransform>())
        {
            t.gameObject.SetActive(false);
        }
    }

}
