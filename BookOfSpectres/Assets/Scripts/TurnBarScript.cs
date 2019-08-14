using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnBarScript : MonoBehaviour
{
    Image turnBar;
    [SerializeField]
    public float maxTurnTime = 10f;
    [SerializeField]
    float speedModifier;
    public float currentTurnTime;

    Color barColour;
    // Use this for initialization
    void Start()
    {
        turnBar = GetComponent<Image>();
        barColour = turnBar.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTurnTime == 0)
        {
            turnBar.color = barColour;
        }
        if (currentTurnTime < maxTurnTime)
        {
            currentTurnTime += Time.deltaTime*speedModifier;
        }
        else
        {
            turnBar.color = new Color(1, 1, 0);
        }

        turnBar.fillAmount = currentTurnTime / maxTurnTime;
    }
}
