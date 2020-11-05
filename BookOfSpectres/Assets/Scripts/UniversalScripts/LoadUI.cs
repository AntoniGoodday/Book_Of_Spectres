using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadUI : MonoBehaviour
{
    AsyncOperation asynchScene;

    public delegate void UILoadDelegate();
    public static event UILoadDelegate UILoadEvent;

    public bool loadExternalScene = true;

    bool alreadyLoaded = false;

    public static LoadUI Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this.gameObject);
        }

        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(alreadyLoaded == false)
        {
            alreadyLoaded = true;
            StartCoroutine(WaitTillLoad());
            
        }
        /*if (scene.name.ToLower().Contains("battle") && loadExternalScene == true)
        {
            StartCoroutine(WaitTillLoad());
        }
        else
        {
            loadExternalScene = false;
        }*/

    }

    IEnumerator WaitTillLoad()
    {
        asynchScene = SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);
        while (!asynchScene.isDone)
        {
            yield return null;
        }
        SceneManager.UnloadSceneAsync("UIScene");
    }
}
