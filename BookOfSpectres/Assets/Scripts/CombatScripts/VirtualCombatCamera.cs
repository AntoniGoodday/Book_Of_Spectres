using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class VirtualCombatCamera : MonoBehaviour
{
    ObjectPooler objectPooler;
    public static VirtualCombatCamera Instance;
    CinemachineImpulseListener impulseListener;
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        impulseListener = GetComponent<CinemachineImpulseListener>();
        Instance = this;
        objectPooler.allPooledObjects.Add(gameObject);
    }
 #region
    public void Paused()
    {
        impulseListener.m_Gain = 0;

    }

    public void UnPaused()
    {
        impulseListener.m_Gain = 1;

    }
#endregion
}
