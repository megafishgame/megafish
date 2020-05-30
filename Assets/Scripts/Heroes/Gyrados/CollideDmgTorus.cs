using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideDmgTorus : MonoBehaviour
{
    public List<Collider> enemies = new List<Collider>();
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemie") && !enemies.Contains(other))
        {
            enemies.Add(other);
            other.GetComponent<EnemieScriptCapacity>().TakeDamage(50);
        }
    }
}
