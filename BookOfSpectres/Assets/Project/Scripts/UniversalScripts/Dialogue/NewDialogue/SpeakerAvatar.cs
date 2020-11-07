using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpeakerAvatar : MonoBehaviour
{
    public SpeakerAsset speaker;


    public bool isActive = false;
    [SerializeField]
    Image image; 
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        if(speaker == null)
        {
            image.enabled = false;
        }
    }

    public void SetSpeaker(SpeakerAsset _spk, bool _active = true, int _expression = 0)
    {
        if(image == null)
        {
            image = GetComponent<Image>();
        }

        if(image.enabled == false)
        {
            image.enabled = true;
        }

        speaker = _spk;
        image.sprite = speaker.expressionSprites[_expression];
        image.SetNativeSize();

        if(_active)
        {
            image.color = Color.white;
        }
        else
        {
            image.color = Color.gray;
        }
    }

    public void GreyOut()
    {
        image.color = Color.gray;
    }

    public void ClearSpeaker()
    {
        speaker = null;
        image.color = Color.white;
        image.enabled = false;
    }
}
