using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
public class SpellAdvance : MonoBehaviour
{
    ObjectPooler objectPooler;
    [SerializeField]
    Color flashColour;

    public List<SpellCard> spellRecipes;
    public List<TextMeshProUGUI> spellNames;
    public TextMeshProUGUI spellAdvanceVisual;

    public string beforeMerge;
    public string afterMerge;
    public List<int> whichFlashing;
    public List<int> whichFlashingAfter;

    public Animator canvasAnim;
    public Animator advanceAnim;
    CardHolder cardHolder;
    int currentSpell = 0;

    [SerializeField]
    CanvasGroup darkness;


    private void Start()
    {
        
    }
    public void InitialSetup(SpellCard s)
    {
        //beforeMerge = "";
        //afterMerge = "";
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
            if (text.gameObject.GetComponent<Animator>().isActiveAndEnabled)
            {
                text.gameObject.GetComponent<Animator>().SetBool("isFlashing", false);
            }
            text.color = new Color(1, 1, 1, 1);
            text.gameObject.SetActive(false);
        }

        CombatMenu.Instance.FlashScreen("MenuClosed");
        //canvasAnim.Play("Flash");

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

        beforeMerge = "";
        afterMerge = "";

        StartCoroutine("StartRound");
    }

    public void LoadSpellAdvance()
    {
        objectPooler = ObjectPooler.Instance;
        cardHolder = GameObject.Find("PlayerCanvas").GetComponent<CardHolder>();
        canvasAnim = GameObject.Find("CombatCanvas").GetComponent<Animator>();
        if (advanceAnim == null)
        {
            advanceAnim = GetComponent<Animator>();
        }
    }

    IEnumerator ExpandList()
    {
        float flashTime = 0.3f * whichFlashing.Count + 1;
        DOTween.Init();
        foreach(int num in whichFlashing)
        {
            GameObject _currentText = spellNames[num].gameObject;

            _currentText.SetActive(true);



            _currentText.GetComponent<Animator>().SetBool("isFlashing", true);

            _currentText.GetComponent<Animator>().Play("FlashingText");

        }

        //string _names = spellRecipes[currentSpell].advancedSpellComponents[0].advancedRecipe;
        string _names = beforeMerge;
        string[] _separatedNames = _names.Split(new string[] { " " }, System.StringSplitOptions.None);
        
        for (int j = 0; j < _separatedNames.Length - 1; j++)
        {
            GameObject _currentText = spellNames[j].gameObject;
            _currentText.SetActive(true);

            _currentText.gameObject.GetComponent<Animator>().Play("Visible",1);

            

            spellNames[j].text = _separatedNames[j];
            yield return new WaitForSecondsRealtime(0.3f);
        }
        yield return new WaitForSecondsRealtime(1f);
        ShowAdvancedSpell();
    }

    IEnumerator StartRound()
    {
        yield return new WaitForSecondsRealtime(1);

        

        foreach (TextMeshProUGUI text in spellNames)
        {
            text.text = "";
            text.gameObject.GetComponent<Animator>().SetBool("isFlashing", false);
            text.color = new Color(1, 1, 1, 0);
            text.gameObject.SetActive(false);
        }

        cardHolder.SetSpellName();

        /*DOTween.Init();
        Sequence _spellAdvanceSequence = DOTween.Sequence();
        CombatMenu _cm = CombatMenu.Instance;

        CanvasGroup _visual = this.GetComponent<CanvasGroup>();

        _spellAdvanceSequence.Append(DOTween.To(() => _visual.alpha, x => _visual.alpha = x, 0, 0.5f))
                .Join(DOTween.To(() => darkness.alpha, x => darkness.alpha = x, 0f, 0.5f))
                .OnComplete(() => _cm.MenuUnPause())
                .SetUpdate(true)
                .Play();*/

        //CombatMenu.Instance.TweenAction = () => CombatMenu.Instance.UnDarkenScreen();
        CombatMenu.Instance.UnDarkenScreen("MenuUnPause");
        //canvasAnim.Play("SpellAdvanceEnd");
        
        //objectPooler.UnPauseAll();
    }

}
