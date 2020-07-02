using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

using PlayerControlNamespace;

using RedBlueGames.Tools.TextTyper;

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

    [SerializeField]
    private Queue<string> dialogueLines = new Queue<string>();

    [SerializeField]
    private TextTyperSimple textTyper;

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

    bool endDialogueEvent = false;

    bool setNextSpeaker = false;

    [SerializeField]
    bool unpauseAfterEnd = true;

    bool initializeButtonChoice = false;

    bool canStart = false;

    //Text Event Variables
    string sceneName = "";

    public DialogueBoxPosition dialogueBoxPosition = DialogueBoxPosition.Left;

    //for simple effects, such as darkening/lightening the screen when dialogue plays
    [SerializeField]
    Image darkness;
    [SerializeField]
    Image lightness;

    ObjectPooler objectPooler;

    //PlayerControl playerControl;

    bool controlsEnabled = true;

    [SerializeField]
    GameObject visuals;

    public GameObject Visuals { get => visuals; set => visuals = value; }
    public bool ControlsEnabled { get => controlsEnabled; set => controlsEnabled = value; }

    public static InkTypewriterText Instance;

    private void Awake()
    {
        //playerControl = new PlayerControl();

        //playerControl.DefaultControls.DialogueSubmit.performed += context => HandlePrintNextClicked();

        //playerControl.DefaultControls.Move.performed += context => SelectButton(context.ReadValue<Vector2>());

        //playerControl.Enable();

    }

    private void OnDisable()
    {
        //playerControl.Disable();
    }

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

        story = new Story(inkJSONAsset.text);

        SceneManager.sceneLoaded += OnSceneLoaded;

        //FIX LATER
        CombatMenu.MenuPauseEvent += MenuPaused;
        CombatMenu.MenuUnPauseEvent += MenuUnPaused;


        //story.ChoosePathString("tutorial_1");

        BindExternalFunctions();

        currentSpeaker = Resources.Load<SpeakerAsset>("DialogueSpeakers/PhoebeDialogue");

        objectPooler = ObjectPooler.Instance;


        this.textTyper.PrintCompleted.AddListener(this.HandlePrintCompleted);
        this.textTyper.CharacterPrinted.AddListener(this.HandleCharacterPrinted);

        //this.printNextButton.onClick.AddListener(this.HandlePrintNextClicked);
        //this.printNoSkipButton.onClick.AddListener(this.HandlePrintNoSkipClicked);

        //story.ChoosePathString("tutorial_1");
        //StartDialogue();


        //this.printNextButton.onClick.AddListener(this.HandlePrintNextClicked);
        //this.printNoSkipButton.onClick.AddListener(this.HandlePrintNoSkipClicked);

        //InkStory();

        /*dialogueLines.Enqueue("Hello! My name is... <delay=0.5>NPC</delay>. Got it, <i>bub</i>?");
        dialogueLines.Enqueue("You can <b>use</b> <i>uGUI</i> <size=40>text</size> <size=20>tag</size> and <color=#ff0000ff>color</color> tag <color=#00ff00ff>like this</color>.");
        dialogueLines.Enqueue("bold <b>text</b> test <b>bold</b> text <b>test</b>");
        dialogueLines.Enqueue("You can <size=40>size 40</size> and <size=20>size 20</size>");
        dialogueLines.Enqueue("You can <color=#ff0000ff>color</color> tag <color=#00ff00ff>like this</color>.");
        dialogueLines.Enqueue("Sample Shake Animations: <anim=lightrot>Light Rotation</anim>, <anim=lightpos>Light Position</anim>, <anim=fullshake>Full Shake</anim>\nSample Curve Animations: <animation=slowsine>Slow Sine</animation>, <animation=bounce>Bounce Bounce</animation>, <animation=crazyflip>Crazy Flip</animation>");*/

        //StartDialogue();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case ("BattleScene"):
                {
                    CombatMenu.MenuPauseEvent += MenuPaused;
                    CombatMenu.MenuUnPauseEvent += MenuUnPaused;
                    break;
                }
        }
    }

    void MenuPaused()
    {
        unpauseAfterEnd = false;
    }

    void MenuUnPaused()
    {
        unpauseAfterEnd = true;
    }

    public void Update()
    {
        if(Input.GetButtonDown("Horizontal"))
        {
            SelectButton(new Vector2( Input.GetAxisRaw("Horizontal"),0));
        }

        if(Input.GetButtonUp("Shoot"))
        {
            HandlePrintNextClicked();
        }

        /*if (Input.GetKeyDown(KeyCode.Tilde))
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
        }*/
    }

    public void StartDialogue(string knot = "")
    {
        //playerControl.DefaultControls.DialogueSubmit.Enable();
        endDialogue = false;
        canStart = false;

        EnableControls();

        if (objectPooler != null)
        {
            objectPooler.PauseAll();
            objectPooler.SetLowpass(300);
        }
        //dialogueLines.Clear();


        InkStory();

        visuals.GetComponent<CanvasGroup>().alpha = 1;

        

       

        

        StartCoroutine(DelayStart());
        if (unpauseAfterEnd == false)
        {
            FindObjectOfType<CombatMenu>().GetComponent<CanvasGroup>().interactable = false;
        }
        

        

        

        var _darkColour = darkness.color;
        _darkColour.a = 0.6f;
        darkness.color = _darkColour;

        //ShowScript();
        if (dialogueLines.Count == 0 && story.currentChoices.Count > 0)
        {
            foreach (GameObject g in activateButtons)
            {
                g.SetActive(true);
            }
        }

        /*if (dialogueLines.Count <= 0)
        {
            if (endDialogue == true)
            {
                StartCoroutine(EndStory());
            }
            return;
        }*/

        //if (textTyper = null)
        //{
        //    textTyper = GameObject.Find("TextTyper").GetComponent<TextTyper>();
        //
        //}
        //this.textTyper.TypeText(dialogueLines.Dequeue(), this);

        StartCoroutine(DelayType());
        SetExpression();
    }


    private void HandlePrintNextClicked()
    {
        if (canStart == true)
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
    }

    private void HandlePrintNoSkipClicked()
    {
        ShowScript();
    }

    private void ShowScript()
    {
        //QuickFix();
        

        if (!story.canContinue && story.currentChoices.Count == 0)
        {
            endDialogue = true;
        }

        if (endDialogue == true && dialogueLines.Count <= 0)
        {
            StartCoroutine(EndStory());
            return;
        }
        if (textTyper.enabled == false)
        {
            visuals.GetComponent<CanvasGroup>().alpha = 1;
        }

        /*if (dialogueLines.Count <= 0)
        {
            if(endDialogue == true)
            {
                EndStory();
            }
            Debug.Log("End, but check failed");
            return;
        }*/

        if (story.currentChoices.Count > 0)
        {
            OnNext();
            foreach (GameObject g in activateButtons)
            {
                g.SetActive(true);
            }
            return;
        }
        OnNext();
        if (dialogueLines.Count > 0)
        {
            this.textTyper.TypeText(dialogueLines.Dequeue(), this);
        }
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
        //Debug.Log("Line print finished 2");
    }

    private void OnNext()
    {
        InkStory();


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
        foreach(int i in Enum.GetValues(typeof(DialogueBoxPosition)))
        {
            if(i == (int)dialogueBoxPosition)
            {
                imagePoints[(int)dialogueBoxPosition].enabled = true;
                imagePoints[(int)dialogueBoxPosition].sprite = currentSpeaker.expressionSprites[changeExpression];
                imagePoints[(int)dialogueBoxPosition].SetNativeSize();
                namePoints[(int)dialogueBoxPosition].text = currentSpeaker.speakerName;
            }
            else
            {
                imagePoints[i].enabled = false;
                namePoints[i].text = "";
            }
        }
        
    }

    public void InvokeVoid()
    {
        Debug.Log("The void has invoked:");
    }

    public void TwoNumbers(int x, int y)
    {
        Debug.Log(x + " and " + y);
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

        /*while (story.canContinue)
        {
            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!
            dialogueLines.Enqueue(text);

        }*/


        if (story.canContinue)
        {
            
            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!
            //dialogueLines.Enqueue(ParseTextLine(text));



            dialogueLines.Enqueue(ParseTextLine(text));

        }
        // Display all the choices, if there are any!
        else if (story.currentChoices.Count > 0)
        {
            initializeButtonChoice = false;


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

            //choiceButtons[0].Select();

        }
        // If we've read all the content and there's no choices, the story is finished!
        else
        {

        }
    }

    public string ParseTextLine(string line)
    {
        string content = "";
        string speaker = "";

        if (TrySplitContentBySearchString(line, ": ", ref speaker, ref content))
        {
            
            Enum.TryParse(speaker, out Speaker _spk);
            changeSpeaker = (int)_spk;
            changeExpression = 0;

            SetSpeaker();
            SetExpression();
            RefreshSpeaker();
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
        if(firstSpecialCharacterIndex == -1)
        {
            return false;
        }

        left = line.Substring(0, firstSpecialCharacterIndex).Trim();
        right = line.Substring(firstSpecialCharacterIndex + searchFor.Length, line.Length - firstSpecialCharacterIndex - searchFor.Length).Trim();
        return true;
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);

        //InkStory();

        ShowScript();
    }

    IEnumerator EndStory(float time = 0.5f)
    {
        if (canStart == true && !this.textTyper.IsTyping && !story.canContinue)
        {
            DisableControls();

            //playerControl.DefaultControls.DialogueSubmit.Disable();
            yield return new WaitForSecondsRealtime(time);
            endDialogue = false;
            //Debug.Log("disable");
            //foreach(RectTransform t in GetComponentInChildren<RectTransform>())
            //{
            //    t.gameObject.SetActive(false);
            //}




            var _darkColour = darkness.color;
            _darkColour.a = 0.6f;
            darkness.color = _darkColour;

            GoToScene();

            /*foreach (Transform t in gameObject.GetComponentInChildren<Transform>())
            {
                if (t.gameObject.name != "TextTyper")
                {
                    t.gameObject.SetActive(false);
                }
            }*/

            visuals.GetComponent<CanvasGroup>().alpha = 0;

            textTyper.GetComponent<TextMeshProUGUI>().text = "";
            dialogueLines.Clear();



            if (unpauseAfterEnd == true)
            {
                if (objectPooler != null)
                {
                    objectPooler.UnPauseAll();
                }
            }
            else
            {
                FindObjectOfType<CombatMenu>().GetComponent<CanvasGroup>().interactable = true;
            }

            objectPooler.SetLowpass(22000);

        }


        //this.gameObject.SetActive(false);
    }

    void PlayStory()
    {

    }

    void BindExternalFunctions()
    {
        story.BindExternalFunction("GoToScene", (string _sceneName) =>
        {
            endDialogueEvent = true;
            sceneName = _sceneName; 
        });
    }


    void GoToScene()
    {
        if (sceneName != "")
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    void SelectButton(Vector2 direction)
    {
        if (choiceButtons[0].IsActive() && initializeButtonChoice == false)
        {
            if (direction.x > 0)
            {
                choiceButtons[0].FindSelectableOnRight().Select();
            }
            else if(direction.x < 0)
            {
                choiceButtons[0].Select();
            }

            initializeButtonChoice = true;
        }
    }

    IEnumerator DelayStart()
    {
        canStart = false;
        yield return new WaitForSecondsRealtime(1);
        canStart = true;
    }

    IEnumerator DelayType()
    {
        yield return new WaitForSecondsRealtime(0);
        this.textTyper.TypeText(dialogueLines.Dequeue(), this);
    }

    void QuickFix()
    {
        if (Time.timeScale != 0)
        {
            visuals.GetComponent<CanvasGroup>().alpha = 1;

            objectPooler.PauseAll();

            objectPooler.SetLowpass(300);


            if (unpauseAfterEnd == false)
            {
                FindObjectOfType<CombatMenu>().GetComponent<CanvasGroup>().interactable = false;
            }






            var _darkColour = darkness.color;
            _darkColour.a = 0.6f;
            darkness.color = _darkColour;

            //ShowScript();
            if (dialogueLines.Count == 0 && story.currentChoices.Count > 0)
            {
                foreach (GameObject g in activateButtons)
                {
                    g.SetActive(true);
                }
            }

            SetExpression();
        }
    }
    
    public void DisableControls()
    {
        controlsEnabled = false;
        //playerControl.DefaultControls.DialogueSubmit.Disable();
    }

    public void EnableControls()
    {
        controlsEnabled = true;
        //playerControl.DefaultControls.DialogueSubmit.Enable();
    }

}
