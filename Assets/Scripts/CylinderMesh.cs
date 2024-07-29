using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CylinderMeshData
{
    public static int nSides = 8;
    public static float sideLength = 0.1f;

    public static Vector3[] vertices1 = new Vector3[nSides];
    public static Vector3[] vertices2 = new Vector3[nSides];

    public static void setVertices()
    {
        float angle = 2 * Mathf.PI / nSides;

        for(int i = nSides-1; i >= 0; i--)
        {
            float x = sideLength * Mathf.Cos(i * angle);
            float z = sideLength * Mathf.Sin(i * angle);
            vertices1[i] = new Vector3(x, 0.1f, z);
        }

        for(int i = 0; i < nSides; i++)
        {
            vertices2[i] = vertices1[i];
            vertices2[i][1] = 0.3f;
        }
    }

    public static Vector3[] faceVertices(int dir)
    {
        setVertices();
        Vector3[] fv = new Vector3[4];

        fv[0] = vertices1[dir];
        fv[1] = vertices2[dir];
        if(dir == nSides-1) dir = -1;
        fv[2] = vertices2[dir+1];
        fv[3] = vertices1[dir+1];

        return fv;
    }

    public static Vector3[] topVertices(int dir)
    {
        Vector3[] fv = new Vector3[3];

        fv[0] = vertices2[dir];
        if(dir == nSides-1) dir = -1;
        fv[1] = new Vector3(0,0.3f,0);
        fv[2] = vertices2[dir+1];

        return fv;
    }
}

public class CylinderMesh
{
    int nSides = CylinderMeshData.nSides;

    public void MakeCylinder(List<Vector3> vertices, List<int> triangles)
    {
        for(int i = 0; i < nSides; i++)
        {
            vertices.AddRange(CylinderMeshData.faceVertices(i));

            int vCount = vertices.Count;

            triangles.Add(vCount - 4);
            triangles.Add(vCount - 3);
            triangles.Add(vCount - 2);
            triangles.Add(vCount - 4);
            triangles.Add(vCount - 2);
            triangles.Add(vCount - 1);
        }

        for(int i = 0; i < nSides; i++)
        {
            vertices.AddRange(CylinderMeshData.topVertices(i));

            int vCount = vertices.Count;

            triangles.Add(vCount - 3);
            triangles.Add(vCount - 2);
            triangles.Add(vCount - 1);
        }
    }
}
