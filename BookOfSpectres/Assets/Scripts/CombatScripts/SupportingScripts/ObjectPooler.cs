using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Audio;
using PlayerControlNamespace;
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
    [SerializeField]
    AudioMixer audioMixer;

    public bool isPaused = false;

    InkTypewriterText inkTypewriterText;

    string dialogueKnot = "";
    [SerializeField]
    string sceneName = "";

    public string DialogueKnot { get => dialogueKnot; set => dialogueKnot = value; }
    public string SceneName { get => sceneName; set => sceneName = value; }

    private void Awake()
    {
        LoadUI.UILoadEvent += UILoaded;

        waveNumber = 0;

        BGM = GameObject.Find("BGMHandler");
        


        Instance = this;


        

        LevelStart();

    }

    void UILoaded()
    {
        inkTypewriterText = GameObject.Find("DialogueCanvas").GetComponent<InkTypewriterText>();
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
            char[] _cloneRemover = { '(', 'C', 'l', 'o', 'n', 'e', ')' };
            _tempObj.name.TrimEnd(_cloneRemover);
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
            objectToSpawn.name = peekObjectToSpawn.name;
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
                obj.name = pool.prefab.name;
                allPooledObjects.Add(obj);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

        ///REDO LATER
        //StartWave();
        //StartCoroutine(WaveStart());

        //previousEnemyTypes.Clear();
    }

    public void StartWave()
    {
        StartCoroutine(WaveStart());
        /*foreach(GameObject enemy in eWaves[waveNumber].enemies)
        {
            
            GameObject _spawnedEnemy;
            _spawnedEnemy = SpawnFromPool(enemy.name, new Vector3(eWaves[waveNumber].enemyPosition[enemiesLeft].x, eWaves[waveNumber].enemyPosition[enemiesLeft].y,-1.4f), Quaternion.identity, transform);

            enemiesLeft++;
        }*/

    }

    IEnumerator WaveStart()
    {
        enemiesLeft += eWaves[waveNumber].enemies.Count;

        int _currentEnemy = 0;
        float _animationTime = 0.5f;

        List<GameObject> _spawnedEnemies = new List<GameObject>();
        foreach (GameObject enemy in eWaves[waveNumber].enemies)
        {
            GameObject _spawnedEnemy;
            _spawnedEnemy = SpawnFromPool(enemy.name, new Vector3(eWaves[waveNumber].enemyPosition[_currentEnemy].x, eWaves[waveNumber].enemyPosition[_currentEnemy].y, -1.4f), Quaternion.identity, transform);
            _spawnedEnemy.GetComponent<EnemyAI>().wait = true;
            _spawnedEnemies.Add(_spawnedEnemy);
            _currentEnemy++;     
            if (_spawnedEnemy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("StartCombat"))
            {
                _animationTime = _spawnedEnemy.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length;
                yield return new WaitForSecondsRealtime(_animationTime);
            }
            else
            {
                yield return new WaitForSecondsRealtime(Random.Range(0.3f,0.6f));
            }
            if(waveNumber > 0)
            {
                _spawnedEnemy.GetComponent<EnemyAI>().wait = false;
            }
        }
        //yield return new WaitForSecondsRealtime(_animationTime);

        if (waveNumber == 0)
        {
            CombatMenu.Instance.TweenMenu();
            PlayerScript.Instance.playerControl.Enable();
        }
        foreach(GameObject g in _spawnedEnemies)
        {
            g.GetComponent<EnemyAI>().wait = false;
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
        Time.timeScale = 0;
        SetLowpass(22000);

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
        Time.timeScale = 1;
        
        foreach (GameObject g in allPooledObjects)
        {
            if (g.activeSelf)
            {
                g.SendMessage("UnPaused", SendMessageOptions.RequireReceiver);
            }
        }
        SetLowpass(22000);

    }
    public void EnemyDefeated()
    {
        StartCoroutine("WaitTillDie");
    }

    public void SetLowpass(float value)
    {
        audioMixer.SetFloat("LowpassCutoff", value);
    }

    IEnumerator WaitTillDie()
    {
        yield return new WaitForSeconds(3);
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
                if (dialogueKnot == "")
                {
                    SceneManager.LoadScene(sceneName);
                }
                else
                {
                    inkTypewriterText.story.ChoosePathString(dialogueKnot);
                    inkTypewriterText.StartDialogue();
                }
            }
        }
    }
}
