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
    public bool occupied = false;
    [SerializeField]
    public Color initialMaterialColour;
    [SerializeField]
    public int column;
    [SerializeField]
    public int row;
    [SerializeField]
    public Vector2 gridLocation;
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
        //rend = gameObject.GetComponent<Renderer>();
    }
    void Start()
    {
        
    }
    public void SetColour(Color c, bool setup = false, bool overrideColour = false)
    {
        rend = gameObject.GetComponent<Renderer>();
        
        var tempMaterial = new Material(rend.sharedMaterial)
        {
            color = c
        };

        if (setup == true)
        {
            
            rend.material.SetColor("_BaseColor", initialMaterialColour);
        }
        else if(setup == false)
        {

            if(c == initialMaterialColour)
            {
                rend.material.SetColor("_BaseColor", initialMaterialColour);
                UnOccupy();
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
            sRend = GetComponentInChildren<SpriteRenderer>();
            sRend.sharedMaterial = tempMaterial;
        }
        
       
    }
   
    

    public void Occupy()
    {

        transform.localPosition = initialPosition + new Vector3(0, Mathf.Lerp(0, 0.1f, 1), 0);
    }

    //Make tile not occupied
    public void UnOccupy()
    {
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
        foreach(KeyValuePair<Direction, GameObject> d in tileNeighbours)
        {
            Debug.Log(d.Key);
            Debug.Log(d.Value);
        }
    }

    public void ChangeAlignment(Color c, TileAlignment _t)
    {
        StopCoroutine("ReturnToInitialAlignment");
        SetColour(c,false);
        tileAlignment = _t;
        StartCoroutine("ReturnToInitialAlignment");
    }

    public void CheckEffect()
    {
        StopCoroutine("ReturnToInitialEffect");
        if (tileEffect == TileEffect.Broken)
        {
            rend.enabled = false;
            
        }
        StartCoroutine("ReturnToInitialEffect");
    }

    

    IEnumerator ReturnToInitialEffect()
    {

        yield return new WaitForSeconds(10);
        if(rend.enabled == false)
        {
            rend.enabled = true;
        }
        tileEffect = initialTileEffect;
        
    }

    IEnumerator ReturnToInitialAlignment()
    {

        yield return new WaitForSeconds(10);

        tileAlignment = initialAlignment;
        SetColour(initialMaterialColour);
    }
}
