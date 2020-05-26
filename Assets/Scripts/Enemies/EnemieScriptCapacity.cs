using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemieScriptCapacity : MonoBehaviour
{
    public float movingSize = 3;
    public float sensorSize = 10;

    private float initalSpeed;
    private float speedMultiplier = 2;

    private NavMeshAgent NAA;
    private GameObject player;
    public abstract void Capacity1();
    public abstract void Capacity2();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, movingSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sensorSize);
    }
    private void Start()
    {
        NAA = GetComponent<NavMeshAgent>();
        initalSpeed = NAA.speed;
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(Move());
    }
    public void Moving()
    {
        Vector3 movements = Vector3.zero;
        if (Vector3.Distance(transform.position, player.transform.position) < sensorSize)
        {
            NAA.speed = speedMultiplier * initalSpeed;
            movements = player.transform.position + (Random.insideUnitSphere * movingSize) / 2;
        }
        else
        {
            NAA.speed = initalSpeed;
            movements = Random.insideUnitSphere * movingSize;
        }
        NAA.SetDestination(movements);
    }



    IEnumerator Move()
    {
        Moving();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Move());
    }
}
