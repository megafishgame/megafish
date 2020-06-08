using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnKey : MonoBehaviour
{
    public string scene;
    public KeyCode key;
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Debug.Log("key down");
            StartCoroutine(Launch());
        }
    }
    IEnumerator Launch()
    {
        GetComponent<Animator>().SetBool("end", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(scene);
    }
}
