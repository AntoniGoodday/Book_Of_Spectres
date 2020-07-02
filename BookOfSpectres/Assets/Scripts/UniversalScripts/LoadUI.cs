using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadUI : MonoBehaviour
{
    AsyncOperation scene;

    public delegate void UILoadDelegate();
    public static event UILoadDelegate UILoadEvent;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    void Start()
    {

        if (!CombatMenu.Instance && !InkTypewriterText.Instance)
        { 
            StartCoroutine(WaitTillLoad());
        }
    }

   

    IEnumerator WaitTillLoad()
    {
        scene = SceneManager.LoadSceneAsync("BattleUIScene", LoadSceneMode.Additive);
        while (!scene.isDone)
        {
            yield return null;
        }
        if (SceneManager.GetActiveScene().buildIndex > 2)
        {
            UILoadEvent?.Invoke();
            CombatMenu.Instance.LoadPlayerStats();
        }
        SceneManager.UnloadSceneAsync("BattleUIScene");
    }
}
