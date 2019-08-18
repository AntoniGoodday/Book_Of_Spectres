using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
public class TileClass : MonoBehaviour
{
    [SerializeField]
    public int tileID;
    [SerializeField]
    public TileAlignment tAlign = TileAlignment.Friendly;
    [SerializeField]
    TileAlignment initialAlignment;
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
    public Dictionary<TileNeighbourDirection, GameObject> tileNeighbours = new Dictionary<TileNeighbourDirection, GameObject>();
    public Renderer rend;
    public SpriteRenderer sRend;
    // Use this for initialization
    private void Awake()
    {
        //rend = gameObject.GetComponent<Renderer>();
    }
    void Start()
    {
        
    }
    public void SetColour(Color c, bool setup = false)
    {
        rend = gameObject.GetComponent<Renderer>();
        
        var tempMaterial = new Material(rend.sharedMaterial)
        {
            color = c
        };

        if (setup == true)
        {
            rend.material.SetColor("_BaseColor", tempMaterial.color);
        }
        else if(setup == false)
        {
            rend.material.SetColor("_BaseColor", tempMaterial.color);
        }
        //tempMaterial.SetFloat("_Glossiness", 0);
        //rend.material.SetColor("_BaseColor",tempMaterial.color);
        if (GetComponentInChildren<SpriteRenderer>() == true)
        {
            sRend = GetComponentInChildren<SpriteRenderer>();
            sRend.sharedMaterial = tempMaterial;
        }
        
        /*var tempSpriteMaterial = new Material(sRend.sharedMaterial)
        {
            color = c
        };*/
        //sRend.color = new Color(c.r,c.g,c.b,c.a);
        //rend.material.color = new Color(colourR, colourG, colourB, colourA); 
    }
    /*public void SetColour(float colourR, float colourG, float colourB, float colourA)
    {
        rend = gameObject.GetComponent<Renderer>();
        var tempMaterial = new Material(rend.sharedMaterial)
        {
            color = new Color(colourR, colourG, colourB, colourA)
        };
        rend.sharedMaterial = tempMaterial;
        //rend.material.color = new Color(colourR, colourG, colourB, colourA); 
    }*/


    //Make tile not occupied
    public void UnOccupy(Color tileColour)
    {
        occupied = false;
        rend.material.color = tileColour;
        transform.localPosition += new Vector3(0, Mathf.Lerp(0, 0.1f, 1), 0);
    }

    public void SetVariables()
    {
        column = (int)transform.position.x + 1;
        row = (int)transform.position.y + 1;
        initialAlignment = tAlign;
    }

    public void DebugCurrentTile()
    {
        foreach(KeyValuePair<TileNeighbourDirection, GameObject> d in tileNeighbours)
        {
            Debug.Log(d.Key);
            Debug.Log(d.Value);
        }
    }
}
