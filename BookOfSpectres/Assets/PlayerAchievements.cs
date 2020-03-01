using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAchievements : MonoBehaviour
{

    [SerializeField]
    int shotBasicAttacks = 0;
    [SerializeField]
    int shotChargedAttacks = 0;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerScript>().shootEvent += OnPlayerShot;
        FindObjectOfType<PlayerScript>().chargedShootEvent += OnPlayerChargedShot;
    }

    public void OnPlayerShot()
    {
        shotBasicAttacks++;
    }
    public void OnPlayerChargedShot()
    {
        shotChargedAttacks++;
    }


}
