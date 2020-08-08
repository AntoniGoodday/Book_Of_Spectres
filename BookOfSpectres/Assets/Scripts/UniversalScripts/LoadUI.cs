using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadUI : MonoBehaviour
{
    AsyncOperation scene;

    public delegate void UILoadDelegate();
    public static event UILoadDelegate UILoadEvent;

    public bool loadExternalScene = false;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    void Start()
    {

        if (!CombatMenu.Instance && !InkTypewriterText.Instance)
        {
            if (loadExternalScene == true)
            {
                StartCoroutine(WaitTillLoad());
            }
        }
    }

   

    IEnumerator WaitTillLoad()
    {
        scene = SceneManager.LoadSceneAsync("BattleUIScene", LoadSceneMode.Additive);
        while (!scene.isDone)
        {
            yield return null;
        }
        SceneManager.UnloadSceneAsync("BattleUIScene");
    }
}
