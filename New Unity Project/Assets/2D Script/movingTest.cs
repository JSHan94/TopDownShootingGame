using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class movingTest : MonoBehaviour
{

    public float speed;
    public int x;
    public int y;

    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;
    public float meshResolution;
    Mesh viewMesh;
    //public MeshFilter viewMeshFilter;

    void Start()
    {
        //viewMesh = new Mesh();
        //viewMesh.name = "View Mesh";
        //viewMeshFilter.mesh = viewMesh;
    }
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, y), step);
  
    }
    void LateUpdate()
    {
        //DrawFieldOfView();
    }
    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;


        //stepCount갯수 만큼의 viewAngle을 나눠 Draw
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;

        }

        int vertexCount = 50; // viewPoint 갯수 + 원점
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3]; // 원점~viewPoint까지 연결되는 triangle의 갯수는(N - 2)개, 한 triangle에 3개의 점이 필요하는 * 3 수행

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            //vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            //[0,1,2, 0,2,3, 0,3,4 ....] 와 같은식으로 꼭짓점 저장
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        

        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
        viewMesh.Clear();
    }
}
