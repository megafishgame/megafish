﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickPosition : MonoBehaviour
{
    public Image image;
    public GameObject stone;
    public GameObject deck;
    public bool isBlue;
    private void Start()
    {
        image.color = isBlue ? Color.blue : Color.red;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            SkystoneScriptable skystone = deck.GetComponent<Deck>().skystones[deck.GetComponent<Deck>().index];
;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (hit.transform.parent != null && hit.transform.parent.gameObject == gameObject && hit.transform.gameObject.GetComponent<Container>().isEmpty)
                {

                    deck.GetComponent<Deck>().Play();
                    stone.GetComponent<SkystoneGenerate>().skystone = skystone;

                    Container cont = hit.transform.GetComponent<Container>();
                    cont.isEmpty = false;
                    cont.stone = stone;
                    cont.Generate();
                    cont.newEntry = true;
                    GenerateTable GT = cont.transform.parent.GetComponent<GenerateTable>();
                    int size = GT.size;

                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            if(GT.table[i, j].GetComponent<Container>().newEntry)
                            {
                                cont.newEntry = false;
                                cont.Reshape(j, i, size);
                                cont.Attack(j, i, size);
                            }
                        }
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            isBlue = !isBlue;
            image.color = isBlue ? Color.blue : Color.red;
        }
    }
}