using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EnumScript;
using Ink.Runtime;
using System.Linq;
using System;
public class TutorialScript : MonoBehaviour
{
    int currentTutorialStage = 1;

    int attacksExecuted = 0;

    [SerializeField]
    bool[] directionsUsed;

    bool doOnce = false;

    bool coroutineRunning = false;

    int dodgeInitial;

    [SerializeField]
    public InkTypewriterText inkTypewriterText;

    PlayerAchievements playerAchievements;

    TurnBarScript turnBarScript;

    ObjectPooler objectPooler;

    Action currentAction;
    // Start is called before the first frame update
    void Start()
    {
        inkTypewriterText = GameObject.Find("DialogueCanvas").GetComponent<InkTypewriterText>();
        playerAchievements = GameObject.Find("PlayerStats").GetComponent<PlayerAchievements>();
        turnBarScript = GameObject.Find("TurnBar").GetComponent<TurnBarScript>();

        turnBarScript.SpeedModifier = 0;

        dodgeInitial = playerAchievements.ConsecutiveProjectilesDodged;



        //FindObjectOfType<PlayerScript>().MoveEvent += OnPlayerMove;
        objectPooler = ObjectPooler.Instance;

        GameObject.Find("PlayerCombat").GetComponent<EntityStatus>().StatusEffects.Add(StatusEffect.Endure);

        StartCoroutine(LateStart());
    }

    private void Update()
    {
        switch(currentTutorialStage)
        {
            case (2):
                {               
                    DodgeCheck();
                    break;
                }
            case (3):
                {
                    //Attack check
                    if (doOnce == false)
                    {
                        //FindObjectOfType<PlayerScript>().ShootEvent += AttackCheck;
                        doOnce = true;
                    }
                    break;
                }
            case (4):
                {
                    //Charge Attack check
                    if (doOnce == false)
                    {
                        //FindObjectOfType<PlayerScript>().ChargedShootEvent += ChargeAttackCheck;
                        doOnce = true;
                    }
                    break;
                }
            case (5):
                {
                    if (doOnce == false)
                    {
                        CombatMenu.OnMenuPaused += MenuCheck;
                        doOnce = true;
                    }
                    break;
                }
            case (6):
                {
                    if (doOnce == false)
                    {
                        CombatMenu.OnMenuUnpaused += MenuEndCheck;
                        doOnce = true;
                    }
                    break;
                }

        }
        

    }


    public void OnPlayerMove(MoveDirection moveDir)
    {
        switch(moveDir)
        {
            case (MoveDirection.Up):
                {
                    directionsUsed[0] = true;
                    break;
                }
            case (MoveDirection.Down):
                {
                    directionsUsed[1] = true;
                    break;
                }
            case (MoveDirection.Left):
                {
                    directionsUsed[2] = true;
                    break;
                }
            case (MoveDirection.Right):
                {
                    directionsUsed[3] = true;
                    break;
                }
        }
        MovementCheck();
    }

    void MovementCheck()
    {
        bool[] _fourTrue = { true, true, true, true };
        if (Enumerable.SequenceEqual(directionsUsed,_fourTrue))
        {
            if (coroutineRunning == false)
            {
                coroutineRunning = true;
                currentAction = () => ExecuteMovementCheck();
                StartCoroutine(ObjectiveDelay(0.5f, currentAction));
            }
        }
    }

    void ExecuteMovementCheck()
    {
        //FindObjectOfType<PlayerScript>().MoveEvent -= OnPlayerMove;

        inkTypewriterText.story.ChoosePathString("tutorial_2");
        objectPooler.StartWave();
        inkTypewriterText.StartDialogue();


        GameObject.Find("AxolotlGhost").GetComponent<EntityStatus>().StatusEffects.Add(StatusEffect.Endure);

        currentTutorialStage++;
    }

