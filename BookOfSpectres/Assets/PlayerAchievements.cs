using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EnumScript;
using UnityEngine.SceneManagement;
public class PlayerAchievements : MonoBehaviour
{
    public static PlayerAchievements Instance;

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

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        Instance = this;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch(scene.name)
        {
            case ("BattleScene"):
                {
                    Debug.Log("Battle Achievs");
                    FindObjectOfType<PlayerScript>().playerShoot.ShootEvent += OnPlayerShot;
                    FindObjectOfType<PlayerScript>().playerShoot.ChargedShootEvent += OnPlayerChargedShot;
                    //FindObjectOfType<PlayerScript>().MoveEvent += OnPlayerMove;
                    FindObjectOfType<PlayerStatus>().hitEvent += OnPlayerHit;
                    break;
                }
        }

        if (FindObjectOfType<PlayerScript>())
        {
            FindObjectOfType<PlayerScript>().playerShoot.ShootEvent += OnPlayerShot;
            FindObjectOfType<PlayerScript>().playerShoot.ChargedShootEvent += OnPlayerChargedShot;
            //FindObjectOfType<PlayerScript>().MoveEvent += OnPlayerMove;
            FindObjectOfType<PlayerStatus>().hitEvent += OnPlayerHit;
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
