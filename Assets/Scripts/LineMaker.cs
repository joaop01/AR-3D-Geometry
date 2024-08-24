using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMaker : MonoBehaviour
{
    LineRenderer line;

	GameObject spawnedObject;

    List<Vector3> vertices;

	int poly;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        spawnedObject = GameObject.Find("XR Origin (AR Rig)").GetComponent<PlaceObject>().spawnedObject;
		poly = spawnedObject.GetComponent<MeshMaker>().polygon;
		line.materials[0].color = Color.blue;

        vertices = spawnedObject.GetComponent<MeshMaker>().vertices;

		//vertices.ForEach(i => Debug.Log(i));
    }

	void Update()
	{
        vertices = spawnedObject.GetComponent<MeshMaker>().vertices;

		if(poly == 0 || poly == 1)
		{
			line.positionCount = 3;
			line.SetPosition(0, vertices[0]);
			line.SetPosition(1, vertices[6]);
			line.SetPosition(2, vertices[7]);
		}

		else if(poly == 3 || poly == 5)
		{
			Vector3 center = spawnedObject.GetComponent<Renderer>().localBounds.center;
			center[1] = 0.0f;
			line.positionCount = 3;
			line.SetPosition(0, vertices[0]);
			line.SetPosition(1, vertices[1]);
			line.SetPosition(2, center);
		}
	}

}
