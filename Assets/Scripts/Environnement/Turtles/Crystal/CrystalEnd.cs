using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalEnd : MonoBehaviour
{
    public bool action;
    public bool active;
    public string actionString;
    public bool keepReference;
    public GameObject objectAction;

    private void FixedUpdate()
    {
        if (action)
        {
            action = false;
            active = true;
            if(keepReference)
                objectAction.GetComponent<KeepReference>().reference.SendMessage(actionString);
            else
                gameObject.SendMessage(actionString);
        }
    }
}
