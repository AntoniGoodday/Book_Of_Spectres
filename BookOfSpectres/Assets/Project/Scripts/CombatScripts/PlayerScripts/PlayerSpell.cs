using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerSpell : MonoBehaviour, ICombatSpell
{
    [SerializeField]
    CardHolder cardHolder;
    public CardHolder CardHolder { get => cardHolder; set => cardHolder = value; }
    PlayerScript playerScript;
    PlayerStatus status;
    [SerializeField]
    bool spellCharge = false;
    [SerializeField]
    float chargeAmount = 0;

    EntityInputManager inputManager;
    private void Start()
    {
        playerScript = PlayerScript.Instance;
        status = GetComponent<PlayerStatus>();
        inputManager = GetComponent<EntityInputManager>();
    }

    public void InitializeSpell()
    {
        cardHolder = GameObject.Find("PlayerCanvas").GetComponent<CardHolder>();
        cardHolder.Initialize();
        cardHolder.Purge();
    }

    public void UseSpell()
    {
        if (inputManager.strongAttack)
        {
            spellCharge = true;
            chargeAmount = inputManager.strongAttackHeldTime;
        }
        else
        {
            if (spellCharge == true)
            {
                if (cardHolder.spellMiniatures.Count > 0)
                {
                    cardHolder.UseSpell(gameObject, playerScript.Status, playerScript.spellOrigin[0].transform);
                    playerScript.anim.Play("Spell");
                    playerScript.spellParticles.Play();
                    chargeAmount = 0;
                    spellCharge = false;
                }
            }
        }
    }
}
