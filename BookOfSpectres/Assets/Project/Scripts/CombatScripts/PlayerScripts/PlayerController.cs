using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EnumScript;
public class PlayerController : MonoBehaviour
{
    PlayerController Instance;
    [SerializeField]
    private EntityScript playerScript;

    private PlayerAttributes playerAttributes;

    private CombatMenu combatMenu;
    private TurnBarScript turnBarScript;
    

    public EntityScript PlayerScript { get => playerScript; set => playerScript = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            //DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        playerAttributes = PlayerAttributes.Instance;
        combatMenu = CombatMenu.Instance;
        turnBarScript = TurnBarScript.Instance;
        playerScript.Status.SetHP(playerAttributes.maxHp);
    }

    private void Update()
    {

    }

    public void SpellMenu()
    {
        if (turnBarScript.CurrentTurnTime >= turnBarScript.MaxTurnTime)
        {
            if (playerScript.Status.IsPaused == false)
            {
                //objectPooler.PauseAll();



                List<SpellCard> _tempSpellList = new List<SpellCard>();

                foreach (SpellCard s in combatMenu.playerCombatInUse)
                {
                    _tempSpellList.Add(s);
                }

                foreach (SpellCard s in _tempSpellList)
                {
                    combatMenu.MoveCardToDestination(s, CardDestination.Combat, CardDestination.Graveyard);
                }

                playerScript.GetComponent<ICombatSpell>().CardHolder.Purge();
                turnBarScript.Pause(false);

                combatMenu.TweenMenu();
                //EventSystem.current.SetSelectedGameObject(null);
                //EventSystem.current.SetSelectedGameObject(firstButton);
            }
            else
            {
                //EventSystem.current.SetSelectedGameObject(null);
                //objectPooler.UnPauseAll();
            }
        }
    }

}
