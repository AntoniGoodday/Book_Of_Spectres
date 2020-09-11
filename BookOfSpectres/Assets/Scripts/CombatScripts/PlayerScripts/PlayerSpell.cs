using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControlNamespace;
public class PlayerSpell : MonoBehaviour, ICombatSpell
{
    [SerializeField]
    CardHolder cardHolder;
    public CardHolder CardHolder { get => cardHolder; set => cardHolder = value; }
    PlayerScript playerScript;

    PlayerControl.DefaultControlsActions spellControls;
    private void Start()
    {
        playerScript = PlayerScript.Instance;

        spellControls = playerScript.playerControl.DefaultControls;

        spellControls.Spell.canceled += context => UseSpell();
    }

    public void InitializeSpell()
    {
        cardHolder = GameObject.Find("PlayerCanvas").GetComponent<CardHolder>();
        cardHolder.Initialize();
        cardHolder.Purge();
    }

    public void UseSpell()
    {
        Debug.Log("spell");
        if (!playerScript.IsPaused && !playerScript.Dying)
        {
            Debug.Log(cardHolder.spellMiniatures.Count);
            if (cardHolder.spellMiniatures.Count > 0)
            {
                Debug.Log("spell used");
                cardHolder.UseSpell(gameObject, playerScript.status, playerScript.spellOrigin[0].transform);
                playerScript.anim.Play("Spell");
                playerScript.spellParticles.Play();
            }
        }
    }
}
