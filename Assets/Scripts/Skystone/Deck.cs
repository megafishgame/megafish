using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Deck : MonoBehaviour
{
    public SkystoneScriptable[] skystones = new SkystoneScriptable[5];
    public GameObject[] skystonesObject = new GameObject[5];
    public GameObject spawn;
    public GameObject holder;
    public float offset;
    public Vector3 scaleFactor;
    public Vector3 scaleInit;

    public int index;
    public int sizeList = 5;

    private void Start()
    {
        Vector3 position = spawn.transform.position;
        position.x -= offset * 2;
        for (int i = 0; i < 5; i++)
        {
            GameObject h = Instantiate(holder, position, spawn.transform.rotation) as GameObject;
            h.GetComponent<SkystoneGenerate>().skystone = skystones[i];
            skystonesObject[i] = h;
            h.transform.parent = gameObject.transform;
            position.x += offset;
        }
        scaleInit = skystonesObject[0].transform.localScale;
        scaleFactor = scaleInit * 1.2f;
        Scale(scaleFactor);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Scale(scaleInit);
            index -= 1;
            index %= sizeList;
            if (index < 0)
                index += sizeList;
            Scale(scaleFactor);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Scale(scaleInit);
            index += 1;
            index %= sizeList;
            Scale(scaleFactor);
        }
    }
    private void Scale(Vector3 value)
    {
        skystonesObject[index].transform.DOScale(value, 0.2f);
    }
    public void Play()
    {
        Destroy(skystonesObject[index]);
        if (index != sizeList - 1)
        {
            Move(index);
            Switch(index);
        }
        sizeList--;
        index = 0;
        transform.DOMove(transform.position + new Vector3(0.625f, 0, 0), 1);
        Scale(scaleFactor);
    }
    private void Move(int pos)
    {
        skystonesObject[pos + 1].transform.DOLocalMove(skystonesObject[pos].transform.localPosition, 1);
        if (pos+2 != sizeList)
            Move(pos + 1);
    }
    private void Switch(int pos)
    {
        (skystonesObject[pos], skystonesObject[pos + 1]) = (skystonesObject[pos + 1], skystonesObject[pos]);
        (skystones[pos], skystones[pos + 1]) = (skystones[pos + 1], skystones[pos]);
        if (pos + 2 != sizeList)
            Switch(pos + 1);
    }
}
