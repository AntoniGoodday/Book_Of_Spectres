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

    
     public virtual void Start()
    {
        
        objectPooler = ObjectPooler.Instance;
        objectPooler.allPooledObjects.Add(gameObject);
        anim = GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        UpdateUI();
    }

    public void DealDamage(int damage, float zPos, float amplitudeModifier = 0.05f)
    {
        hp -= damage;
        UpdateUI();
        int _clampedDamage = Mathf.Clamp(damage/10, 1, 10);
        
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
        anim.Play("Die");
    }

}
