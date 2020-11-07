using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnBarScript : MonoBehaviour
{
    public static TurnBarScript Instance;
    Image turnBar;
    [SerializeField]
    float maxTurnTime = 10f;
    [SerializeField]
    float speedModifier;
    [SerializeField]
    float currentTurnTime;
    [SerializeField]
    Material material;

    public bool isPaused = false;

    Color barColour;

    public float SpeedModifier { get => speedModifier; set => speedModifier = value; }
    public float MaxTurnTime { get => maxTurnTime; set => maxTurnTime = value; }
    public float CurrentTurnTime { get => currentTurnTime; set => currentTurnTime = value; }

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
            if (CurrentTurnTime == 0)
            {
                turnBar.material = null;
                turnBar.color = barColour;
            }
            if (CurrentTurnTime < MaxTurnTime)
            {
                CurrentTurnTime += Time.deltaTime * SpeedModifier;
            }
            else if (CurrentTurnTime >= MaxTurnTime)
            {
                turnBar.material = material;
                turnBar.color = new Color(0, 0, 0);
            }

            turnBar.fillAmount = CurrentTurnTime / MaxTurnTime;
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
            CurrentTurnTime = 0;
            turnBar.material = null;
            turnBar.color = barColour;
        }
        else
        {
            isPaused = true;
            CurrentTurnTime = ctt;
        }
        turnBar.fillAmount = CurrentTurnTime / MaxTurnTime;
    }
    public void UnPause(float changeTime = 0)
    {
        CurrentTurnTime += changeTime;
        isPaused = false;
        turnBar.fillAmount = CurrentTurnTime / MaxTurnTime;
    }
}
