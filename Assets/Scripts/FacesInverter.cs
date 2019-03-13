using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacesInverter : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        // Invert normals
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        mesh.normals = normals;

        // Invert triangles
        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            int[] tris = mesh.GetTriangles(i);

            for (int j = 0; j < tris.Length; j += 3)
            {
                // Swap order of tri vertices
                int tmp = tris[j];
                tris[j] = tris[j + 1];
                tris[j + 1] = tmp;
            }

            mesh.SetTriangles(tris, i);
        }
    }
}
