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

    ObjectPooler objectPooler;

    float initialWait = 10;
    float bonusWait = 0;

    float timeSinceStart = 0;

    // Use this for initialization
    private void Awake()
    {
        objectPooler = ObjectPooler.Instance;
        tileEffect = initialTileEffect;
        initialPosition = transform.localPosition;
        CheckEffect(tileEffect,true);
        sRend = GetComponentInChildren<SpriteRenderer>(true);
        sRend.enabled = false;
        sRend.gameObject.SetActive(true);
        //rend = gameObject.GetComponent<Renderer>();
    }
    void Start()
    {
        StartCoroutine(UpdateRealTime());
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
        transform.localPosition = initialPosition + new Vector3(0, 0, Mathf.Lerp(0, -0.1f, 1));
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
        occupied = false;
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
        StopCoroutine(ReturnToInitialAlignment());
        SetColour(c,false);
        tileAlignment = _t;
        StartCoroutine(ReturnToInitialAlignment());
    }

    public void CheckEffect(TileEffect _tEffect, bool setup = false)
    {
        
        //StopCoroutine(ReturnToInitialEffect());
        switch (_tEffect)
        {
            case (TileEffect.Broken):
                {
                    if (occupied == false)
                    {
                        sRend.enabled = false;
                        rend.enabled = false;
                        if (Resources.Load("Particles/TileBreakParticles") != null)
                        {
                            objectPooler.SpawnFromPool("TileBreakParticles", this.gameObject.transform.position - new Vector3(0, 0, 0), Quaternion.Euler(180, 0, 0), objectPooler.transform, (Resources.Load("Particles/TileBreakParticles") as GameObject));
                        }
                        gameObject.layer = 9;
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
                    if (tileEffect != TileEffect.Cracked && tileEffect != TileEffect.Broken)
                    {
                        sRend.enabled = true;
                        tileEffect = _tEffect;
                        if (Resources.Load("Particles/TileCrackParticles") != null)
                        {
                            objectPooler.SpawnFromPool("TileCrackParticles", this.gameObject.transform.position - new Vector3(0, 0, 0.75f), Quaternion.Euler(180, 0, 0), objectPooler.transform, (Resources.Load("Particles/TileCrackParticles") as GameObject));
                        }
                        return;
                    }
                    else
                    {
                        if(setup == true)
                        {
                            sRend.enabled = true;
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
            if (effectResetIsRunning == true)
            {
                bonusWait += initialWait;
            }
            else
            {
                StartCoroutine(ReturnToInitialEffect());
            }
        }


    }

    IEnumerator ReturnToInitialEffect()
    {
        effectResetIsRunning = true;
        yield return new WaitForSeconds(initialWait + bonusWait);
        if(returnToStandard == false)
        {
            InitialEffect();
        }
        else
        {
            Standard();
        }
        effectResetIsRunning = false;
        bonusWait = 0;
        
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
                sRend.enabled = true;
            }
        }
        tileEffect = initialTileEffect;

        gameObject.layer = 0;
    }

    void Standard()
    {
        if (rend.enabled == false)
        {
            rend.enabled = true;
        }

        if (sRend.sprite != null)
        {
            sRend.enabled = false; ;
        }
        tileEffect = TileEffect.Standard;

        gameObject.layer = 0;
    }

    IEnumerator ReturnToInitialAlignment()
    {

        yield return new WaitForSeconds(10);

        tileAlignment = initialAlignment;
        SetColour(initialMaterialColour);
    }

    IEnumerator UpdateRealTime()
    {
        while (true)
        {
            rend.material.SetFloat("_UnscaledTime", timeSinceStart);
            timeSinceStart += Time.unscaledDeltaTime;
            yield return new WaitForSecondsRealtime(0);
        }
    }
}
