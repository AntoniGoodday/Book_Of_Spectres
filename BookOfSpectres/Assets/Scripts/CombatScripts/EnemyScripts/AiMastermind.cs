using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class AiMastermind : MonoBehaviour
{
    public static AiMastermind Instance;
    [SerializeField]
    public GameObject player;
    [SerializeField]
    public List<GameObject> enemies;

    

    [SerializeField]
    public List<bool> attackTokens;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        StartCoroutine("GiveToken");
    }


    IEnumerator GiveToken()
    {
        yield return new WaitForSeconds(1);
        if (enemies.Count > 0)
        {
            for (int i = 0; i < attackTokens.Count; i++)
            {
                if (attackTokens[i] == true)
                {
                    for (int j = 0; j < attackTokens.Count; j++)
                    {
                        if (enemies[j].GetComponent<Animator>().GetBool("AttackToken") == false)
                        {
                            enemies[j].GetComponent<Animator>().SetBool("AttackToken", true);
                            attackTokens[i] = false;
                            i = attackTokens.Count;
                            GameObject previousEnemy = enemies[j];
                            enemies.Remove(previousEnemy);
                            enemies.Add(previousEnemy);
                            break;
                        }
                    }
                }
            }
            if (attackTokens.All(y => y))
            {

            }
            else
            {
                StartCoroutine("GiveToken");
            }
        }
        else
        {
            StartCoroutine("GiveToken");
        }
    }
}
