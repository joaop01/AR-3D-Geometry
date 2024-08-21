using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshMaker : MonoBehaviour
{
    public enum Polygons {Cone, Cylinder};

    public int polygon = (int) Polygons.Cone;

    public int nSides;
    
    public float height;

    Mesh mesh;

    List<Vector3> vertices;
    List<int> triangles;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    public void Start()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        switch(polygon)
        {
            case (int) Polygons.Cylinder:
                CylinderMesh cylinder = new CylinderMesh(nSides, height);
                cylinder.MakeCylinder(vertices, triangles);
            break;

			case (int) Polygons.Cone:
                ConeMesh cone = new ConeMesh(nSides, height);
                cone.MakeCone(vertices, triangles);
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
