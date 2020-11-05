using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ClearTextOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = "";
    }
}
