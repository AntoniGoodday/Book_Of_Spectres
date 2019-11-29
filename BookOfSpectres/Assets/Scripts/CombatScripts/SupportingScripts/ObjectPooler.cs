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
    List<GameObject> enemyTypes = new List<GameObject>();

    public int waveNumber = 0;

    public int enemiesLeft;


    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public List<GameObject> allPooledObjects = new List<GameObject>();

    public List<EnemyWave> eWaves = new List<EnemyWave>();

    

    public static ObjectPooler Instance;

    [SerializeField]
    public GameObject BGM;

    public bool isPaused = false;

    private void Awake()
    {
        waveNumber = 0;

        BGM = GameObject.Find("BGMHandler");

        Instance = this;

        
        LevelStart();

    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, Transform parent, GameObject _prefab = null)

    {
        if (!poolDictionary.ContainsKey(tag))
        {

            Pool _tempPool = new Pool();
            _tempPool.tag = tag;
            _tempPool.prefab = _prefab;
            _tempPool.size = 1;
            pools.Add(_tempPool);
            GameObject _tempObj = Instantiate(_tempPool.prefab);
            allPooledObjects.Add(_tempObj);
            Queue<GameObject> _objQueue = new Queue<GameObject>();
            _objQueue.Enqueue(_tempObj);
            poolDictionary.Add(tag, _objQueue);
            _tempObj.SetActive(false);
            //return null;
        }


        //GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        GameObject peekObjectToSpawn = poolDictionary[tag].Peek();


        GameObject objectToSpawn = null;

        IpooledObject pooledObj = null;
        if (peekObjectToSpawn.activeSelf == true)
        {
            objectToSpawn = Instantiate(peekObjectToSpawn.gameObject);
            allPooledObjects.Add(objectToSpawn);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            objectToSpawn.transform.parent = parent;
            pooledObj = objectToSpawn.GetComponent<IpooledObject>();

            

            objectToSpawn.SetActive(false);

            //objectPool.Enqueue(objectToSpawn);
            //pooledObj.Deactivate();
            //objectToSpawn.SetActive(false);
        }
        else if(peekObjectToSpawn.activeSelf == false)
        {
            objectToSpawn = poolDictionary[tag].Dequeue();
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            objectToSpawn.transform.SetParent(parent);
            pooledObj = objectToSpawn.GetComponent<IpooledObject>();
            
        }

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

        bool canBeAdded = false;

        for (int i = 0; i < eWaves.Count; i++)
        {


            foreach (GameObject enemy in eWaves[i].enemies)
            {
                enemyTypes.Add(enemy);
            }
        }
        enemyTypes = enemyTypes.Distinct().ToList();


        foreach(GameObject eType in enemyTypes)
        {
            int _typeCount = 0;
            Pool p = new Pool();
            for (int i = 0; i < eWaves.Count; i++)
            {
                foreach (GameObject enemy in eWaves[i].enemies)
                {
                    if(enemy == eType)
                    {
                        _typeCount++;

                    }
                }
            }
            p.prefab = eType;
            p.tag = eType.name;
            p.size = _typeCount; 
            pools.Add(p);
        }


            /*foreach (GameObject enemy in eWaves[i].enemies)
            {
                previousEnemyTypes.Add(enemy);
            }
            previousEnemyTypes = previousEnemyTypes.Distinct().ToList()
            foreach (GameObject eType in previousEnemyTypes)
            {
                
                foreach (GameObject enemy in eWaves[i].enemies)
                {
                    if (eType == enemy)
                    {
                        waveSize++;
                    }
                }

                p.prefab = eType;
                p.tag = eType.name + i;
                p.size = waveSize;

                pools.Add(p);
            }*/



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

        StartWave();

        //previousEnemyTypes.Clear();
    }

    public void StartWave()
    {

        foreach(GameObject enemy in eWaves[waveNumber].enemies)
        {
            
            GameObject _spawnedEnemy;
            _spawnedEnemy = SpawnFromPool(enemy.name, new Vector3(eWaves[waveNumber].enemyPosition[enemiesLeft].x, eWaves[waveNumber].enemyPosition[enemiesLeft].y,-1.4f), Quaternion.identity, transform);

            enemiesLeft++;
        }

    }

    /*public void SpawnWave()
    {
        
        bool canBeAdded = false;

        int waveSize = 0;

        foreach (GameObject enemy in eWaves[waveNumber].enemies)
        {
            enemyTypes.Add(enemy);
        }
        enemyTypes = enemyTypes.Distinct().ToList();
        foreach (GameObject eType in enemyTypes)
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
            p.size = waveSize;;
            pools.Add(p);

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < p.size; i++)
            {
                GameObject obj = Instantiate(p.prefab);
                obj.name = p.tag;
                allPooledObjects.Add(obj);

                obj.SetActive(true);

                objectPool.Enqueue(obj);
            }
            
            poolDictionary.Add(p.tag, objectPool);
        }
        enemyTypes.Clear();
    }*/

    public void PauseAll()
    {
        isPaused = true;
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
        isPaused = false;
        foreach (GameObject g in allPooledObjects)
        {
            if (g.activeSelf)
            {
                g.SendMessage("UnPaused", SendMessageOptions.RequireReceiver);
            }
        }


    }
    public void EnemyDefeated()
    {
        StartCoroutine("WaitTillDie");
    }
    IEnumerator WaitTillDie()
    {
        yield return new WaitForSeconds(0);
        enemiesLeft--;
        if (enemiesLeft <= 0)
        {
            waveNumber++;
            if (waveNumber < eWaves.Count)
            {
                StartWave();
            }
            else
            {
                Application.LoadLevel(0);
            }
        }
    }
}
