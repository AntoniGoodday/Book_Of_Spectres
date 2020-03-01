using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMastermind : MonoBehaviour
{
    public static AiMastermind Instance;
    [SerializeField]
    public GameObject player;
    [SerializeField]
    public List<GameObject> enemies;

    [SerializeField]
    bool attackToken;


    private void Start()
    {
        Instance = this;
    }

    IEnumerator GenerateToken()
    {
        yield return new WaitForSeconds(5);
    }
}
