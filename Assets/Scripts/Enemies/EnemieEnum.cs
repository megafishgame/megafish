using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieEnum : MonoBehaviour
{
    public enum gender
    {
        warrior,
        archer,
        kamikaze,
    }
    public enum movements
    {
        motionless,
        random_distance,
        random_kamikaze,
        pattern,
    }
}
