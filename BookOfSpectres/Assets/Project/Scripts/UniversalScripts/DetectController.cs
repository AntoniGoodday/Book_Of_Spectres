using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class DetectController : MonoBehaviour
{
    /*PlayerIndex player = PlayerIndex.One;
    [SerializeField]
    bool controllerConnected = false;
    [SerializeField]
    InkTypewriterText inkTypewriterText;

    private void Awake()
    {
        GamePadState testState = GamePad.GetState(player);
        Debug.Log(Input.GetJoystickNames().Length);
    }

    // Start is called before the first frame update
    void Start()
    {
        //inkTypewriterText = GameObject.Find("DialogueCanvas").GetComponent<InkTypewriterText>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MenuScene" && scene.name != "CreditsScene")
        {
            inkTypewriterText = GameObject.Find("DialogueCanvas").GetComponent<InkTypewriterText>();
            StartCoroutine(LateStart());
        }
    }

    // Update is called once per frame
    void Update()
    {
        GamePadState testState = GamePad.GetState(player);
        if(Input.GetJoystickNames().Length >= 1 && controllerConnected == false)
        {
            Controller();
        }
        else if(Input.GetJoystickNames().Length == 0 && controllerConnected == true)
        {
            Keyboard();
        }
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSecondsRealtime(1);
        if (Input.GetJoystickNames().Length >= 1)
        {
            Controller();
        }
        else
        {
            Keyboard();
        }
    }

    void Controller()
    {
        AlteredInputModule _alt = GameObject.Find("EventSystem").GetComponent<AlteredInputModule>();
        _alt.horizontalAxis = "MoveHorizontal";
        _alt.verticalAxis = "MoveVertical";

        //inkTypewriterText = GameObject.Find("DialogueCanvas").GetComponent<InkTypewriterText>();
        if (inkTypewriterText != null)
        {
            inkTypewriterText.story.variablesState["controllerConnected"] = true;
        }
        controllerConnected = true;
    }

    void Keyboard()
    {
        AlteredInputModule _alt = GameObject.Find("EventSystem").GetComponent<AlteredInputModule>();
        _alt.horizontalAxis = "Horizontal";
        _alt.verticalAxis = "Vertical";

        //inkTypewriterText = GameObject.Find("DialogueCanvas").GetComponent<InkTypewriterText>();
        if (inkTypewriterText != null)
        {
            inkTypewriterText.story.variablesState["controllerConnected"] = false;
        }
        controllerConnected = false;
    }*/
}
