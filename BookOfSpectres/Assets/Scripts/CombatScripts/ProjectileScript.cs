using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
using Cinemachine.Editor;
using Cinemachine;
public class ProjectileScript : MonoBehaviour, IpooledObject
{
    ObjectPooler objectPooler;

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
    public int damageDealt;
    int initialDamageDealt;
    [SerializeField]
    float amplitudeModifier = 0.05f;
    [SerializeField]
    float expirationTime;
    [SerializeField]
    GameObject bulletTrail;
    TrailRenderer tr;
    float trailTime;
    ParticleSystem pSystem;
    BoxCollider boxCollider;
    float pauseTime;
    float resumeTime;
    

    private GameObject previousTile;
    private TileClass previousTileClass;
    private string tileTag = "Tile";
    private Ray ray;
    private RaycastHit hit;
    public bool isPaused = false;


    // Start is called before the first frame update
    void Awake()
    {
        objectPooler = ObjectPooler.Instance;

        //defaultParent = gameObject.transform.parent;
        defaultParent = objectPooler.transform;
        tr = GetComponentInChildren<TrailRenderer>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
       
        initialDamageDealt = damageDealt;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.tag != owner && other.transform.tag != transform.tag)
        {
            tr.autodestruct = true;
            tr.gameObject.transform.parent = null;
            if (other.gameObject.GetComponent<EntityStatus>())
            {
                
                other.gameObject.GetComponent<EntityStatus>().DealDamage(damageDealt, transform.position.z,amplitudeModifier);
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
                GameObject _hitTile = hit.transform.gameObject;
                if (previousTile == null)
                {
                    previousTile = hit.transform.gameObject;
                    previousTileClass = previousTile.GetComponent<TileClass>();
                    previousTileClass.SetColour(Color.yellow, false, true);
                    tr.sortingOrder = -(int)previousTileClass.gridLocation.y + 5;
                    
                }
                else if(previousTile != _hitTile)
                {

                    previousTile.GetComponent<TileClass>().SetColour(previousTileClass.initialMaterialColour);
                    previousTile = _hitTile;
                    previousTileClass = previousTile.GetComponent<TileClass>();
                    previousTileClass.SetColour(Color.yellow,false, true);
                    tr.sortingOrder = -(int)previousTileClass.gridLocation.y + 5;
                }

            }
            else
            {
                if (previousTile != null)
                {
                    previousTileClass = previousTile.GetComponent<TileClass>();
                    previousTileClass.SetColour(previousTileClass.initialMaterialColour);
                    previousTile = null;
                    previousTileClass = null;
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
            pauseTime = Time.time;
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
            resumeTime = Time.time;
            tr.time = (resumeTime - pauseTime) + trailTime;
            Invoke("SetTrailTime", trailTime);
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

    void SetTrailTime()
    {
        tr.time = trailTime;
    }
}
