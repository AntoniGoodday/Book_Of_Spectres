using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ManaScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI manaAmount;
    [SerializeField]
    Image manaBar;
    public int currentAmount;
    public int maxAmount;
    public void InitialSetText(int _currentAmount, int _maxAmount)
    {
        manaAmount.text = _currentAmount + "/" + _maxAmount;
        manaBar.fillAmount = (float)_currentAmount / (float)_maxAmount;
        currentAmount = _currentAmount;
        maxAmount = _maxAmount;
    }
    public void SetText()
    {
        manaAmount.text = currentAmount + "/" + maxAmount;
        manaBar.fillAmount = (float)currentAmount / (float)maxAmount;
        //currentAmount = _amount;
    }
}
