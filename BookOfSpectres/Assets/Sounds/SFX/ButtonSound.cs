using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    AudioSource aSource;
    [SerializeField]
    AudioClip aClip;
    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        aSource = GetComponent<AudioSource>();
        aSource.enabled = true;
        aSource.pitch = Random.Range(0.8f, 1.2f);
        aSource.PlayOneShot(aClip);
    }
}
