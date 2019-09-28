using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpellAdvance : MonoBehaviour
{
    ObjectPooler objectPooler;

    public List<SpellCard> spellRecipes;
    public List<TextMeshProUGUI> spellNames;
    public TextMeshProUGUI spellAdvanceVisual;

    public string beforeMerge;
    public string afterMerge;
    public List<int> whichFlashing;
    public List<int> whichFlashingAfter;

    public Animator canvasAnim;
    public Animator advanceAnim;
    int currentSpell = 0;

    
    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        canvasAnim = GameObject.Find("Canvas").GetComponent<Animator>();
        if (advanceAnim == null)
        {
            advanceAnim = GetComponent<Animator>();
        }
    }
    public void InitialSetup(SpellCard s)
    {
        spellRecipes.Add(s);
        
    }

    public void SetSpellNames()
    {

        StartCoroutine("ExpandList");
        
        //advanceAnim.Play("SpellAdvanceFlashes");
    }

    public void ShowAdvancedSpell()
    {
        foreach(TextMeshProUGUI text in spellNames)
        {
            text.text = "";
            text.gameObject.GetComponent<Animator>().SetBool("isFlashing", false);
            text.color = new Color(1, 1, 1, 1);
            text.gameObject.SetActive(false);
        }
        canvasAnim.Play("Flash");

        string _names = afterMerge;
        string[] _separatedNames = _names.Split(new string[] { " " }, System.StringSplitOptions.None);

        //spellNames[0].gameObject.SetActive(true);
        //spellNames[0].text = spellRecipes[currentSpell].advancedSpellComponents[0].advancedSpell.name;

        for (int j = 0; j < _separatedNames.Length - 1; j++)
        {
            spellNames[j].gameObject.SetActive(true);

            Animator _tempAnim = spellNames[j].gameObject.GetComponent<Animator>();

            _tempAnim.Play("Visible", 1);

            foreach(int num in whichFlashingAfter)
            {
                if(j == num)
                {
                    _tempAnim.Play("FlashingText", 0);
                }
            }

            spellNames[j].text = _separatedNames[j];
        }

        StartCoroutine("StartRound");
    }

    IEnumerator ExpandList()
    {

        
        foreach(int num in whichFlashing)
        {
            spellNames[num].gameObject.SetActive(true);
            spellNames[num].gameObject.GetComponent<Animator>().SetBool("isFlashing", true);
            spellNames[num].gameObject.GetComponent<Animator>().Play("FlashingText");

        }

        //string _names = spellRecipes[currentSpell].advancedSpellComponents[0].advancedRecipe;
        string _names = beforeMerge;
        string[] _separatedNames = _names.Split(new string[] { " " }, System.StringSplitOptions.None);
        Debug.Log(_separatedNames.Length);
        for (int j = 0; j < _separatedNames.Length - 1; j++)
        {
            spellNames[j].gameObject.SetActive(true);
            
            spellNames[j].gameObject.GetComponent<Animator>().Play("Visible",1);
            
            spellNames[j].text = _separatedNames[j];
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(1f);
        ShowAdvancedSpell();
    }

    IEnumerator StartRound()
    {
        yield return new WaitForSeconds(1);

        

        foreach (TextMeshProUGUI text in spellNames)
        {
            text.text = "";
            text.gameObject.GetComponent<Animator>().SetBool("isFlashing", false);
            text.color = new Color(1, 1, 1, 0);
            text.gameObject.SetActive(false);
        }

        canvasAnim.Play("SpellAdvanceEnd");
        //objectPooler.UnPauseAll();
    }

}
