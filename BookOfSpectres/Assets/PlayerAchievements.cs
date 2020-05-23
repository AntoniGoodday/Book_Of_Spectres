using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EnumScript;
using UnityEngine.SceneManagement;
public class PlayerAchievements : MonoBehaviour
{

    [SerializeField]
    private int shotBasicAttacks = 0;
    [SerializeField]
    int shotChargedAttacks = 0;
    [SerializeField]
    int numberOfMoves = 0;
    [SerializeField]
    int projectilesDodged = 0;
    [SerializeField]
    int consecutiveProjectilesDodged = 0;
    [SerializeField]
    int recordConsecutiveProjectilesDodged = 0;
    [SerializeField]
    int timesHit = 0;

    public int ShotBasicAttacks { get => shotBasicAttacks; set => shotBasicAttacks = value; }
    public int ConsecutiveProjectilesDodged { get => consecutiveProjectilesDodged; set => consecutiveProjectilesDodged = value; }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch(scene.name)
        {
            case ("BattleScene"):
                {
                    FindObjectOfType<PlayerScript>().shootEvent += OnPlayerShot;
                    FindObjectOfType<PlayerScript>().chargedShootEvent += OnPlayerChargedShot;
                    FindObjectOfType<PlayerScript>().moveEvent += OnPlayerMove;
                    FindObjectOfType<PlayerStatus>().hitEvent += OnPlayerHit;
                    break;
                }
        }
    }

    public void OnPlayerShot()
    {
        ShotBasicAttacks++;
    }
    public void OnPlayerChargedShot()
    {
        shotChargedAttacks++;
    }

    public void OnPlayerMove(MoveDirection moveDir)
    {
        numberOfMoves++;
    }

    public void OnProjectileDodge()
    {
        projectilesDodged++;

        ConsecutiveProjectilesDodged++;

        if(ConsecutiveProjectilesDodged > recordConsecutiveProjectilesDodged)
        {
            recordConsecutiveProjectilesDodged++;
        }
    }

    public void OnPlayerHit()
    {
        timesHit++;
        ConsecutiveProjectilesDodged = 0;
    }


    public void AddProjectileToTrack(ProjectileScript projectile)
    {
        projectile.dodgeEvent += OnProjectileDodge;
    }

}
