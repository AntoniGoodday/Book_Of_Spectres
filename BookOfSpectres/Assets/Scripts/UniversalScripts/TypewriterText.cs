using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterText : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    int totalVisibleCharacters;
    float speed = 1f;
    void Start()
    {
        textMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
        
        
        StartCoroutine(RevealText());
    }

    IEnumerator RevealText()
    {
        textMeshProUGUI.ForceMeshUpdate();
        totalVisibleCharacters = textMeshProUGUI.textInfo.characterCount;
        Debug.Log(totalVisibleCharacters);
        int counter = 0;

        while (true)
        {
            

            

            int visibleCount = counter % (totalVisibleCharacters + 1);

            textMeshProUGUI.maxVisibleCharacters = visibleCount;

            if(visibleCount >= totalVisibleCharacters)
            {
                yield return new WaitForSeconds(speed);
            }

            counter++;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
