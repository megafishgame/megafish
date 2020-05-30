using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemieScriptCapacity : MonoBehaviour
{
    [Header("stats")]
    public float lifeMax;
    public float life;
    public float movingSize = 3;
    public float sensorSize = 10;

    [Header("type")]
    public EnumScriptName.ScriptEnemiesName Capacities;
    public EnemieEnum.gender Type;
    public EnemieEnum.movements MovementsMode;

    [Header("other")]
    public GameObject deathParticles;
    public float time = 5;
    private float initalSpeed;
    private float speedMultiplier = 2;
    private Vector3 movements;

    [Header("Components")]
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
        if (MovementsMode == EnemieEnum.movements.motionless)
            movements = new Vector3(9999, 9999);
    }
    private void Start()
    {
        life = lifeMax;
        NAA = GetComponent<NavMeshAgent>();
        initalSpeed = NAA.speed;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, movements) < 0.1f)
            Move();
    }
    
    public void Move()
    {
        if (MovementsMode == EnemieEnum.movements.pattern)
        {
            MovePattern();
        }
        else if (MovementsMode == EnemieEnum.movements.random_distance)
        {
            MoveRandomDistance();
        }
        else if (MovementsMode == EnemieEnum.movements.random_kamikaze)
        {
            MoveKamikaze();
        }
    }

    public void MovePattern()
    {

    }
    public void MoveKamikaze()
    {
        movements = Vector3.zero;
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
    public void MoveRandomDistance()
    {
        movements = Vector3.zero;
        if (Vector3.Distance(transform.position, player.transform.position) < sensorSize)
        {
            NAA.speed = speedMultiplier * initalSpeed;
            movements = transform.position + (transform.position - player.transform.position);
        }
        else
        {
            NAA.speed = initalSpeed;
            movements = Random.insideUnitSphere * movingSize;
        }
        NAA.SetDestination(movements);
    }


    public void Death()
    {
        Debug.Log($"Enemie number: {gameObject.GetInstanceID()} is dead");
        GameObject GO = Instantiate(deathParticles, transform.position, Quaternion.identity) as GameObject;
        Destroy(GO, time);
        Destroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
            Death();
        Debug.Log($"{gameObject} is taking damage!");
    }
}
