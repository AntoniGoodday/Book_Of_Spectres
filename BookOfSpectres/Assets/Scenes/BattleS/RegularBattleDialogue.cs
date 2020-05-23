using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBattleDialogue : MonoBehaviour
{
    InkTypewriterText inkTypewriterText;
    ObjectPooler objectPooler;
    [SerializeField]
    string storyBegin;
    [SerializeField]
    string storyEnd;
    // Start is called before the first frame update
    void Start()
    {
        inkTypewriterText = GameObject.Find("DialogueCanvas").GetComponent<InkTypewriterText>();
        objectPooler = ObjectPooler.Instance;
        StartCoroutine(LateStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.001f);
        if (storyBegin != "")
        {
            inkTypewriterText.story.ChoosePathString(storyBegin);
            inkTypewriterText.StartDialogue();

            objectPooler.StartWave();

            objectPooler.DialogueKnot = storyEnd;
        }
        else
        {
            inkTypewriterText.gameObject.SetActive(false);
            objectPooler.SetLowpass(22000);
            objectPooler.StartWave();
        }
    }
}
