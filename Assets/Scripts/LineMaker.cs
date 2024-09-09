using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMaker : MonoBehaviour
{
    private enum Polygons {Cubo, Ortoedro, Esfera, Cone, Cilindro, Piramide, Prisma};

    LineRenderer line;

	GameObject spawnedObject;

    List<Vector3> vertices;

	int polygon;

	MeshMaker mesh;

    public void Start()
    {
        line = GetComponent<LineRenderer>();
        spawnedObject = GameObject.Find("XR Origin (AR Rig)").GetComponent<PlaceObject>().spawnedObject;
        polygon = spawnedObject.GetComponent<MeshMaker>().polygon;
        vertices = spawnedObject.GetComponent<MeshMaker>().vertices;

		//mesh.vertices.ForEach(i => Debug.Log(i));
    }

	void Update()
	{
        vertices = spawnedObject.GetComponent<MeshMaker>().vertices;
	}

	public void DiagonalCube()
	{
		line.positionCount = 2;
		line.SetPosition(0, vertices[0]);
		line.SetPosition(1, vertices[6]);
		line.materials[0].color = Color.red;
	}

	public void DiagonalSq()
	{
		line.positionCount = 2;
		line.SetPosition(0, vertices[1]);
		line.SetPosition(1, vertices[6]);
		line.materials[0].color = Color.blue;
	}

	public void Altura()
	{
		line.positionCount = 2;
		line.SetPosition(0, vertices[0]);
		line.SetPosition(1, vertices[1]);
		line.materials[0].color = Color.green;
	}

	public void AlturaPiramide()
	{
		Vector3 center = spawnedObject.GetComponent<Renderer>().localBounds.center;
		center[1] = 0.0f;
		line.positionCount = 3;
		line.SetPosition(0, vertices[0]);
		line.SetPosition(1, vertices[1]);
		line.SetPosition(2, center);
	}

	public void Base()
	{
	}
}
