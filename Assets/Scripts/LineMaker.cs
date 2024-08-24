using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMaker : MonoBehaviour
{
    LineRenderer line;

	GameObject lineObject;

    List<Vector3> vertices;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        lineObject = GameObject.Find("XR Origin (AR Rig)").GetComponent<PlaceObject>().spawnedObject;
    }

	void Update()
	{
        vertices = lineObject.GetComponent<MeshMaker>().vertices;
		line.positionCount = 3;
		line.SetPosition(0, vertices[0]);
		line.SetPosition(1, vertices[6]);
		line.SetPosition(2, vertices[7]);
	}

}
