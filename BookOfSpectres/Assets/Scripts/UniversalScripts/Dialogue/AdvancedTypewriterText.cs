using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedBlueGames.Tools.TextTyper;
using UnityEngine.UI;
using TMPro;

using EnumScript;
public class AdvancedTypewriterText : MonoBehaviour
{
    [SerializeField]
    private AudioClip printSoundEffect;

    [Header("UI References")]

    [SerializeField]
    private Button printNextButton;

    [SerializeField]
    private Button printNoSkipButton;

    [SerializeField]
    DialogueAsset dialogue;

    [SerializeField]
    Image[] imagePoints;

    [SerializeField]
    TextMeshProUGUI[] namePoints;

    private Queue<string> dialogueLines = new Queue<string>();

    [SerializeField]
    private TextTyper textTyper;

    int currentLineNumber;
    public int changeExpression;
    // Start is called before the first frame update
    void Start()
    {
        currentLineNumber = -1;
        this.textTyper.PrintCompleted.AddListener(this.HandlePrintCompleted);
        this.textTyper.CharacterPrinted.AddListener(this.HandleCharacterPrinted);

        //this.printNextButton.onClick.AddListener(this.HandlePrintNextClicked);
        //this.printNoSkipButton.onClick.AddListener(this.HandlePrintNoSkipClicked);

        DialogueClass _dialogueClass = dialogue.dialogueClass;

        string[] _tempArray = new string[_dialogueClass.speakerDialogue.Length];

        
        
        for(int i = 0; i < _dialogueClass.speakerDialogue.Length; i++)
        {
            _tempArray[i] = AlignToSide(i) + _dialogueClass.speakerDialogue[i];

            
            //dialogueLines.Enqueue(_tempArray[i]);
        }

        foreach(string s in _tempArray)
        {
            dialogueLines.Enqueue(s);
        }

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
        if(Input.GetKeyDown(KeyCode.Space))
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
       
        if (dialogueLines.Count <= 0)
        {
            return;
        }

        OnNext();
        //this.textTyper.TypeText(dialogueLines.Dequeue(), this);
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
        DialogueClass _dialogueClass = dialogue.dialogueClass;
        if (currentLineNumber + 1 < _dialogueClass.speakerDialogue.Length)
        {

            
        currentLineNumber++;

            //int _expressionNumber = 1;
            
            //_expressionNumber = (int)_dialogueClass.setExpression[currentLineNumber];

            
            //changeExpression = _expressionNumber;

            //Debug.Log(changeExpression);

            //SetExpression();
            
            
            SpeakerAsset _currentSpeakerInfo = _dialogueClass.speakerInfo[currentLineNumber];
            switch (_dialogueClass.location[currentLineNumber])
            {
                case (DialogueBoxPosition.Left):
                    {
                        

                        imagePoints[0].enabled = true;
                        /*if(_dialogueClass.holdExpression[currentLineNumber] == false)
                        {
                            imagePoints[0].sprite = _currentSpeakerInfo.expressionSprites[_expressionNumber];
                        }*/
                        namePoints[0].text = _currentSpeakerInfo.speakerName;

                        break;
                    }
                case (DialogueBoxPosition.Right):
                    {
                        imagePoints[1].enabled = true;
                        /*if (_dialogueClass.holdExpression[currentLineNumber] == false)
                        {
                            imagePoints[1].sprite = _currentSpeakerInfo.expressionSprites[_expressionNumber];
                        }*/
                        namePoints[1].text = _currentSpeakerInfo.speakerName;
                        break;
                    }
            }
            
        }
        else
        {
            Debug.Log("No More Lines");
        }

        
    }

    string AlignToSide(int i)
    {
        string quote = "\"";
        string _align = "";

        switch(dialogue.dialogueClass.location[i])
        {
            case (DialogueBoxPosition.Left):
                {
                    _align = "<align=" + quote + "left" + quote + ">";
                    return _align;
                }
            case (DialogueBoxPosition.Right):
                {
                    _align = "<align=" + quote + "right" + quote + ">";
                    return _align;
                }
        }
        return _align;

    }

    public void TagSetExpression(int f)
    {
        changeExpression = f;
        SetExpression();
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

        int _expressionNumber = (int)expression;

        changeExpression = _expressionNumber;

        DialogueClass _dialogueClass = dialogue.dialogueClass;
        SpeakerAsset _currentSpeakerInfo = _dialogueClass.speakerInfo[currentLineNumber];

        switch (_dialogueClass.location[currentLineNumber])
        {
            case (DialogueBoxPosition.Left):
                {


                    imagePoints[0].enabled = true;
                    //if (_dialogueClass.holdExpression[currentLineNumber] == false)
                    //{
                        imagePoints[0].sprite = _currentSpeakerInfo.expressionSprites[_expressionNumber];
                    //}
                    namePoints[0].text = _currentSpeakerInfo.speakerName;

                    break;
                }
            case (DialogueBoxPosition.Right):
                {
                    imagePoints[1].enabled = true;
                    //if (_dialogueClass.holdExpression[currentLineNumber] == false)
                    //{
                        imagePoints[1].sprite = _currentSpeakerInfo.expressionSprites[_expressionNumber];
                    //}
                    namePoints[1].text = _currentSpeakerInfo.speakerName;
                    break;
                }
        }

        
        //Debug.Log("Expression: " + expression);

    }


}
