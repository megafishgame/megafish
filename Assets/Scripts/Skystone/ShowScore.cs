using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public string mapToLoad = "FirstMap";
    public Text red;
    public Text blue;
    private bool first = true;
    private float blueScore;
    private float redScore;
    private void FixedUpdate()
    {
        GameObject[,] table = GetComponentInParent<GenerateTable>().table;
        blueScore = 0;
        redScore = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!table[i, j].GetComponent<Container>().isEmpty)
                {
                    if (table[i, j].GetComponent<Container>().skystone.GetComponent<SkystoneBehaviour>().isBlue)
                        blueScore++;
                    else 
                        redScore++;
                }
            }
        }
        red.text = redScore.ToString();
        blue.text = blueScore.ToString();
        if(blueScore + redScore == 9 && first)
        {
            first = false;
            StartCoroutine(Launch());
        }
    }
    IEnumerator Launch()
    {
        yield return new WaitForSeconds(2);
        GameObject.FindGameObjectWithTag("RESULT").GetComponent<Result>().hasWinLast = redScore > blueScore ? true : false;
        StartCoroutine(GameObject.FindGameObjectWithTag("SceneManager").GetComponentInChildren<LoadScenePannel>().Launch(mapToLoad));
        this.enabled = false;
    }
}
