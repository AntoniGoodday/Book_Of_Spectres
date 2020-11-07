using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevCommands : MonoBehaviour
{
    bool devCommands = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.BackQuote))
        {
            devCommands = !devCommands;
            Debug.Log("Dev commands status: " + devCommands.ToString());
        }
        if (devCommands)
        {
            if (Input.GetKeyUp(KeyCode.F1))
            {
                GameObject _pStats = GameObject.Find("PlayerStats");
                _pStats.GetComponent<SaveLoadGamestate>().SaveGameState();
            }

            if (Input.GetKeyUp(KeyCode.F2))
            {
                GameObject _pStats = GameObject.Find("PlayerStats");
                _pStats.GetComponent<SaveLoadGamestate>().LoadGameState(); ;
            }

            if (Input.GetKeyUp(KeyCode.F3))
            {
                PlayerScript.Instance.status.Die();
            }
        }

    }
}
