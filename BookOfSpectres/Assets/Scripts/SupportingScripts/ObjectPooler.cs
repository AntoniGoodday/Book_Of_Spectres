using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    [SerializeField]
    List<GameObject> previousEnemyTypes = new List<GameObject>();

    public int waveNumber = 0;

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public List<GameObject> allPooledObjects = new List<GameObject>();

    public List<EnemyWave> eWaves = new List<EnemyWave>();
    public static ObjectPooler Instance;

    [SerializeField]
    public GameObject BGM;


    private void Awake()
    {
        waveNumber = 0;

        BGM = GameObject.Find("BGMHandler");

        Instance = this;

        LevelStart();

    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, Transform parent)

    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        IpooledObject pooledObj = objectToSpawn.GetComponent<IpooledObject>();
        if (objectToSpawn.activeSelf == true)
        {
            pooledObj.Deactivate();
            objectToSpawn.SetActive(false);
        }
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.transform.parent = parent;

        objectToSpawn.SetActive(true);


        //IpooledObject pooledObj = objectToSpawn.GetComponent<IpooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
    void LevelStart()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        /*bool canBeAdded = false;

        int waveSize = 0;

        foreach (GameObject enemy in eWaves[waveNumber].enemies)
        {
            previousEnemyTypes.Add(enemy);
        }
        previousEnemyTypes = previousEnemyTypes.Distinct().ToList();
        foreach (GameObject eType in previousEnemyTypes)
        {
            Pool p = new Pool();
            foreach (GameObject enemy in eWaves[waveNumber].enemies)
            {
                if (eType == enemy)
                {
                    waveSize++;
                }
            }

            p.prefab = eType;
            p.tag = eType.name + waveNumber;
            p.size = waveSize;
            Debug.Log(p.tag);
            pools.Add(p);
        }
        */


        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab,gameObject.transform);
                allPooledObjects.Add(obj);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
        //previousEnemyTypes.Clear();
    }
    public void SpawnWave()
    {
        //BOSS MUSIC
        if (eWaves[waveNumber].name == "BossWave")
        {

            //BGM.GetComponent<BGMHandler>().PlaySongWithIntro(2);
           
        }

        bool canBeAdded = false;

        int waveSize = 0;

        foreach (GameObject enemy in eWaves[waveNumber].enemies)
        {
            previousEnemyTypes.Add(enemy);


        }
        previousEnemyTypes = previousEnemyTypes.Distinct().ToList();
        foreach (GameObject eType in previousEnemyTypes)
        {
            Pool p = new Pool();
            foreach (GameObject enemy in eWaves[waveNumber].enemies)
            {
                if (eType.name == enemy.name)
                {
                    waveSize++;
                }
            }

            p.prefab = eType;
            p.tag = eType.name + waveNumber;
            p.size = waveSize;
            Debug.Log(p.tag);
            pools.Add(p);

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < p.size; i++)
            {
                GameObject obj = Instantiate(p.prefab);
                allPooledObjects.Add(obj);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            Debug.Log(p.tag);
            poolDictionary.Add(p.tag, objectPool);
        }
        previousEnemyTypes.Clear();
    }

    public void PauseAll()
    {
        foreach (GameObject g in allPooledObjects)
        {
            if (g.activeSelf)
            {
                g.SendMessage("Paused", SendMessageOptions.RequireReceiver);
            }
        }
    }
    public void UnPauseAll()
    {
        foreach (GameObject g in allPooledObjects)
        {
            if (g.activeSelf)
            {
                g.SendMessage("UnPaused", SendMessageOptions.RequireReceiver);
            }
        }
    }
}
