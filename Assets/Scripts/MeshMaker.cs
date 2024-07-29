using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshMaker : MonoBehaviour
{
    public enum Polygons {Cylinder, Cone};

    public Polygons polygon = Polygons.Cylinder;

    Mesh mesh;

    List<Vector3> vertices;
    List<int> triangles;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    void Start()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        switch(polygon)
        {
            case Polygons.Cylinder:
                CylinderMesh cylinder = new CylinderMesh();
                cylinder.MakeCylinder(vertices, triangles);
            break;
        }

        UpdateMesh();
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }
}
