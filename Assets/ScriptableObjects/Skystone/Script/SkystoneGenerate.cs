using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkystoneGenerate : MonoBehaviour
{
    public SkystoneScriptable skystone;
    public GameObject triangle;
    public GameObject[] positions = new GameObject[4];
    private Vector3 offsetRotation = new Vector3(0, 90, 0);
    private float offsetPosition = 0.15f;
    public GameObject particles;

    public Image[] icon = new Image[2];
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            icon[i].sprite = skystone.icon;
        }
        Vector3 triangleOrientation = new Vector3(90, 90, 0);

        int index = 0;

        foreach (int attackNumber in skystone.Attack)
        {
            bool even = false;
            bool evenSide = false;
            float offsetStart = 0;

            if (attackNumber % 2 == 0) even = true;
            if (index % 2 == 0) evenSide = true;

            if(!even) offsetStart = -(attackNumber / 2) * offsetPosition;
            else offsetStart = -(attackNumber / 2) * offsetPosition + offsetPosition/2;

            Vector3 offset = Vector3.zero;

            if(evenSide) offset.x = offsetStart;
            else offset.z = offsetStart;

            for (int i = 0; i < attackNumber; i++)
            {
                GameObject t = Instantiate(triangle, positions[index].transform.position + offset, Quaternion.Euler(triangleOrientation)) as GameObject;
                t.transform.parent = positions[index].transform;
                if (evenSide) offset.x += offsetPosition;
                else offset.z += offsetPosition;
            }
            triangleOrientation += offsetRotation;
            index++;
        }
    }
}
