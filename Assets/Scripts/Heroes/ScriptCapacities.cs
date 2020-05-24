using System.Collections;
using UnityEngine;

public abstract class ScriptCapacities : MonoBehaviour
{
    private const float time = 0.1f;
    private GetAllTurtleArena turtles;
    public bool isInTurtle;
    public abstract void Capacity1();
    public abstract void Capacity2();
    private void Start()
    {
        turtles = gameObject.GetComponent<GetAllTurtleArena>();
        StartCoroutine(CheckTurtles());
    }
    public void AreYouInTurtle()
    {
        foreach (var isIn in turtles.turtlesIsIn)
        {
            if(isIn)
            {
                isInTurtle = true;
                return;
            }
        }
        isInTurtle = false;
    }
    IEnumerator CheckTurtles()
    {
        AreYouInTurtle();
        yield return new WaitForSeconds(time);
        StartCoroutine(CheckTurtles());
    }
}
