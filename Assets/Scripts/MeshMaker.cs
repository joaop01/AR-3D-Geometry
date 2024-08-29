using System.Collections;
using System.Collections.Generic;


using UnityEngine;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshMaker : MonoBehaviour
{
    public enum Polygons {Cubo, Ortoedro, Esfera, Cone, Cilindro, Piramide, Prisma};

    [System.NonSerialized]
    public int polygon;

    public int nSides;

    public float height;

    public float radius;

    public List<Vector3> vertices;

    List<int> triangles;

    private Mesh mesh;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }
    
    public void Start()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

		CylinderMesh cylinder;
		ConeMesh cone;

        switch(polygon)
        {
            case (int) Polygons.Cubo:
                cylinder = new CylinderMesh(4, radius, radius);
                cylinder.MakeCylinder(vertices, triangles);
            break;

			case (int) Polygons.Ortoedro:
                cylinder = new CylinderMesh(4, height, radius);
                cylinder.MakeCylinder(vertices, triangles);
            break;

			case (int) Polygons.Esfera:
				GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				vertices.AddRange(sphere.GetComponent<MeshFilter>().mesh.vertices);
				triangles.AddRange(sphere.GetComponent<MeshFilter>().mesh.triangles);
				Destroy(sphere);
            break;

			case (int) Polygons.Cone:
                cone = new ConeMesh(50, height, radius);
                cone.MakeCone(vertices, triangles);
            break;

			case (int) Polygons.Cilindro:
                cylinder = new CylinderMesh(50, height, radius);
                cylinder.MakeCylinder(vertices, triangles);
            break;

			case (int) Polygons.Piramide:
                cone = new ConeMesh(nSides, height, radius);
                cone.MakeCone(vertices, triangles);
            break;

			case (int) Polygons.Prisma:
                cylinder = new CylinderMesh(nSides, height, radius);
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
