using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemieScriptCapacity : MonoBehaviour
{
    [Header("stats")]
    public float lifeMax;
    public float life;
    public float movingSize = 3;
    public float sensorSize = 4;
    public float range;
    public int damage;
    public float attackCD;

    [Header("type")]
    public EnumScriptName.ScriptEnemiesName Capacities;
    public EnemieEnum.gender Type;
    public EnemieEnum.movements MovementsMode;

    [Header("other")]
    public GameObject deathParticles;
    private float time = 5; // destroy

    private float attackWaitTime;

    public int id;
    private int index;
    public List<Transform> waypointPath = new List<Transform>();

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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, movingSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sensorSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    private void Start()
    {
        life = lifeMax;
        NAA = GetComponent<NavMeshAgent>();
        initalSpeed = NAA.speed;
        player = GameObject.FindGameObjectWithTag("Player");


        if (MovementsMode == EnemieEnum.movements.motionless)
            movements = new Vector3(9999, 9999);
        else
        {
            if (MovementsMode == EnemieEnum.movements.pattern)
            {
                GameObject[] waypoints = GameObject.FindGameObjectsWithTag("waypointID");
                foreach (var waypoint in waypoints)
                {
                    if (waypoint.GetComponent<WaypointID>().id == id)
                    {
                        waypointPath = waypoint.GetComponent<WaypointID>().path;
                        break;
                    }
                }
                if (waypointPath.Count == 0)
                    Debug.Log("<color=blue> NO WAYPOINTPATH </color>");
            }

        Move();
        }
    }
    private void Update()
    {
        movements.y = transform.position.y;
        if (Vector3.Distance(transform.position, movements) < 0.1f)
            Move();
        else if (MovementsMode == EnemieEnum.movements.random_distance && Vector3.Distance(transform.position, player.transform.position) < sensorSize)
            MoveDistance();

        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            NAA.updateRotation = false;
            NAA.gameObject.transform.LookAt(player.transform);
        }

            Timer();
    }
    public void Timer()
    {
        if (attackWaitTime < 0)
        {
            attackWaitTime = attackCD;
            Attack();
        }
        else attackWaitTime -= Time.deltaTime;
    }
    public void Move()
    {
        NAA.updateRotation = true;
        if (MovementsMode == EnemieEnum.movements.pattern)
        {
            MovePattern();
        }
        else if (MovementsMode == EnemieEnum.movements.random_distance)
        {
            MoveDistance();
        }
        else if (MovementsMode == EnemieEnum.movements.random_kamikaze)
        {
            MoveKamikaze();
        }
    }
    public void Attack() 
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            if (Type == EnemieEnum.gender.archer)
            {
                AttackArcher();
            }
            else if (Type == EnemieEnum.gender.kamikaze)
            {
                AttackExplode();
            }
            else if (Type == EnemieEnum.gender.warrior)
            {
                AttackOnHit();
            }
            if(MovementsMode != EnemieEnum.movements.motionless)
                Move();
        }
    }
    public void MovePattern()
    {
        index += 1;
        index %= waypointPath.Count;
        movements = waypointPath[index].position;
        NAA.SetDestination(movements);
    }
    public void MoveKamikaze()
    {
        movements = Vector3.zero;
        if (Vector3.Distance(transform.position, player.transform.position) < sensorSize)
        {
            NAA.speed = speedMultiplier * initalSpeed;
            movements = player.transform.position;
        }
        else
        {
            NAA.speed = initalSpeed;
            movements = Random.insideUnitSphere * movingSize;
        }
        NAA.SetDestination(movements);
    }
    public void MoveDistance()
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
    public void AttackArcher()
    {
        GameObject Bullet = GameObject.FindGameObjectWithTag("Respawn").GetComponent<ObjectToLoad>().objects[0];
        Vector3 force = transform.forward;
        force *= 1000;
        GameObject ArrowCreated = Instantiate(Bullet, transform.position + Vector3.up + transform.forward, Quaternion.identity) as GameObject;

        //dmg ?

        ArrowCreated.GetComponent<Rigidbody>().AddForce(force);
    }
    public void AttackExplode()
    {
        GameObject Explosion = GameObject.FindGameObjectWithTag("Respawn").GetComponent<ObjectToLoad>().objects[0];
        Instantiate(Explosion, transform.position + Vector3.up + transform.forward, Quaternion.identity);
        AttackOnHit();
        Death();
    }
    public void AttackOnHit()
    {
        attackWaitTime = 0.5f;
        Collider[] sphere = Physics.OverlapSphere(transform.position, range);
        foreach (var GO in sphere)
        {
            if (GO.CompareTag("Player"))
                player.GetComponent<ActionPlayer>().TakeDamage(damage);
        }
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
        Debug.Log($"{gameObject.name} is taking damage!");
    }
}
