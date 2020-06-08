using UnityEngine;
using System.Collections;
using System.Drawing;

[RequireComponent(typeof(LineRenderer))]
public class CrystalLaserBeam : MonoBehaviour
{
    LineRenderer lineRenderer;
    ParticleSystem particleSystem;
    public GameObject beamOut;
    public GameObject beamStop;
    public float sizeSphere;
    public bool init;
    public bool active;

    public int steps = 40;
    public float offset = 0.5f;
    public GameObject childCrystal;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        if (init)
            active = true;
    }
    private void FixedUpdate()
    {
        if (init || active)
            DrawLine();
        else
        {
            lineRenderer.enabled = false;
            particleSystem.enableEmission = false;
        }
        if(!active && childCrystal != null)
        {
            childCrystal.GetComponent<CrystalLaserBeam>().active = false;
            childCrystal = null;
        }
    }
    public void DrawLine()
    {
        beamStop.transform.position = beamOut.transform.position + transform.forward * offset;
        lineRenderer.enabled = true;
        particleSystem.enableEmission = true;

        for (int i = 0; i < steps; i++)
        {
            if (!CheckValidity())
            {
                beamStop.transform.position += transform.forward/2;
            }
            else
                break;
        }

        lineRenderer.SetPosition(0, beamOut.transform.position);
        lineRenderer.SetPosition(1, beamStop.transform.position);
        CheckIfNextIsCrystal();
    }
    bool CheckValidity()
    {
        return Physics.CheckSphere(beamStop.transform.position, sizeSphere);
    }
    void CheckIfNextIsCrystal()
    {
        Collider[] colliders = Physics.OverlapSphere(beamStop.transform.position, sizeSphere);
        bool found = false; 
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Crystal") && collider.gameObject != gameObject)
            {
                collider.GetComponent<CrystalLaserBeam>().active = true;
                childCrystal = collider.gameObject;
                found = true;
                break;
            }
            else if (collider.CompareTag("EndCrystal") && !collider.GetComponent<ActionEnd>().active)
            {
                collider.GetComponent<ActionEnd>().action = true;
            }
        }
        if(!found && childCrystal != null)
        {
            childCrystal.GetComponent<CrystalLaserBeam>().active = false;
            childCrystal = null;
        }
    }
}