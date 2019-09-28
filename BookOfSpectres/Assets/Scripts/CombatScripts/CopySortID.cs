using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopySortID : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sr;
    [SerializeField]
    TrailRenderer tr;
    [SerializeField]
    ParticleSystem ps;

    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    TrailRenderer trailRenderer;
    [SerializeField]
    ParticleSystem particleSystem;
    ParticleSystemRenderer psr;
    [SerializeField]
    int offset;

    private void Start()
    {
        psr = particleSystem.GetComponent<ParticleSystemRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sr != null)
        {
            if(spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = sr.sortingOrder + offset;
            }
            else if (trailRenderer != null)
            {
                trailRenderer.sortingOrder = sr.sortingOrder + offset;
            }
            else if (particleSystem != null)
            {
                psr.sortingOrder = sr.sortingOrder + offset;
            }
        }
    }
}
