using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

using TMPro;

using Ink.Runtime;

using EnumScript;

public class InkDialogueManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip printSoundEffect;

    public AudioSource audioSource;

    [Header("UI References")]

    [SerializeField]
    private CanvasGroup visuals;

    [SerializeField]
    Image lightScreen;

    [SerializeField]
    Image darkScreen;

    [SerializeField]
    private Button printNextButton;

    [SerializeField]
    private Button printNoSkipButton;

    [SerializeField]
    SpeakerAvatar[] speakerAvatars;

    [SerializeField]
    TextMeshProUGUI[] namePoints;

    [SerializeField]
    private Queue<string> dialogueLines = new Queue<string>();

    [SerializeField]
    AnimatedTextTyper currentTextTyper;

    [SerializeField]
    SpeakerAsset currentSpeaker;
    [SerializeField]
    SpeakerAvatar currentAvatar;

    Speaker speakerEnum = Speaker.Phoebe;
    [SerializeField]
    Expression speakerExpression = Expression.neutral;

    [SerializeField]
    public TextAsset inkJSONAsset = null;
    [SerializeField]
    public Story story;
    [SerializeField]
    List<Button> choiceButtons;

    bool isChoosing = false;
    bool isInDialogue = false;

    List<GameObject> activateButtons = new List<GameObject>();

    public static InkDialogueManager Instance;

    #region delegates
    public delegate void DialogueStartAction();
    public static event DialogueStartAction OnDialogueStarted;

    public delegate void DialogueEndAction();
    public static event DialogueEndAction OnDialogueEnded;
    #endregion

    public delegate void EndAction(string param1);
    public string chosenEndAction;
    public string endParam1;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        audioSource = GetComponent<AudioSource>();

        //inkJSONAsset = Resources.Load<TextAsset>("Story/" + "MainStory");
        story = new Story(inkJSONAsset.text);

        //StartDialogue("simple_test");
        

    }

    

    public void StartDialogue(string _knot = "")
    {
        OnDialogueStarted();
        isInDialogue = true;
        visuals.alpha = 1;
        story.ChoosePathString(_knot);
        Color _col = darkScreen.color;
        darkScreen.color = new Color(_col.r, _col.g, _col.b, 0.5f);
        InkStory();
        currentTextTyper.StartShowingText();
    }

    public void EndDialogue()
    {
        OnDialogueEnded();
        isInDialogue = false;
        visuals.alpha = 0;
        currentTextTyper.StopShowingText();

        EndAction endAction = null;

        switch (chosenEndAction)
        {
            case "changescene":
                endAction = ChangeScene;
                break;
        }
        endAction?.Invoke(endParam1);

        chosenEndAction = "";
        endParam1 = "";
    }

    #region endScripts
    void ChangeScene(string scene)
    {       
        SceneManager.LoadScene(scene);
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Shoot") && isInDialogue)
        {
            //currentTextTyper.StartShowingText();
            if (isChoosing == false)
            {
                if (!currentTextTyper.isBaseInsideRoutine)
                {
                    InkStory();
                }
                else
                {
                    currentTextTyper.SkipTypewriter();
                }
            }
        }
    }

    

    void InkStory()
    {

        if (story.canContinue)
        {

            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!

            //dialogueLines.Enqueue(ParseTextLine(text));
            currentTextTyper.ShowText(ParseTextLine(text));
            currentTextTyper.StartShowingText();
            currentTextTyper.SetTypewriterSpeed(2);

        }
        // Display all the choices, if there are any!
        else if (story.currentChoices.Count > 0)
        {
            currentTextTyper.SkipTypewriter();
            //initializeButtonChoice = false;

            isChoosing = true;

            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                if (!choiceButtons[i].gameObject.activeSelf)
                {
                    //activateButtons.Add(choiceButtons[i].gameObject);
                    choiceButtons[i].gameObject.SetActive(true);
                }
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = choice.text.Trim();

                // Tell the button what to do when we press it
                choiceButtons[i].onClick.AddListener(delegate 
                {
                    OnClickChoiceButton(choice);
                });


            }

            //choiceButtons[0].Select();

        }
        // If we've read all the content and there's no choices, the story is finished!
        else
        {
            EndDialogue();
        }
    }

    public void OnClickChoiceButton(Choice choice)
    {
        isChoosing = false;

        story.ChooseChoiceIndex(choice.index);

        foreach (Button b in choiceButtons)
        {
            b.onClick.RemoveAllListeners();
            b.gameObject.SetActive(false);
        }

        InkStory();

        //ShowScript();
    }

    public string ParseTextLine(string line)
    {
        string content = "";
        string speakerFull = "";
        string speaker = "";
        string location = "";

        if (TrySplitContentBySearchString(line, ": ", ref speakerFull, ref content))
        {
            if (TrySplitContentBySearchString(line, "-", ref speaker, ref speakerFull))
            {
                Enum.TryParse(speaker, out Speaker _spk);
                speakerEnum = _spk;
                //changeSpeaker = (int)_spk;
                //changeExpression = 0;
                SpeakerAvatar _tempAvatar = currentAvatar;

                if (TrySplitContentBySearchString(line, ": ", ref location, ref speakerFull))
                {
                    location = location.Remove(0, speaker.Length + 1);
                    foreach (SpeakerAvatar s in speakerAvatars)
                    {
                        if (s.gameObject.name.ToLower() == location.ToLower())
                        {
                            _tempAvatar = s;
                        }
                    }
                }

                if(_tempAvatar == currentAvatar)
                {
                    currentAvatar = _tempAvatar;
                    SetCurrentSpeaker();
                }
                else
                {
                    currentAvatar.GreyOut();
                    currentAvatar = _tempAvatar;
                    SetCurrentSpeaker();
                }

                
                //SetExpression();
                //RefreshSpeaker();

            }
            else
            {
                Enum.TryParse(speaker, out Speaker _spk);
                speakerEnum = _spk;
                //changeSpeaker = (int)_spk;
                //changeExpression = 0;

                SetCurrentSpeaker();
                //SetExpression();
                //RefreshSpeaker();
            }


        }
        else
        {
            return line;
        }

        return content;
    }

    public bool TrySplitContentBySearchString(string line, string searchFor, ref string left, ref string right)
    {
        int firstSpecialCharacterIndex = line.IndexOf(searchFor);
        if (firstSpecialCharacterIndex == -1)
        {
            return false;
        }

        left = line.Substring(0, firstSpecialCharacterIndex).Trim();
        right = line.Substring(firstSpecialCharacterIndex + searchFor.Length, line.Length - firstSpecialCharacterIndex - searchFor.Length).Trim();
        return true;
    }

    public void SetCurrentSpeaker()
    {

        //in case event happens on character change
        switch (speakerEnum)
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

        currentSpeaker = Resources.Load<SpeakerAsset>("DialogueSpeakers/" + speakerEnum + "Dialogue");


        currentAvatar.SetSpeaker(currentSpeaker, true, (int)speakerExpression);
        if (currentAvatar.transform.localPosition.x < 0)
        {
            namePoints[1].text = "";
            namePoints[0].text = currentSpeaker.speakerName;
        }
        else
        {
            namePoints[0].text = "";
            namePoints[1].text = currentSpeaker.speakerName;
        }
        //RefreshSpeaker(0);

    }

    public void SetAnySpeaker(string _spk, string _avatarName, bool _isActive = true, int _expression = 0)
    {
        bool _worked = false;

        Enum.TryParse(_spk, out Speaker _speak);
        SpeakerAsset _tempSpeak = Resources.Load<SpeakerAsset>("DialogueSpeakers/" + _speak + "Dialogue");

        foreach(SpeakerAvatar s in speakerAvatars)
        {
            if(s.gameObject.name.ToLower() == _avatarName)
            {
                _worked = true;
                s.SetSpeaker(_tempSpeak, _isActive, _expression);
            }
        }

        if(_worked == false)
        {
            Debug.Log("Possible Typo");
        }
    }

    #region Expressions
    public void SetExpression(string _exp)
    {
        
        Enum.TryParse(_exp, out Expression _expression);
        speakerExpression = _expression;

        currentAvatar.SetSpeaker(currentSpeaker, true, (int)speakerExpression);
    }

    public void ClearSpeakers(string _type)
    {
        switch(_type)
        {
            case "all":
                foreach (SpeakerAvatar s in speakerAvatars)
                {
                    s.ClearSpeaker();
                }
                break;
            case "others":
                foreach (SpeakerAvatar s in speakerAvatars)
                {
                    if (s != currentAvatar)
                    {
                        s.ClearSpeaker();
                    }
                }
                break;
            default:
                foreach (SpeakerAvatar s in speakerAvatars)
                {
                    if (s.gameObject.name == _type)
                    {
                        s.ClearSpeaker();
                    }
                }
                break;
        }
        
    }
    #endregion

    void RefreshSpeaker(int speakerID)
    {
        speakerAvatars[speakerID].SetSpeaker(currentSpeaker, true, (int)speakerExpression);
        //imagePoints[speakerID].sprite = currentSpeaker.expressionSprites[(int)speakerExpression];


        //speakerAvatars[speakerID].SetNativeSize();
        /*if(!imagePoints[speakerID].enabled)
        {
            imagePoints[speakerID].enabled = true;
        }*/
        namePoints[speakerID].text = currentSpeaker.speakerName;
    }
}
