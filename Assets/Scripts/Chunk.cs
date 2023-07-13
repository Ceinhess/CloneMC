using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{

    [SerializeField]
    private GameObject CubePrefab;

    [SerializeField]
    private Quaternion Orientation;

    [SerializeField]
    public Vector3 Position;
    


    // donnees des blocs du chunk
    public Block[][][] Blocks = new Block[16][][];

    private GameObject[][][] WorldBlocks = new GameObject[16][][];

    


    // Start is called before the first frame update
    void Start()
    {

        Position = this.gameObject.transform.position;

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

                    //Debug.Log(Blocks[x][y][z].Position + Blocks[x][y][z].BlockID);
                }
            }
        }

        GenerateMeshes();
    }

    public void GenerateMeshes()
    {

        for(int x = 0; x < 16; x++)
        {
            WorldBlocks[x] = new GameObject[16][];
            for(int y = 0; y < 16; y++)
            {
                WorldBlocks[x][y] = new GameObject[16];
                for(int z = 0; z < 16; z++)
                {
                    
                    if(Blocks[x][y][z].BlockID != "Air")
                        WorldBlocks[x][y][z] = Instantiate(CubePrefab, Blocks[x][y][z].Position, Orientation);
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
