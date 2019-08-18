using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
using TMPro;
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
    ParticleSystem.Burst customBurst;

     public virtual void Start()
    {
        objectPooler = ObjectPooler.Instance;
        objectPooler.allPooledObjects.Add(gameObject);
        anim = GetComponent<Animator>();
        
        UpdateUI();
    }

    public void DealDamage(int damage, float zPos)
    {
        hp -= damage;
        UpdateUI();
        int _clampedDamage = Mathf.Clamp(damage/10, 1, 10);
        customBurst = new ParticleSystem.Burst(0, _clampedDamage);
        if (hitParticles != null)
        {
            hitParticles.emission.SetBurst(0, customBurst);
            hitLocation.transform.localPosition = new Vector3(0, 0, zPos);
            foreach (int h in hitLayers)
            {
                anim.Play("Hit", h, 0f);
            }
            hitParticles.Emit(_clampedDamage);

        }
        if(hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void UpdateUI()
    {

    }

}
