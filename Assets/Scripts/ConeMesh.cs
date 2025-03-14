using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeMesh
{
    int nSides;
    float height;
    float sideLength;
    List<Vector3> points = new List<Vector3>();

    public ConeMesh(int nSides, float height, float sideLength)
    {
        this.nSides = nSides;
        this.height = height;
        this.sideLength = sideLength/Mathf.Sqrt(2);
    }

    void setVertices()
    {
        float angle = 2 * Mathf.PI / nSides;

        for (int i = 0; i < nSides; i++)
        {
            float x = sideLength * Mathf.Cos(i * angle);
            float z = sideLength * Mathf.Sin(i * angle);
            points.Insert(i, new Vector3(x, 0.0f, z));
        }
    }

    Vector3[] faceVertices(int dir)
    {
        Vector3[] fv = new Vector3[3];

        fv[0] = points[dir];
        fv[1] = new Vector3(0, height, 0);
        if (dir == nSides - 1) dir = -1;
        fv[2] = points[dir + 1];

        return fv;
    }

    Vector3[] bottonVertices(int dir)
    {
        Vector3[] fv = new Vector3[3];

        fv[2] = points[dir];
        if (dir == nSides - 1) dir = -1;
        fv[1] = new Vector3(0, 0.0f, 0);
        fv[0] = points[dir + 1];

        return fv;
    }

    public void MakeCone(List<Vector3> vertices, List<int> triangles)
    {
        this.setVertices();

        for(int i = 0; i < nSides; i++)
        {
            vertices.AddRange(this.faceVertices(i));

            int vCount = vertices.Count;

            triangles.Add(vCount - 3);
            triangles.Add(vCount - 2);
            triangles.Add(vCount - 1);
        }

        for(int i = 0; i < nSides; i++)
        {
            vertices.AddRange(this.bottonVertices(i));

            int vCount = vertices.Count;

            triangles.Add(vCount - 3);
            triangles.Add(vCount - 2);
            triangles.Add(vCount - 1);
        }
    }
}
