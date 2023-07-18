using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{

     [SerializeField]
    private Material Color; 

    private Vector3[] baseVertices = new Vector3[8];

    private List<int> triangles = new List<int>();

    [SerializeField]
    private Quaternion Orientation;

    [SerializeField]
    public Vector3 Position;

    private Mesh shape;

    private int faceNumber = 6;

    private int[][] allFaces;

    private int verticesCount = 0;

    private List<Vector3> finalVertices = new List<Vector3>();

    public string State = "Initiating";
    


    // donnees des blocs du chunk
    public Block[][][] Blocks = new Block[16][][];

    private GameObject[][][] WorldBlocks = new GameObject[16][][];

    


    // Start is called before the first frame update
    void Start()
    {
        Position = this.gameObject.transform.position / 16;
        //this.gameObject.transform.position = new Vector3(0,0,0);

        //Mesh Building Initiation
        this.gameObject.AddComponent<MeshFilter>();
        this.gameObject.AddComponent<MeshRenderer>();
        GetComponent<MeshFilter>().mesh = new Mesh();
        GetComponent<MeshRenderer>().material = Color;
        shape = GetComponent<MeshFilter>().mesh;
        
        allFaces = new int[6][];

        allFaces[0] = new int[4] {0, 1, 2 , 3} ; // est
        allFaces[1] = new int[4] {5, 0, 3 , 6} ; // nord
        allFaces[2] = new int[4] {4, 5, 6 , 7} ; // ouest
        allFaces[3] = new int[4] {1, 4, 7 , 2} ; // sud
        allFaces[4] = new int[4] {5, 4, 1 , 0} ; // dessus
        allFaces[5] = new int[4] {3, 2, 7 , 6} ; // dessous

        

        

        for(int x = 0; x < 16; x++)
        {
            Blocks[x] = new Block[16][];
            for(int y = 0; y < 16; y++)
            {
                Blocks[x][y] = new Block[16];
                for(int z = 0; z < 16; z++)
                {
                    Vector3 WorldPos = Position * 16 + new Vector3(x,y,z);
                    Blocks[x][y][z] = new Block(WorldPos);
                    if(-3 < WorldPos.y && WorldPos.y < 1 + WorldPos.z /18 + WorldPos.x / 24) Blocks[x][y][z].BlockID = "Grass";

                }
            }
        }

        State = "ReadyForInit";

        
    }

    public void GenerateMeshes()
    {
        

        for(int x = 0; x < 16; x++)
        {
            for(int y = 0; y < 16; y++)
            {
                for(int z = 0; z < 16; z++)
                {
                    
                    if(Blocks[x][y][z].BlockID != "Air")
                    {
                        int verticesByFaces = 4;
                        Vector3 _Pos = Blocks[x][y][z].Position;
                        Vector3 _Pos3 = new Vector3(x,y,z);

                        bool[] ToDraw = new bool[6]
                        {
                            "Air" == CheckCoo.CheckBlock(_Pos + new Vector3(0, 0, 1)),
                            "Air" == CheckCoo.CheckBlock(_Pos + new Vector3(1, 0, 0)),
                            "Air" == CheckCoo.CheckBlock(_Pos + new Vector3(0, 0, -1)),
                            "Air" == CheckCoo.CheckBlock(_Pos + new Vector3(-1, 0, 0)),
                            "Air" == CheckCoo.CheckBlock(_Pos + new Vector3(0, 1, 0)),
                            "Air" == CheckCoo.CheckBlock(_Pos + new Vector3(0, -1, 0))
                        };

                        baseVertices[0] = new Vector3(0.5f,0.5f,0.5f) + _Pos3;
                        baseVertices[1] = new Vector3(-0.5f,0.5f,0.5f) + _Pos3;
                        baseVertices[2] = new Vector3(-0.5f,-0.5f,0.5f) + _Pos3;
                        baseVertices[3] = new Vector3(0.5f,-0.5f,0.5f) + _Pos3;
                        baseVertices[4] = new Vector3(-0.5f,0.5f,-0.5f) + _Pos3;
                        baseVertices[5] = new Vector3(0.5f,0.5f,-0.5f) + _Pos3;
                        baseVertices[6] = new Vector3(0.5f,-0.5f,-0.5f) + _Pos3;
                        baseVertices[7] = new Vector3(-0.5f,-0.5f,-0.5f) + _Pos3;

                        for(int face = 0; face<faceNumber; face++)
                        {
                            if(ToDraw[face])
                            {
                                // 1 face = 2 triangles = 6 sommets, associe chaque sommet a l'index des vertices crees

                                // en gros triangles gere les triangles par paquets de 3 vertices (sommets), et associer l'index de chacun permet de recuperer
                                // les coordonnees du sommet directement depuis finalVertices
                                triangles.Add(verticesCount);
                                triangles.Add(verticesCount + 1);
                                triangles.Add(verticesCount + 2);
                                triangles.Add(verticesCount);
                                triangles.Add(verticesCount + 2);
                                triangles.Add(verticesCount + 3);

                                //pr chaque sommet formant cette face
                                for(int vertex = 0; vertex < verticesByFaces; vertex++)
                                {
                                    //rajoute les coordonnees du sommet a finalVertices puis augmente verticesCount (index)
                                    Vector3 currentPoint = baseVertices[allFaces[face][vertex]];
                                    finalVertices.Add(currentPoint);
                                    verticesCount++;
                                }
                            }
                        }
                    }
                }
            }
        }
        
        UpdateMesh();
    }

    

    private void UpdateMesh()
    {
        shape.Clear();

        shape.vertices = finalVertices.ToArray();
        shape.triangles = triangles.ToArray();

        shape.RecalculateNormals();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
