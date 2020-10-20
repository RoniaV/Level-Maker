using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;
    [SerializeField]
    private float size;
    [SerializeField]
    private Material material;
    [SerializeField]
    private bool flipNormals;
    [SerializeField]
    private bool joinLastPoint;
    [SerializeField]
    private Transform parent;

    private GameObject obj;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    public void CreateObject()
    {
        mesh = new Mesh();

        obj = new GameObject();
        obj.transform.parent = parent;
        obj.AddComponent<MeshFilter>().mesh = mesh;
        obj.AddComponent<MeshRenderer>().material = material;
        obj.AddComponent<MeshCollider>().sharedMesh = mesh;
        
        CreateShape();
        UpdateMesh();
    }

    private void CreateShape()
    {
        vertices = new Vector3[points.Length * 2];
        
        bool altitude = flipNormals;
        for (int i = 0, e = 0, x = 0; x < 2; x++)
        {
            for (int y = 0; y < points.Length; y++)
            {
                vertices[i] = new Vector3(points[e].position.x, altitude ? size : -size, points[e].position.z);
                i++;
                e++;
            }
            altitude = !altitude;
            e = 0;
        }
        
        triangles = new int[(points.Length - (joinLastPoint ? 0 : 1)) * 6];

        int vert = 0;
        int tris = 0;

        for (int x = 0; x < points.Length - 1; x++)
        {
            triangles[tris + 0] = vert + 0;
            triangles[tris + 1] = vert + points.Length;
            triangles[tris + 2] = vert + 1;
            triangles[tris + 3] = vert + 1;
            triangles[tris + 4] = vert + points.Length;
            triangles[tris + 5] = vert + points.Length + 1;

            vert++;
            tris += 6;
        }
        if(joinLastPoint)
        {
            triangles[tris + 0] = vert + 0;
            triangles[tris + 1] = vert + points.Length;
            triangles[tris + 2] = 0;
            triangles[tris + 3] = 0;
            triangles[tris + 4] = vert + points.Length;
            triangles[tris + 5] = points.Length;
        }
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        mesh.RecalculateBounds();
        obj.GetComponent<MeshCollider>().sharedMesh = mesh;
    } 

}
