using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnBarScript : MonoBehaviour
{
    public static TurnBarScript Instance;
    Image turnBar;
    [SerializeField]
    public float maxTurnTime = 10f;
    [SerializeField]
    float speedModifier;
    public float currentTurnTime;
    [SerializeField]
    Material material;

    public bool isPaused = false;

    Color barColour;
    // Use this for initialization
    void Start()
    {
        Instance = this;
        turnBar = GetComponent<Image>();
        barColour = turnBar.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused == false)
        {
            if (currentTurnTime == 0)
            {
                turnBar.material = null;
                turnBar.color = barColour;
            }
            if (currentTurnTime < maxTurnTime)
            {
                currentTurnTime += Time.deltaTime * speedModifier;
            }
            else if (currentTurnTime >= maxTurnTime)
            {
                turnBar.material = material;
                turnBar.color = new Color(0, 0, 0);
            }

            turnBar.fillAmount = currentTurnTime / maxTurnTime;
        }
    }

    public void Pause(bool keepTime = false, float ctt = 0)
    {
        if (keepTime == true)
        {
            isPaused = true;
        }
        else if(keepTime == false && ctt <= 0)
        {
            isPaused = true;
            currentTurnTime = 0;
            turnBar.material = null;
            turnBar.color = barColour;
        }
        else
        {
            isPaused = true;
            currentTurnTime = ctt;
        }
        turnBar.fillAmount = currentTurnTime / maxTurnTime;
    }
    public void UnPause(float changeTime = 0)
    {
        currentTurnTime += changeTime;
        isPaused = false;
        turnBar.fillAmount = currentTurnTime / maxTurnTime;
    }
}
