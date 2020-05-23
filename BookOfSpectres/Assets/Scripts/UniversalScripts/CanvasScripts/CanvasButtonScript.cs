using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CanvasButtonScript : MonoBehaviour
{
    private void Start()
    {
        GetComponentInChildren<Button>().Select();
    }
    public void ChooseLevel(string i)
    {
        SceneManager.LoadScene(i);
    }
}
