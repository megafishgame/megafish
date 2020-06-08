using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkystoneIa : MonoBehaviour
{
    public SkystoneScriptable[] skystones = new SkystoneScriptable[5];
    private SkystoneTurn turn;
    public GameObject stone;
    public GameObject table;
    private int size = 3;
    private bool p;
    private void Start()
    {
        p = true;
        table = GameObject.FindGameObjectWithTag("Skystone");
        turn = GetComponentInParent<SkystoneTurn>();
    }

    private void Update()
    {
        if (!turn.yourTurn && p)
        {
            p = false;
            StartCoroutine(PlayTurn());
        }
    }

    IEnumerator PlayTurn()
    {
        yield return new WaitForSeconds(2f);
        int imax = 0;
        int jmax = 0;
        int indexmax = 0;
        int scoremax = -100;

        for (int index = 0; index < 5; index++)
        {
            if (skystones[index] != null)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        int score = WinPoint(i, j, skystones[index]);
                        if (score > scoremax) // is empty ?
                        {
                            imax = i;
                            jmax = j;
                            indexmax = index;
                            scoremax = score;
                        }
                    }
                }
            }
        }
        if (scoremax == 0)
        {
            while (true)
            {
                int i = Random.Range(0, 3);
                int j = Random.Range(0, 3);
                Container tablestone = table.GetComponent<GenerateTable>().table[i, j].GetComponent<Container>();
                if (tablestone.isEmpty)
                {
                    imax = i;
                    jmax = j;
                    break;
                }
            }
            while (true)
            {
                int index = Random.Range(0, 5);
                if (skystones[index] != null)
                    break;
            }
        }
        stone.GetComponent<SkystoneGenerate>().skystone = skystones[indexmax];

        Container cont = table.GetComponent<GenerateTable>().table[imax, jmax].gameObject.GetComponent<Container>();

        cont.isEmpty = false;
        cont.stone = stone;
        cont.Generate();
        cont.newEntry = true;
        GenerateTable GT = cont.transform.parent.GetComponent<GenerateTable>();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (GT.table[i, j].GetComponent<Container>().newEntry)
                {
                    cont.newEntry = false;
                    cont.Reshape(j, i, size);
                    cont.skystone.GetComponent<SkystoneBehaviour>().ChangeColorFast();
                    cont.Attack(j, i, size);
                }
            }
        }
        if(scoremax != 0)
            yield return new WaitForSeconds(2f);
        skystones[indexmax] = null;
        turn.yourTurn = true;
        p = true;
    }
    int WinPoint(int i, int j, SkystoneScriptable skystone)
    {
        Container tablestone = table.GetComponent<GenerateTable>().table[i, j].GetComponent<Container>();

        if (tablestone.isEmpty)
            return Attack(i, j, skystone);
        else
            return -10;
    }

    int Attack(int i, int j, SkystoneScriptable skystone)
    {
        int score = 0;
        score += AttackPos(i - 1, j, 0, skystone);
        score += AttackPos(i + 1, j, 2, skystone);

        score += AttackPos(i, j - 1, 3, skystone);
        score += AttackPos(i, j + 1, 1, skystone);


        return score;
    }
    int AttackPos(int i, int j,  int side, SkystoneScriptable me)
    {
        if (j >= 0 && i >= 0 && j < size && i < size)
        {
            Container tablestone = table.GetComponent<GenerateTable>().table[i, j].GetComponent<Container>();

            if (!tablestone.isEmpty)
            {

                SkystoneGenerate enemieSkystone = tablestone.skystone.GetComponent<SkystoneGenerate>();

                int attackM = me.Attack[side];
                int attackE = enemieSkystone.positions[(side + 2) % 4].transform.childCount;


                if (attackE < attackM)
                {
                    if (enemieSkystone.gameObject.GetComponent<SkystoneBehaviour>().isBlue == false) //
                        return 1;
                }
            }
        }
        return 0;
    }
}
