using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Febucci.UI.Core;
using Febucci.UI;

public class AnimatedTextTyper : TAnimPlayerBase
{
    [Header("References")]
    public AudioSource source;

    [Header("Management")]
    [Tooltip("How much time has to pass before playing the next sound"), SerializeField]
    float minSoundDelay = .07f;

    [Tooltip("True if you want the new sound to cut the previous one\nFalse if each sound will continue until its end"), SerializeField]
    bool interruptPreviousSound = true;

    [Header("Audio Clips")]
    [Tooltip("True if sounds will be picked random from the array\nFalse if they'll be chosen in order"), SerializeField]
    bool randomSequence = false;
    [SerializeField] AudioClip[] sounds = new AudioClip[0];

    float latestTimePlayed = -1;
    int clipIndex;

    InkDialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = InkDialogueManager.Instance;
    }
    private void Awake()
    {
        /*Assert.IsNotNull(source, "TAnimSoundWriter: Typewriter Audio Source reference is null");
        Assert.IsNotNull(sounds, "TAnimSoundWriter: Sounds clips array is null in the");
        Assert.IsTrue(sounds.Length > 0, "TAnimSoundWriter: Sounds clips array is empty");
        Assert.IsNotNull(GetComponent<Core.TAnimPlayerBase>(), "TAnimSoundWriter: Component TAnimPlayerBase is not present");*/

        


        //Prevents subscribing the event if the component has not been set correctly
        if (source == null || sounds.Length <= 0)
            return;

        //Prevents common setup errors
        source.playOnAwake = false;
        source.loop = false;

        this?.onCharacterVisible.AddListener(OnCharacter);

        clipIndex = randomSequence ? Random.Range(0, sounds.Length) : 0;
    }

    void OnCharacter(char character)
    {
        if (Time.time - latestTimePlayed <= minSoundDelay)
            return; //Early return if not enough time passed yet

        source.clip = sounds[clipIndex];

        //Plays audio
        if (interruptPreviousSound)
            source.Play();
        else
            source.PlayOneShot(source.clip);

        //Chooses next clip to play
        if (randomSequence)
        {
            clipIndex = Random.Range(0, sounds.Length);
        }
        else
        {
            clipIndex++;
            if (clipIndex >= sounds.Length)
                clipIndex = 0;
        }

        latestTimePlayed = Time.time;

    }

    protected override IEnumerator WaitInput()
    {
        while (!Input.anyKeyDown)
        {
            yield return null;
        }
    }

    protected override float WaitTimeOf(char character)
    {
        switch (character)
        {
            case ',': return 0.3f;

            case '!':
            case '?':
            case '.': return 0.6f;
        }

        return 0.1f;
    }

    protected override IEnumerator DoCustomAction(TypewriterAction action)
    {
        if(dialogueManager == null)
        {
            dialogueManager = InkDialogueManager.Instance;
        }

        switch (action.actionID)
        {
            case "test":
                for (int i = 0; i < action.parameters.Count; i++)
                {
                    Debug.Log(action.parameters[i]);
                }
                break;
            case "expression":
                for (int i = 0; i < action.parameters.Count; i++)
                {
                    switch(action.parameters.Count)
                    {
                        case 1:
                            dialogueManager.SetExpression(action.parameters[i]);
                            break;
                    }
                    
                }
                break;
            case "speaker":
                bool _temp = false;
                switch (action.parameters.Count)
                {
                    case 2:
                        dialogueManager.SetAnySpeaker(action.parameters[0], action.parameters[1]);
                        break;
                    case 3:
                        
                        if(action.parameters[2].ToLower() == "yes" || action.parameters[2].ToLower() == "true")
                        {
                            _temp = true;
                        }
                        dialogueManager.SetAnySpeaker(action.parameters[0], action.parameters[1], _temp);
                        break;
                    case 4:
                        if (action.parameters[2].ToLower() == "yes" || action.parameters[2].ToLower() == "true")
                        {
                            _temp = true;
                        }
                        int.TryParse(action.parameters[3], out int _parsed);
                        dialogueManager.SetAnySpeaker(action.parameters[0], action.parameters[1], _temp, _parsed);
                        break;
                }
                break;
            case "clearaspeakers":
                dialogueManager.ClearSpeakers(action.parameters[0]);
                break;
            case "endaction":
                dialogueManager.chosenEndAction = action.parameters[0];
                dialogueManager.endParam1 = action.parameters[1];
                break;
        }
        yield return null;
    }
}
