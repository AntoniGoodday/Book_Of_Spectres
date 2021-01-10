using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
using System;
public class EnemyAI : MonoBehaviour
{
    
    public EntityScript enemy;
    public PlayerScript player;
    public BattlefieldScript bfs;
    public Animator anim;
    public State currentState;
    public AiMastermind aiMastermind;
    public EntityInputManager entityInput;

    public Vector2 currentMovementInput;

    public bool isInCounterState = false;
    public bool canBeCounteredAgain = true;
    public bool wait = false;
    public bool die = false;

    public bool hasAttackToken = false;


    [SerializeField] private State initialState = null;

    public GameObject stateParent;

    private StateMachine stateMachine;

    private StateMachine StateMachine
    {
        get
        {
            if(stateMachine != null)
            {
                return stateMachine;
            }

            stateMachine = new StateMachine(initialState);

            return stateMachine;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentState = StateMachine.CurrentState;
        if (die == false)
        {
            StateMachine.StateMachineTick();
        }
    }

    public void ChangeState(State state)
    {
        StateMachine.ChangeState(state);
    }


    // Start is called before the first frame update
    void Start()
    {
        enemy = this.GetComponent<EntityScript>();
        player = PlayerScript.Instance;
        bfs = BattlefieldScript.Instance;
        anim = this.GetComponent<Animator>();
        entityInput = this.GetComponent<EntityInputManager>();
        aiMastermind = AiMastermind.Instance;
    }

    public void Die()
    {
        stateMachine.ChangeState(initialState);
    }

    
}
