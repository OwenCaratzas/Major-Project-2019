using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class BuildMesh : MonoBehaviour
{
    GameObject parent;
    Sentry aiScript;
    Mesh mesh;

    public Vector3[] vertices;
    public int[] triangles;

    private float _xMultiplier = 5.0f;
    private float _yMultiplier = 5.0f;
    private float _zMultiplier = 10.0f;

    private void Start()
    {
        parent = GetComponentInParent<Transform>().gameObject;
        aiScript = parent.GetComponentInParent<Sentry>();
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        CreateCone2();
        UpdateMesh();
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void Update()
    {
        //UpdateMesh();
        //Debug.DrawRay(transform.position, new Vector3(-1 * _xMultiplier, 0, 1 * _zMultiplier), Color.blue);
    }

    void CreateCone()
    {
        // start with 4 triangles
        vertices = new Vector3[]
            {
                //// using the perspective of this cone coming out of a face

                //// base point, the peak of the cone
                //new Vector3(aiScript.transform.position.x, aiScript.transform.position.y, aiScript.transform.position.z),
                //// top vertice of the circle
                //new Vector3(aiScript.transform.position.x, aiScript.radius, aiScript.height),
                
                // ^^^^^^ keep working on this if it works

            // vert 0
            new Vector3(0, 0, 0),
            // vert 1
            new Vector3(0, 1, 1),
            // vert 2
            new Vector3(1, 0, 1),
            // vert 3
            new Vector3(0, -1, 1),
            // vert 4
            new Vector3(-1, 0, 1),
            // vert 5
            new Vector3(0, 0, 1)

            };

        triangles = new int[]
            {
                // peak end
                0, 1, 2,
                0, 2, 3,
                0, 3, 4,
                0, 4, 1,
                // flat end
                5, 2, 1,
                5, 3, 2,
                5, 4, 3,
                5, 1, 4
            };
    }

    void CreateCone2()
    {
        // Gonna go with 8 faces now
        vertices = new Vector3[]
            {
                //// using the perspective of this cone coming out of a face

                //// base point, the peak of the cone
                //new Vector3(aiScript.transform.position.x, aiScript.transform.position.y, aiScript.transform.position.z),
                //// top vertice of the circle
                //new Vector3(aiScript.transform.position.x, aiScript.radius, aiScript.height),
                
                // ^^^^^^ keep working on this if it works

            // vert 0
            new Vector3(0, 0, 0),
            // vert 1
            new Vector3(0, 1 * _yMultiplier, 1 * _zMultiplier),
            // vert 2
            new Vector3(0.75f * _xMultiplier, 0.75f * _yMultiplier, 1 * _zMultiplier),
            // vert 3
            new Vector3(1 * _xMultiplier, 0, 1 * _zMultiplier),
            // vert 4
            new Vector3(0.75f * _xMultiplier, -0.75f * _yMultiplier, 1 * _zMultiplier),
            // vert 5
            new Vector3(0, -1 * _yMultiplier, 1 * _zMultiplier),
            // vert 6
            new Vector3(-0.75f * _xMultiplier, -0.75f * _yMultiplier, 1 * _zMultiplier),
            // vert 7
            new Vector3(-1 * _xMultiplier, 0, 1 * _zMultiplier),
            // vert 8
            new Vector3(-0.75f * _xMultiplier, 0.75f * _yMultiplier, 1 * _zMultiplier),
            // vert 9
            new Vector3(0, 0, 1 * _zMultiplier)

            };

        triangles = new int[]
            {
                // outside

                // peak end
                0, 1, 2,
                0, 2, 3,
                0, 3, 4,
                0, 4, 5,
                0, 5, 6,
                0, 6, 7,
                0, 7, 8,
                0, 8, 1,
                // flat end
                9, 2, 1,
                9, 3, 2,
                9, 4, 3,
                9, 5, 4,
                9, 6, 5,
                9, 7, 6,
                9, 8, 7,
                9, 1, 8
                
                //,
                //// inside
                //
                // // peak end
                //0, 2, 1,
                //0, 3, 2,
                //0, 4, 3,
                //0, 5, 4,
                //0, 6, 5,
                //0, 7, 6,
                //0, 8, 7,
                //0, 1, 8,
                //// flat end
                //9, 1, 2,
                //9, 2, 3,
                //9, 3, 4,
                //9, 4, 5,
                //9, 5, 6,
                //9, 6, 7,
                //9, 7, 8,
                //9, 8, 1
            };
    }
}
