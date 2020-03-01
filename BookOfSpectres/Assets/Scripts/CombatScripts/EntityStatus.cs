using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
using TMPro;
using Cinemachine.Editor;
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
    GameObject hitLocation;
    [SerializeField]
    List<int> hitLayers;
    [SerializeField]
    public Animator anim;

    CinemachineImpulseSource impulseSource;

    public AiMastermind aiMastermind;
     public virtual void Start()
    {
        objectPooler = ObjectPooler.Instance;
        aiMastermind = AiMastermind.Instance;

        objectPooler.allPooledObjects.Add(gameObject);
        EnemyStart();

        anim = GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        UpdateUI();
        
    }

    public void DealDamage(int damage, float zPos = -1.4f, float amplitudeModifier = 0.05f, BulletAlignement damageSource = BulletAlignement.Enemy)
    {
        hp -= damage;
        UpdateUI();
        int _clampedDamage = Mathf.Clamp(damage/10, 0, 10);
        
        if (hitParticles != null)
        {
            
            hitLocation.transform.localPosition = new Vector3(0, zPos + 1.4f, 0);
            foreach (int h in hitLayers)
            {
                anim.Play("Hit", h, 0f);
            }
            impulseSource.m_ImpulseDefinition.m_AmplitudeGain = damage * amplitudeModifier;
            impulseSource.GenerateImpulse();
            hitParticles.Emit(_clampedDamage);

            Debug.Log("emit " + _clampedDamage);

            StartCoroutine("PauseGame", damage);

        }
        if(hp <= 0)
        {
            Die();
            
            //gameObject.SetActive(false);
            
        }
    }

    public virtual void UpdateUI()
    {

    }

    public virtual void ProgressWave()
    {
        objectPooler.EnemyDefeated();
        gameObject.SetActive(false);
    }
    public virtual void StartDying()
    {
        anim.SetBool("isDying", true);
    }

    public virtual void Die()
    {
        aiMastermind.enemies.Remove(gameObject);
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
}
