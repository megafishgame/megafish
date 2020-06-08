using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenePannel : MonoBehaviour
{
    public IEnumerator Launch(string scene)
    {
        GetComponent<Animator>().SetBool("end", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(scene);
    }
}