    void DodgeCheck()
    {
        if (playerAchievements.ConsecutiveProjectilesDodged >= 3 + dodgeInitial)
        {
            //inkTypewriterText.story.ChoosePathString("tutorial_3");
            if (coroutineRunning == false)
            {
                coroutineRunning = true;
                currentAction = () => ExecuteDodgeCheck();
                StartCoroutine(ObjectiveDelay(0.5f, currentAction));
            }


            //inkTypewriterText.StartDialogue();


            //currentTutorialStage = 3;
        }
    }
    void ExecuteDodgeCheck()
    {
        dodgeInitial = 1000;
        inkTypewriterText.story.ChoosePathString("tutorial_3");
        inkTypewriterText.StartDialogue();
        currentTutorialStage = 3;
    }

    void AttackCheck()
    {
        attacksExecuted++;

        if (attacksExecuted >= 3)
        {
            if (coroutineRunning == false)
            {
                coroutineRunning = true;
                attacksExecuted = -100;
                currentAction = () => ExecuteAttackCheck();
                StartCoroutine(ObjectiveDelay(0.5f, currentAction));
            }
        }
    }

    void ExecuteAttackCheck()
    {

        //FindObjectOfType<PlayerScript>().ShootEvent -= AttackCheck;

        inkTypewriterText.story.ChoosePathString("tutorial_4");
        inkTypewriterText.StartDialogue();



        currentTutorialStage = 4;
    }

    void ChargeAttackCheck()
    {
        if (coroutineRunning == false)
        {
            coroutineRunning = true;
            currentAction = () => ExecuteChargeAttackCheck();
            StartCoroutine(ObjectiveDelay(0.5f, currentAction));
        }
    }

    void ExecuteChargeAttackCheck()
    {

        //FindObjectOfType<PlayerScript>().ChargedShootEvent -= ChargeAttackCheck;

        inkTypewriterText.story.ChoosePathString("tutorial_5");
        inkTypewriterText.StartDialogue();



        turnBarScript.SpeedModifier = 1;
        turnBarScript.CurrentTurnTime = 10;

        currentTutorialStage = 5;
    }

    void MenuCheck()
    {
        if (coroutineRunning == false)
        {
            coroutineRunning = true;
            currentAction = () => ExecuteMenuCheck();
            StartCoroutine(ObjectiveDelay(0, currentAction));
        }
    }

    void ExecuteMenuCheck()
    {
        CombatMenu.OnMenuPaused -= MenuCheck;

        inkTypewriterText.story.ChoosePathString("tutorial_6");
        inkTypewriterText.StartDialogue();

        FindObjectOfType<CombatMenu>().MinimumSpellsChosen = 3;



        currentTutorialStage = 6;
    }

    void MenuEndCheck()
    {
        if (coroutineRunning == false)
        {
            coroutineRunning = true;
            currentAction = () => ExecuteEndMenuCheck();
            StartCoroutine(ObjectiveDelay(0, currentAction));
        }
    }

    void ExecuteEndMenuCheck()
    {

        CombatMenu.OnMenuUnpaused -= MenuEndCheck;

        inkTypewriterText.story.ChoosePathString("tutorial_7");
        inkTypewriterText.StartDialogue();

        FindObjectOfType<CombatMenu>().MinimumSpellsChosen = 0;


        GameObject.Find("AxolotlGhost").GetComponent<EntityStatus>().StatusEffects.Remove(StatusEffect.Endure);

        currentTutorialStage = 7;

        objectPooler.DialogueKnot ="tutorial_8";

    }

    IEnumerator ObjectiveDelay(float time = 1, Action method = null, bool realtime = true)
    {
        if(realtime == true)
        {
            yield return new WaitForSeconds(time);
            method();
        }
        else
        {
            yield return new WaitForSecondsRealtime(time);
            method();            
        }

        currentAction = null;
        coroutineRunning = false;
        doOnce = false;
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.001f);
        inkTypewriterText.story.ChoosePathString("tutorial_1");
        inkTypewriterText.StartDialogue();
    }
}
