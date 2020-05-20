using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFences : MonoBehaviour
{
    public bool gen;

    public GameObject pillar; //0.1f en x
    public GameObject joint;  //1f en x
    public GameObject apex;
    public GameObject[] position;
    public uint pillars;
    public uint row = 1;
    private uint joints;

    public float Yscale = 1;
    private float Zscale = 1; // <- private

    private Vector3 smallOffset = new Vector3(0, 0, 0.5f);

    private GameObject empty;

    public GameObject type;
    public Vector3 typeOffset;
    public Vector3 finalRotation;
    public Material fenceMaterial;

    void Start()
    {
        Generate();
    }
    private void FixedUpdate()
    {
        if (gen)
        {
            gen = false;
            Destroy(empty);
            Generate();
        }
    }

    void Generate()
    {
        empty = new GameObject("Fence");

        joints = pillars + 1;
        for (int i = 0; i < 2; i++)
        {
            GameObject P = Instantiate(pillar, position[i].transform.position, Quaternion.identity) as GameObject;
            P.transform.localScale = new Vector3(P.transform.localScale.x, P.transform.localScale.y * Yscale, P.transform.localScale.x);
            P.transform.parent = empty.transform;

            GameObject A = Instantiate(apex, position[0].transform.position + new Vector3(0, Yscale, 0), Quaternion.identity) as GameObject;
            A.transform.parent = empty.transform;
        }

        float distance = Vector3.Distance(position[0].transform.position, position[1].transform.position);
        float size = ComputeSize(distance);

        Zscale = size / joints;

        CreatePillars();

        GenerateMesh();

        GenerateType(distance);

        AddScripts();

        empty.AddComponent<BoxCollider>();

        empty.transform.rotation = Quaternion.Euler(finalRotation);

        empty.tag = "Fence";

    }

    void CreatePillars()
    {
        Vector3 difference = position[0].transform.position - position[1].transform.position;
        Vector3 offset = difference / (pillars + 1);

        for (int i = 1; i < pillars + 2; i++)
        {
            GameObject P = Instantiate(pillar, position[0].transform.position - offset * (i), Quaternion.identity) as GameObject;
            P.transform.localScale = new Vector3(P.transform.localScale.x, P.transform.localScale.y * Yscale, P.transform.localScale.z);
            CreateJoints(offset, i);
            P.transform.parent = empty.transform;

            GameObject A = Instantiate(apex, position[0].transform.position - offset * (i) + new Vector3(0, Yscale, 0), Quaternion.identity) as GameObject;
            A.transform.parent = empty.transform;
        }
    }

    void CreateJoints(Vector3 offset, int iteration)
    {
        Vector3 Yoffset = new Vector3(0, Yscale / (row + 1), 0);
        for (int i = 1; i < row + 1; i++)
        {

            GameObject J = Instantiate(joint, position[0].transform.position + smallOffset / 10 - offset * iteration + Yoffset * i, Quaternion.identity) as GameObject;
            J.transform.localScale = new Vector3(J.transform.localScale.x, J.transform.localScale.y, J.transform.localScale.z * Zscale);
            J.transform.parent = empty.transform;
        }
    }


    float ComputeSize(float distance)
    {
        return distance -= 0.1f * (pillars + 1);
    }

    void GenerateType(float distance)
    {
        Vector3 offset = new Vector3(0, -Yscale / 2, distance / 2);
        GameObject T = Instantiate(type, position[0].transform.position - offset + typeOffset, Quaternion.identity) as GameObject;
        T.transform.parent = empty.transform;
    }

    void AddScripts()
    {
        empty.AddComponent<FenceDrop>().Ymovement = -Yscale;
    }

    void GenerateMesh()
    {
        MeshFilter[] meshFilters = empty.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {

            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }
        GameObject fence = new GameObject("fence");
        fence.AddComponent<MeshFilter>();
        fence.AddComponent<MeshRenderer>().material = fenceMaterial;
        fence.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        fence.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        fence.transform.gameObject.SetActive(true);
        

        Destroy(empty);
        empty = fence;
    }
}
