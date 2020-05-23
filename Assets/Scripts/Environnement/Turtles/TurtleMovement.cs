using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.EditorTools;

public class TurtleMovement : MonoBehaviour
{
    public bool isIn;
    public Vector3 lastAngle;

    private bool CR_running = false;

    public float timeMax = 0.5f;
    public float movingTime;
    public float step = 1;

    public bool Xaxis;
    public bool both;

    public GameObject arrowPosition;
    public GameObject arrow;

    public float sphereSize = 0.5f;

    public GameObject checkSphere;
    public GameObject floorSphere;

    public LayerMask groundMask;

    private void Awake()
    {

        Quaternion rotation = Xaxis ? new Quaternion(0, 1, 1, 0) : new Quaternion(0.5f, 0.5f, -0.5f, 0.5f);
        GameObject a = Instantiate(arrow, arrowPosition.transform.position, rotation) as GameObject;
        a.transform.parent = gameObject.transform;
        if (both)
        {
            rotation = !Xaxis ? new Quaternion(0, 1, 1, 0) : new Quaternion(0.5f, 0.5f, -0.5f, 0.5f);
            a = Instantiate(arrow, arrowPosition.transform.position, rotation) as GameObject;
            a.transform.parent = gameObject.transform;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lastAngle = transform.position - other.transform.position;
            for (int i = 0; i < 3; i++)
            {
                if (lastAngle[i] <= 1f && lastAngle[i] >= -1f)
                    lastAngle[i] = 0;
                else lastAngle[i] = ((int)lastAngle[i]);
            }
            lastAngle.y = 0;
            if (!both)
            {
                if (Xaxis)
                    lastAngle.z = 0;
                else
                    lastAngle.x = 0;
            }
            else
            {
                if (lastAngle.x != 0 && lastAngle.z != 0)
                    lastAngle = Vector3.zero;
            }

            isIn = true;
            StartCoroutine(Timer());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isIn = false;
        }
    }

    IEnumerator Timer()
    {
        CR_running = true;
        yield return new WaitForSeconds(timeMax);
        CR_running = false;
        if (isIn)
            moving();

    }
    private void moving()
    {
        Vector3 vector = transform.position + lastAngle * step;
        checkSphere.transform.position = vector;
        if (!Physics.CheckSphere(checkSphere.transform.position, sphereSize))
        {
            gameObject.transform.DOLocalMove(vector, movingTime);
            StartCoroutine(falling());
        }
            
    }

    IEnumerator falling()
    {
        yield return new WaitForSeconds(movingTime);
        if (!Physics.CheckSphere(floorSphere.transform.position, sphereSize, groundMask))
        {
            gameObject.transform.DOLocalMove(transform.position - Vector3.up * step, movingTime);
            StartCoroutine(falling());
        }
        
    }

    private void OnDrawGizmosSelected()
    {
     Gizmos.color = Color.red;
     Gizmos.DrawSphere(transform.position + lastAngle * step, sphereSize); 
     Gizmos.color = Color.blue;
     Gizmos.DrawSphere(floorSphere.transform.position, sphereSize);
    }
}
