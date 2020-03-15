using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineScript : MonoBehaviour
{
    public static CoroutineScript Instance;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void StartRemoteCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

   
}
