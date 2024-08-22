using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderMesh
{
    int nSides;
    float height;
    float sideLength = 0.05f;
    List<Vector3> points1 = new List<Vector3>();
    List<Vector3> points2 = new List<Vector3>();

    public CylinderMesh(int nSides, float height)
    {
        this.nSides = nSides;
        this.height = height;
    }

    void setVertices()
    {
        float angle = 2 * Mathf.PI / nSides;

        for (int i = 0; i < nSides; i++)
        {
            float x = sideLength * Mathf.Cos(i * angle);
            float z = sideLength * Mathf.Sin(i * angle);
            points1.Insert(i, new Vector3(x, 0.0f, z));
            points2.Insert(i, new Vector3(x, height, z));
        }
    }

    Vector3[] faceVertices(int dir)
    {
        Vector3[] fv = new Vector3[4];

        fv[0] = points1[dir];
        fv[1] = points2[dir];
        if (dir == nSides - 1) dir = -1;
        fv[2] = points2[dir + 1];
        fv[3] = points1[dir + 1];

        return fv;
    }

    Vector3[] topVertices(int dir)
    {
        Vector3[] fv = new Vector3[3];

        fv[0] = points2[dir];
        if (dir == nSides - 1) dir = -1;
        fv[1] = new Vector3(0, height, 0);
        fv[2] = points2[dir + 1];

        return fv;
    }

    Vector3[] bottonVertices(int dir)
    {
        Vector3[] fv = new Vector3[3];

        fv[2] = points1[dir];
        if (dir == nSides - 1) dir = -1;
        fv[1] = new Vector3(0, 0.0f, 0);
        fv[0] = points1[dir + 1];

        return fv;
    }

    public void MakeCylinder(List<Vector3> vertices, List<int> triangles)
    {
        this.setVertices();

        for(int i = 0; i < nSides; i++)
        {
            vertices.AddRange(this.faceVertices(i));

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
            vertices.AddRange(this.topVertices(i));

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
