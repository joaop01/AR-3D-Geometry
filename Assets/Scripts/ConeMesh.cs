using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConeMeshData
{
    public static int nSides = 20;
    public static float sideLength = 0.1f;

    public static Vector3[] vertices = new Vector3[nSides];

    public static void setVertices()
    {
        float angle = 2 * Mathf.PI / nSides;

        for(int i = nSides-1; i >= 0; i--)
        {
            float x = sideLength * Mathf.Cos(i * angle);
            float z = sideLength * Mathf.Sin(i * angle);
            vertices[i] = new Vector3(x, 0.1f, z);
        }
    }

    public static Vector3[] faceVertices(int dir)
    {
        setVertices();
        Vector3[] fv = new Vector3[3];

        fv[0] = vertices[dir];
        fv[1] = new Vector3(0, 0.3f, 0);
        if(dir == nSides-1) dir = -1;
        fv[2] = vertices[dir+1];

        return fv;
    }
}

public class ConeMesh
{
    int nSides = ConeMeshData.nSides;

    public void MakeCone(List<Vector3> vertices, List<int> triangles)
    {
        for(int i = 0; i < nSides; i++)
        {
            vertices.AddRange(ConeMeshData.faceVertices(i));

            int vCount = vertices.Count;

            triangles.Add(vCount - 3);
            triangles.Add(vCount - 2);
            triangles.Add(vCount - 1);
        }
    }
}
