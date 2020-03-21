using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
public class TileClass : MonoBehaviour
{
    [SerializeField]
    public int tileID;
    [SerializeField]
    public TileAlignment tileAlignment = TileAlignment.Friendly;
    [SerializeField]
    TileAlignment initialAlignment;
    [SerializeField]
    TileEffect initialTileEffect = TileEffect.Standard;
    [SerializeField]
    bool returnToStandard = true;
    [SerializeField]
    public bool occupied = false;
    [SerializeField]
    public int occupierPriority = 0;
    [SerializeField]
    public Color initialMaterialColour;
    [SerializeField]
    public Color currentColour;
    [SerializeField]
    public int column;
    [SerializeField]
    public int row;
    [SerializeField]
    public Vector2Int gridLocation;
    [SerializeField]
    public Dictionary<Direction, GameObject> tileNeighbours = new Dictionary<Direction, GameObject>();
    public Renderer rend;
    public SpriteRenderer sRend;

    public TileEffect tileEffect;

    bool effectResetIsRunning = false;
    bool alignmentResetIsRunning = false;
    Vector3 initialPosition;
    // Use this for initialization
    private void Awake()
    {
        tileEffect = initialTileEffect;
        initialPosition = transform.localPosition;
        CheckEffect(tileEffect,true);
        sRend = GetComponentInChildren<SpriteRenderer>(true);
        //rend = gameObject.GetComponent<Renderer>();
    }
    void Start()
    {
        
    }
    public void SetColour(Color c, bool setup = false, bool overrideColour = false, bool affectsCracked = false, int priority = 10)
    {
        if(priority >= occupierPriority)
        {
            occupierPriority = priority;
            rend = gameObject.GetComponent<Renderer>();
            currentColour = c;

            var tempMaterial = new Material(rend.sharedMaterial)
            {
                color = c
            };

            if (setup == true)
            {
                rend.material.SetColor("_BaseColor", initialMaterialColour);
            }
            else if (setup == false)
            {

                if (c == initialMaterialColour)
                {
                    rend.material.SetColor("_BaseColor", initialMaterialColour);
                    UnOccupy(affectsCracked);
                }
                else
                {
                    if (overrideColour == false)
                    {
                        rend.material.SetColor("_BaseColor", initialMaterialColour - tempMaterial.color);
                    }
                    else
                    {
                        rend.material.SetColor("_BaseColor", tempMaterial.color);
                    }
                    Occupy();
                }

            }

            if (GetComponentInChildren<SpriteRenderer>() == true)
            {
                //sRend = GetComponentInChildren<SpriteRenderer>();
                //sRend.sharedMaterial = tempMaterial;
            }

        }


    }

    
   
    

    public void Occupy()
    {

        transform.localPosition = initialPosition + new Vector3(0, Mathf.Lerp(0, 0.1f, 1), 0);
    }

    //Make tile not occupied
    public void UnOccupy(bool affectsCracked)
    {
        if(affectsCracked == true && tileEffect == TileEffect.Cracked)
        {
            CheckEffect(TileEffect.Cracked);
        }
        occupierPriority = 0;
        transform.localPosition = initialPosition;
    }

    public void SetVariables()
    {
        column = (int)transform.position.x + 1;
        row = (int)transform.position.y + 1;
        initialAlignment = tileAlignment;
    }

    public void DebugCurrentTile()
    {
        Debug.Log("Current tile is: " + gameObject.name);
        foreach(KeyValuePair<Direction, GameObject> d in tileNeighbours)
        {
            Debug.Log(d.Key);
            Debug.Log(d.Value);
        }
        Debug.Log("End of: " + gameObject.name);
    }

    public void ChangeAlignment(Color c, TileAlignment _t)
    {
        StopCoroutine("ReturnToInitialAlignment");
        SetColour(c,false);
        tileAlignment = _t;
        StartCoroutine("ReturnToInitialAlignment");
    }

    public void CheckEffect(TileEffect _tEffect, bool setup = false)
    {
        StopCoroutine("ReturnToInitialEffect");
        switch (_tEffect)
        {
            case (TileEffect.Broken):
                {
                    if (occupied == false)
                    {
                        sRend.enabled = false;
                        rend.enabled = false;
                    }
                    else
                    {
                        CheckEffect(TileEffect.Cracked);
                        return;
                    }
                    break;
                }
            case (TileEffect.Cracked):
                {
                    if (tileEffect != TileEffect.Cracked)
                    {
                        sRend.gameObject.SetActive(true);
                        tileEffect = _tEffect;
                        return;
                    }
                    else
                    {
                        if(setup == true)
                        {
                            sRend.gameObject.SetActive(true);
                            tileEffect = _tEffect;
                            return;
                        }
                        CheckEffect(TileEffect.Broken);    
                        return;
                    }
                }


        }
        tileEffect = _tEffect;
        if (setup == false)
        {
            StartCoroutine("ReturnToInitialEffect");
        }


    }

    IEnumerator ReturnToInitialEffect()
    {

        yield return new WaitForSeconds(10);
        if(returnToStandard == false)
        {
            InitialEffect();
        }
        else
        {
            Standard();
        }
        
    }

    void InitialEffect()
    {
        if (rend.enabled == false)
        {
            rend.enabled = true;
        }
        if (initialTileEffect == TileEffect.Cracked)
        {
            if (sRend.sprite != null)
            {
                sRend.gameObject.SetActive(true);
            }
        }
        tileEffect = initialTileEffect;
    }

    void Standard()
    {
        if (rend.enabled == false)
        {
            rend.enabled = true;
        }

        if (sRend.sprite != null)
        {
            sRend.gameObject.SetActive(false);
        }
        tileEffect = TileEffect.Standard;
    }

    IEnumerator ReturnToInitialAlignment()
    {

        yield return new WaitForSeconds(10);

        tileAlignment = initialAlignment;
        SetColour(initialMaterialColour);
    }
}
