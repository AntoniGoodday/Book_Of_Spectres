﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EnumScript;
using TMPro;
using Cinemachine;

public class EntityStatus : MonoBehaviour
{


    ObjectPooler objectPooler;
    public float maxHp;
    public float hp;
    [SerializeField]
    List<StatusEffect> statusEffects;
    [SerializeField]
    public ParticleSystem hitParticles;
    [SerializeField]
    public Facing directionFacing;

    [SerializeField]
    AudioClip hitSound;
    [SerializeField]
    AudioClip hitWeak;

    AudioSource aSource;

    [SerializeField]
    GameObject hitLocation;
    [SerializeField]
    List<int> hitLayers;
    [SerializeField]
    public Animator anim;
    [SerializeField]
    CinemachineImpulseSource impulseSource;

    public AiMastermind aiMastermind;

    public delegate void DieDelegate();
    public event DieDelegate dieEvent;

    [SerializeField]
    bool isPaused = false;
    [SerializeField]
    bool isDying = false;
    [SerializeField]
    bool canBeCountered = false;
    [SerializeField]
    bool isInterrupted = false;




    public List<StatusEffect> StatusEffects { get => statusEffects; set => statusEffects = value; }
    public List<int> HitLayers { get => hitLayers; set => hitLayers = value; }

    public bool IsDying { get => isDying; set => isDying = value; }
    public bool IsPaused { get => isPaused; set => isPaused = value; }
    public bool CanBeCountered { get => canBeCountered; set => canBeCountered = value; }
    public bool IsInterrupted { get => isInterrupted; set => isInterrupted = value; }

    public virtual void OnEnable()
    {
        GetComponent<Collider>().enabled = true;
        //UpdateUI();
    }

    public virtual void Start()
    {
        objectPooler = ObjectPooler.Instance;
        aiMastermind = AiMastermind.Instance;

        objectPooler.allPooledObjects.Add(gameObject);
        EnemyStart();

        anim = GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();

        aSource = GetComponent<AudioSource>();
        UpdateUI();
        
    }

   

    public virtual void DealDamage(int damage, float zPos = -1.4f, float amplitudeModifier = 0.05f, BulletAlignement damageSource = BulletAlignement.Enemy)
    {
        hp -= damage;
        UpdateUI();
        int _clampedDamage = Mathf.Clamp(damage/10, 0, 10);
        aSource.pitch = Random.Range(0.8f, 1.2f);
        if (damage < 20)
        {
            aSource.PlayOneShot(hitWeak);
        }
        else
        {
            aSource.PlayOneShot(hitSound);
        }
        
        if (hitParticles != null)
        {

            GetHit(damage, zPos, amplitudeModifier, _clampedDamage);

        }

        if(hp <= 0 && !statusEffects.Contains(StatusEffect.Endure))
        {
            Die();
            
            //gameObject.SetActive(false);
            
        }
        else if(hp <= 0 && statusEffects.Contains(StatusEffect.Endure))
        {
            hp = 1;
            if(maxHp <= 0)
            {
                maxHp = 1;
            }
            UpdateUI();
        }
    }

    public virtual void GetHit(int damage, float zPos, float amplitudeModifier, int _clampedDamage)
    {
        hitLocation.transform.localPosition = new Vector3(0, zPos + 1.4f, 0);

        PlayHitAnim(damage);
        
        impulseSource.m_ImpulseDefinition.m_AmplitudeGain = damage * amplitudeModifier;
        impulseSource.GenerateImpulse();
        hitParticles.Emit(_clampedDamage);



        StartCoroutine("PauseGame", damage);
    }

    public virtual void PlayHitAnim(int damage)
    {

    }

    public virtual void UpdateUI()
    {

    }

    public virtual void SetHP(int _maxHP, int _hp = 0, bool _equalToMax = true)
    {
        
        if(_equalToMax == true)
        {
            maxHp = _maxHP;
            hp = _maxHP;
        }
        else
        {
            maxHp = _maxHP;
            if(_hp < 1)
            {
                hp = 1;
            }
            else
            {
                hp = _hp;
            }
        }
    }

    public virtual void ProgressWave()
    {
        objectPooler.EnemyDefeated();
        gameObject.SetActive(false);
    }
    public virtual void StartDying()
    {
        //anim.SetBool("isDying", true);
    }

    public virtual void Die()
    {
        aiMastermind.enemies.Remove(gameObject);
        if(GetComponent<EnemyAI>().hasAttackToken == true)
        {
            for (int i = 0; i < aiMastermind.attackTokens.Count; i++)
            {
                if (aiMastermind.attackTokens[i] == false)
                {
                    aiMastermind.attackTokens[i] = true;
                    aiMastermind.StartCoroutine("GiveToken",1);
                    break;
                }
            }
            GetComponent<EnemyAI>().hasAttackToken = false;
        }
        dieEvent?.Invoke();
        GetComponent<EnemyAI>().die = true;
        GetComponent<Collider>().enabled = false;
        anim.Play("Die");    
    }

    IEnumerator PauseGame(float damage)
    {
        Time.timeScale = 0f;
        float _t;
        if (damage > 30 && damage < 50)
        {
            _t = 0.016f;
            yield return new WaitForSecondsRealtime(_t);
        }
        else if(damage >= 50)
        {
            _t = 0.016f*(damage/50);
            yield return new WaitForSecondsRealtime(_t);
        }
        Time.timeScale = 1;
    }

    public virtual void EnemyStart()
    {
        aiMastermind.enemies.Add(this.gameObject);
    }

    public virtual void Initialize()
    {

    }

    public virtual void Paused()
    {
        IsPaused = true;
    }

    public virtual void UnPaused()
    {
        IsPaused = false;
    }
}