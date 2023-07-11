using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField]
    private Material Color;

    private Mesh Shape;

    private Vector3[] baseVertices = new Vector3[8];

    private Vector3[] finalVertices = new Vector3[24];

    private int[] triangles = new int[36];

    private int[][] allFaces = new int[6][];

    private int faceNumber = 6;

    private int CubeRadius = 50;

    public Vector3 Position { get => Position; set => Position = value; }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.AddComponent<MeshFilter>();
        this.gameObject.AddComponent<MeshRenderer>();
        GetComponent<MeshFilter>().mesh = new Mesh();
        GetComponent<MeshRenderer>().material = Color;
        Shape = GetComponent<MeshFilter>().mesh;

        baseVertices[0] = new Vector3(0,0,0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
