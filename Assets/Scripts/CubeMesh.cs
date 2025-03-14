using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CubeMeshData
{
    public static Vector3[] vertices = {
        new Vector3(0.1f, 0.1f, 0.1f),
        new Vector3(-0.1f, 0.1f, 0.1f),
        new Vector3(-0.1f, -0.1f, 0.1f),
        new Vector3(0.1f, -0.1f, 0.1f),
        new Vector3(-0.1f, 0.1f, -0.1f),
        new Vector3(0.1f, 0.1f, -0.1f),
        new Vector3(0.1f, -0.1f, -0.1f),
        new Vector3(-0.1f, -0.1f, -0.1f),
    };

    public static int[][] faceTriangles = {
        new int[] {0, 1, 2, 3},
        new int[] {5, 0, 3, 6},
        new int[] {4, 5, 6, 7},
        new int[] {1, 4, 7, 2},
        new int[] {5, 4, 1, 0},
        new int[] {3, 2, 7, 6},
    };

    public static Vector3[] faceVertices(int dir)
    {
        Vector3[] fv = new Vector3[4];
        for (int i = 0; i < fv.Length; i++) {
            fv[i] = vertices[faceTriangles[dir][i]];
        }
        return fv;
    }
}

public class CubeMesh
{
    public void MakeCube(int dir, List<Vector3> vertices, List<int> triangles)
    {
        vertices.AddRange(CubeMeshData.faceVertices(dir));
        int vCount = vertices.Count;

        triangles.Add(vCount - 4);
        triangles.Add(vCount - 3);
        triangles.Add(vCount - 2);
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 2);
        triangles.Add(vCount - 1);
    }
}
