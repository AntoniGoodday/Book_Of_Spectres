using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatIntro : MonoBehaviour
{
    public static CombatIntro current;

    //public event Action<>

    private void Awake()
    {
        current = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
