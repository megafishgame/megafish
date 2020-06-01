using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class SkystoneBehaviour : MonoBehaviour
{
    public bool isBlue;

    public GameObject[] positions;
    private GameObject table;
    private float time = 0.05f;
    private bool doAttack = false;

    private int iRef;
    private int jRef;
    private int sizeRef;
    private float shouldInstantiateParticles;

    public void Start()
    {
        table = GameObject.FindGameObjectWithTag("Skystone");
        GameObject par = Instantiate(GetComponent<SkystoneGenerate>().particles, transform.position, Quaternion.identity) as GameObject;
        Destroy(par, 1);
    }
    public void Reshape(int i, int j, int size)
    {
        positions = GetComponent<SkystoneGenerate>().positions;

        if (j == 0) // left
        {
            Destroy(positions[3]);
        }
        if (j == size - 1) // right
        {
            Destroy(positions[1]);
        }
        if (i == 0) //up
        {
            Destroy(positions[0]);
        }
        if (i == size - 1) // down
        {
            Destroy(positions[2]);
        }
    }
    private void Update()
    {
        if (doAttack)
        {
            Timer();
            if (time < 0)
            {
                doAttack = false;
                Attack(iRef, jRef, sizeRef);
            }
        }
        if(shouldInstantiateParticles == 1)
        {
            GameObject par = Instantiate(GetComponent<SkystoneGenerate>().particles, transform.position, Quaternion.identity) as GameObject;
            Destroy(par, 1);
            shouldInstantiateParticles = 0;
        }
    }
    public void Timer()
    {
        time -= Time.deltaTime;
    }
    public void AttackTimer(int i, int j, int size)
    {
        doAttack = true;
        iRef = i;
        jRef = j;
        sizeRef = size;
    }
    private void Attack(int i, int j, int size)
    {
        AttackPos(i - 1, j, size, 0);
        AttackPos(i + 1, j, size, 2);

        AttackPos(i, j - 1, size, 3);
        AttackPos(i, j + 1, size, 1);
        Debug.Log("Attack ended");
    }
    void AttackPos(int i, int j, int size, int side)
    {
        if(j >= 0 && i >= 0 && j < size && i < size)
        {
            Container tablestone = table.GetComponent<GenerateTable>().table[i, j].GetComponent<Container>();

            if (!tablestone.isEmpty)
            {

                SkystoneGenerate enemieSkystone = tablestone.skystone.GetComponent<SkystoneGenerate>();

                int attackM = positions[side].transform.childCount;
                int attackE = enemieSkystone.positions[(side + 2) % 4].transform.childCount;

                Debug.Log($"Attack : {positions[side].name} and you have {attackM}, defense has {attackE}");

                if (attackE < attackM)
                {
                    if(enemieSkystone.gameObject.GetComponent<SkystoneBehaviour>().isBlue != isBlue)
                        enemieSkystone.gameObject.GetComponent<SkystoneBehaviour>().ChangeColor();
                }
            }
        }
    }
    public void ChangeColor()
    {
        isBlue = !isBlue;
        DOTween.Play(ChangeColorSequence());
        Rotate();
    }
    public void ChangeColorFast()
    {
        isBlue = !isBlue;
        RotateFast();
    }
    public void Rotate()
    {
        int factor = 1;
        for (int i = 1; i < 4; i += 2)
        {
            if (positions[i] != null)
            {
                positions[i].transform.position -= new Vector3(0.8f, 0, 0) * factor;
                positions[i].transform.rotation = Quaternion.Euler(positions[i].transform.rotation.eulerAngles + new Vector3(0, 180, 0));
            }
            factor = -1;
        }
    }
    public void RotateFast()
    {
        Rotate();
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, 180));
    }

    Sequence ChangeColorSequence()
    {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOMoveY(transform.position.y + 1, 0.5f));
        s.Append(transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, 180), 1f));
        s.Append(transform.DOMoveY(transform.position.y, 0.2f));
        s.Join(DOTween.To(() => shouldInstantiateParticles, x => shouldInstantiateParticles = x, 1, 0.15f));
        return s;
    }
}
