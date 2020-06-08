using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public int id;
    public string scene;
    public float radius;
    public bool hasPlayer = false;
    public KeyCode key;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Start()
    {
        GameObject[] pannels = GameObject.FindGameObjectsWithTag("LastPannel");
        if (pannels.Length != 1)
        {
            foreach (var pannel in pannels)
            {
                if (pannel.GetComponent<LastPanel>().lastPanel == 0)
                {
                    Destroy(pannel);
                }
            }
        }
        GameObject result = GameObject.FindGameObjectWithTag("RESULT");
        if (result != null)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.FindGameObjectWithTag("LastPannel").GetComponent<LastPanel>().position;
            if (result.GetComponent<Result>().hasWinLast && 
                GameObject.FindGameObjectWithTag("LastPannel").GetComponent<LastPanel>().lastPanel == id)
            {
                Debug.Log("win");
                Destroy(result);
                GetComponent<ActionEnd>().action = true;
                Destroy(this);
            }

            Debug.Log("lose");
        } 
    }

    private void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius);
        bool checkPlayer = false;
        foreach (var col in cols)
        {
            if (col.CompareTag("Player"))
            {
                checkPlayer = true;
                break;
            }
        }
        hasPlayer = checkPlayer;
        if(checkPlayer && Input.GetKeyDown(key))
        {
            GameObject.FindGameObjectWithTag("LastPannel").GetComponent<LastPanel>().lastPanel = id;
            StartCoroutine(GameObject.FindGameObjectWithTag("SceneManager").GetComponentInChildren<LoadScenePannel>().Launch(scene));
            GameObject.FindGameObjectWithTag("LastPannel").GetComponent<LastPanel>().position = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }
}
