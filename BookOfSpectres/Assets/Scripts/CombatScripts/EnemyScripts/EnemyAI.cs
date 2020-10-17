using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
using System;
public class EnemyAI : MonoBehaviour
{
    
    EnemyScript enemy;
    PlayerScript player;
    BattlefieldScript bfs;
    Animator anim;
    State currentState;
    public AiMastermind aiMastermind;

    public bool isInCounterState = false;
    public bool canBeCounteredAgain = true;
    public bool wait = false;
    public bool die = false;

    public bool hasAttackToken = false;

    [SerializeField]
    string stateName;

    [SerializeField]
    AIType aiType =  new AIType();

    [SerializeField]
    public Dictionary<Type, State> customStates;


    public void InitializeStateMachine()
    {
        var commonStates = new Dictionary<Type, State>()
        {

            {typeof(EnemyIdle), new EnemyIdle(enemy, bfs, anim, player, this) },
            {typeof(Move), new MoveVertical(enemy, bfs, anim, player, this) },
            {typeof(Attack), new Attack(enemy, bfs, anim, player, this) }
            
        };

        customStates = commonStates;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy = this.GetComponent<EnemyScript>();
        player = PlayerScript.Instance;
        bfs = BattlefieldScript.Instance;
        anim = this.GetComponent<Animator>();
        aiMastermind = AiMastermind.Instance;

        InitializeStateMachine();

        currentState = new StartCombat(enemy, bfs, anim, player, this);
    }

    public void Die()
    {
        currentState = new StartCombat(enemy, bfs, anim, player, this);
        stateName = currentState.name.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (die == false)
        {
            currentState = currentState.Process();
            stateName = currentState.name.ToString();
        }
    }
}
