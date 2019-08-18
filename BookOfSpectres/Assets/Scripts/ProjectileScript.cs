using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
public class ProjectileScript : MonoBehaviour, IpooledObject
{
    public string owner;
    [SerializeField]
    BulletAlignement bAlign;
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    public float speed;
    [SerializeField]
    Transform defaultParent;
    [SerializeField]
    int damageDealt;
    [SerializeField]
    float expirationTime;
    [SerializeField]
    GameObject bulletTrail;
    TrailRenderer tr;
    float trailTime;
    ParticleSystem pSystem;
    BoxCollider boxCollider;

    private GameObject previousTile;
    private string tileTag = "Tile";
    private Ray ray;
    private RaycastHit hit;
    public bool isPaused = false;
    

    // Start is called before the first frame update
    void Awake()
    {
        defaultParent = gameObject.transform.parent;
        tr = GetComponentInChildren<TrailRenderer>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.tag != owner && other.transform.tag != transform.tag)
        {
            tr.autodestruct = true;
            tr.gameObject.transform.parent = null;
            if (other.gameObject.GetComponent<EntityStatus>())
            {
                other.gameObject.GetComponent<EntityStatus>().DealDamage(damageDealt, transform.position.z);
            }
            if (previousTile != null)
            {
                previousTile.GetComponent<TileClass>().SetColour(previousTile.GetComponent<TileClass>().initialMaterialColour);
                previousTile = null;
            }
            StopAllCoroutines();
            
            gameObject.SetActive(false);
        }
        
    }

    

    void Start()
    {
        
        
    }

    

    public void OnObjectSpawn()
    {
        if(GetComponentInChildren<TrailRenderer>() == false)
        {
            Instantiate(bulletTrail, transform);
            tr = GetComponentInChildren<TrailRenderer>();
        }

        trailTime = tr.time;

        if (GetComponent<ParticleSystem>())
        {
            pSystem = GetComponent<ParticleSystem>();
        }
        
        boxCollider.enabled = true;
        rb.velocity = new Vector3(speed, 0, 0);
        owner = transform.parent.tag;
        gameObject.transform.parent = defaultParent;
        StartCoroutine("BulletExpiration");
        previousTile = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused == false )
        {
            
                rb.velocity = new Vector3(speed, 0, 0);
                ray = new Ray(transform.position, transform.forward);
                if (Physics.Raycast(ray, out hit, 10f))
                {
                    Debug.DrawRay(transform.position, -transform.up);
                    if (previousTile == null)
                    {
                        previousTile = hit.transform.gameObject;
                        previousTile.GetComponent<TileClass>().SetColour(Color.yellow);
                    }
                    else
                    {
                        previousTile.GetComponent<TileClass>().SetColour(previousTile.GetComponent<TileClass>().initialMaterialColour);
                        previousTile = hit.transform.gameObject;
                        previousTile.GetComponent<TileClass>().SetColour(Color.yellow);
                    }

                }
                else
                {
                    if (previousTile != null)
                    {
                        previousTile.GetComponent<TileClass>().SetColour(previousTile.GetComponent<TileClass>().initialMaterialColour);
                        previousTile = null;
                    }
                }
            

        }
    }

    IEnumerator BulletExpiration()
    {
        
        float _timer = 0f;
        while(_timer < expirationTime)
        {
            if (isPaused == false)
            {
                _timer += Time.deltaTime;
            }
            yield return null;
        }
        tr.Clear();
        gameObject.SetActive(false);
        StopAllCoroutines();
        yield return new WaitForSeconds(0);
    }

    /*IEnumerator HitStopTrail()
    {
        float _timer = 0f;
        while (_timer < expirationTime)
        {
            if (isPaused == false)
            {
                _timer += Time.deltaTime;
            }
            yield return null;
        }
        tr.Clear();
        gameObject.SetActive(false);
        yield return new WaitForSeconds(0);
    }*/




    public void Deactivate()
    {
        tr.Clear();
        gameObject.SetActive(false);
    }

    public void Paused()
    {
        if (gameObject.activeSelf == true)
        {
            
            isPaused = true;
            rb.velocity = new Vector3(0, 0, 0);

            
            tr.time = Mathf.Infinity;
            //tr.emitting = false;
            //rb.isKinematic = true;
            
            if (pSystem != null)
            {
                pSystem.Pause();
            }
        }
    }
    public void UnPaused()
    {
        if (gameObject.activeSelf == true)
        {
            tr.time = trailTime;
            rb.velocity = new Vector3(speed, 0, 0);
            isPaused = false;
            
            //tr.emitting = true;
            //rb.isKinematic = false;
            
            if (pSystem != null)
            {
                pSystem.Play();
            }
            
        }
    }
}
