using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField]
    private Material Color; 

    private Mesh shape;

    private Vector3[] baseVertices = new Vector3[8];

    private Vector3[] finalVertices = new Vector3[24];

    private int[] triangles = new int[36];

    private int[][] allFaces = new int[6][];

    private int faceNumber = 6;

    private bool[] ToDraw ;

    public Vector3 Position ;

    public Quaternion Orientation ;

    // Start is called before the first frame update
    void Start()
    {
        Position = transform.position;
        
        this.gameObject.AddComponent<MeshFilter>();
        this.gameObject.AddComponent<MeshRenderer>();
        GetComponent<MeshFilter>().mesh = new Mesh();
        GetComponent<MeshRenderer>().material = Color;
        shape = GetComponent<MeshFilter>().mesh;
        
        
        // stocke dans baseVertices les coordonnees des 8 sommets du cube
        baseVertices[0] = new Vector3(1,1,1);
        baseVertices[1] = new Vector3(-1,1,1);
        baseVertices[2] = new Vector3(-1,-1,1);
        baseVertices[3] = new Vector3(1,-1,1);
        baseVertices[4] = new Vector3(-1,1,-1);
        baseVertices[5] = new Vector3(1,1,-1);
        baseVertices[6] = new Vector3(1,-1,-1);
        baseVertices[7] = new Vector3(-1,-1,-1);


        //chaque liste dans allFaces correspond aux 4 sommets associes a la face en question (le nombre correspond au sommet de baseVertices)
        allFaces[0] = new int[4] {0, 1, 2 , 3} ; // est
        allFaces[1] = new int[4] {5, 0, 3 , 6} ; // nord
        allFaces[2] = new int[4] {4, 5, 6 , 7} ; // ouest
        allFaces[3] = new int[4] {1, 4, 7 , 2} ; // sud
        allFaces[4] = new int[4] {5, 4, 1 , 0} ; // dessus
        allFaces[5] = new int[4] {3, 2, 7 , 6} ; // dessous

        GenerateCube();

    }

    private void GenerateCube()
    {
        int verticesByFaces = 4;
        int verticesCount = 0;
        int trianglesCount = 0;


        ToDraw = new bool[6]
        {
            "Air" == CheckCoo.CheckBlock(Position + new Vector3(0, 0, 1)),
            "Air" == CheckCoo.CheckBlock(Position + new Vector3(1, 0, 0)),
            "Air" == CheckCoo.CheckBlock(Position + new Vector3(0, 0, -1)),
            "Air" == CheckCoo.CheckBlock(Position + new Vector3(-1, 0, 0)),
            "Air" == CheckCoo.CheckBlock(Position + new Vector3(0, 1, 0)),
            "Air" == CheckCoo.CheckBlock(Position + new Vector3(0, -1, 0))
        };

        


        triangles = new int[36];

        //pour chaque face du cube
        for(int face = 0; face<faceNumber; face++)
        {
            if(ToDraw[face])
            {

                // 1 face = 2 triangles = 6 sommets, associe chaque sommet a l'index des vertices crees

                // en gros triangles gere les triangles par paquets de 3 vertices (sommets), et associer l'index de chacun permet de recuperer
                // les coordonnees du sommet directement depuis finalVertices
                triangles[trianglesCount + 0] = verticesCount;
                triangles[trianglesCount + 1] = verticesCount + 1;
                triangles[trianglesCount + 2] = verticesCount + 2;
                triangles[trianglesCount + 3] = verticesCount;
                triangles[trianglesCount + 4] = verticesCount + 2;
                triangles[trianglesCount + 5] = verticesCount + 3;

                

                trianglesCount+= faceNumber;

                //pr chaque sommet formant cette face
                for(int vertex = 0; vertex < verticesByFaces; vertex++)
                {
                    //rajoute les coordonnees du sommet a finalVertices puis augmente verticesCount (index)
                    Vector3 currentPoint = baseVertices[allFaces[face][vertex]] * 0.5f;
                    finalVertices[verticesCount] = currentPoint;
                    verticesCount++;
                }
            }
        }


        UpdateMesh();
    }

    

    private void UpdateMesh()
    {
        shape.Clear();

        shape.vertices = finalVertices;
        shape.triangles = triangles;

        shape.RecalculateNormals();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
